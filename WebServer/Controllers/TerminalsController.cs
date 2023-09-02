using DataApiService;
using DataApiService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Threading.Tasks;
using WebServer.Models;

namespace WebServer.Controllers
{
    public class TerminalsController : BaseController
    {
        private readonly ILogger<EventsController> _logger;
        private IDataManager _dataManager;
        private static List<SendCommandResult> _commandSendHistory;

        static TerminalsController()
        {
            _commandSendHistory = new List<SendCommandResult>();
        }

        public TerminalsController(ILogger<EventsController> logger, IDataManager dataManager)
        {
            _logger = logger;
            _dataManager = dataManager;

            _dataManager.Auth("user2", "password2");
        }

        public async Task<IActionResult> Index()
        {
            var commands = await _dataManager.GetItems<CommandResults>("commands/types", new Dictionary<string, string>());

            //Мапинг в дроп лист
            var resultList = commands?.Select(x => new SelectListItem($"{x.Name}", x.Id.ToString())).ToList();
            var resultCommandParameters = commands?.Select(x => new { Id = x.Id,
                Data = new {
                    parameter_name1 = x.ParameterOne,
                    parameter_name2 = x.ParameterTwo,
                    parameter_name3 = x.ParameterThree,
                    parameter_default_value1 = x.ParameterOneDefault,
                    parameter_default_value2 = x.ParameterTwoDefault,
                    parameter_default_value3 = x.ParameterThreeDefault,
                }
            });

            //Значение для "Все"
            ViewData["Commands"] = resultList;

            ViewData["CommandParameters"] = JsonSerializer.Serialize(new List<dynamic>(resultCommandParameters));

            return View(_commandSendHistory);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CommandActionParameters pars)
        {
            var terminalIds = pars.TerminalIds?.Split(Environment.NewLine);
            var terminalIdValue = terminalIds?.FirstOrDefault();
            decimal terminalId = 0;

            //Проверка параметров запроса
            if (
                !ModelState.IsValid
                || !decimal.TryParse(terminalIdValue, out terminalId)
            ) {
                //Можно формировать сообщение или отправить на страницу ошибок, пока так
                //TODO Не в продакшен
                return BadRequest();
            }

            var parameters = new Dictionary<string, object>
            {
                { "command_id", pars.Command_id },
                { "parameter1", pars.parameter_name1 ?? "0" },
                { "parameter2", pars.parameter_name2 ?? "0" },
                { "parameter3", pars.parameter_name3 ?? "0" },
            };

            var sendCommand = await _dataManager
                                        .PostItems<SendCommandResult>($"terminals/{terminalId}/commands", parameters);

            var commands = await _dataManager.GetItems<CommandResults>("commands/types", new Dictionary<string, string>());
            var resultCommandParameters = commands?.Select(x => new {
                x.Id,
                Data = new
                {
                    parameter_name1 = x.ParameterOne,
                    parameter_name2 = x.ParameterTwo,
                    parameter_name3 = x.ParameterThree,
                    parameter_default_value1 = x.ParameterOneDefault,
                    parameter_default_value2 = x.ParameterTwoDefault,
                    parameter_default_value3 = x.ParameterThreeDefault,
                }
            });

            SendCommandResult responesData;
            if (
                (responesData = sendCommand?.FirstOrDefault()) != null
                && commands != null
            ) {
                var commandName = commands.FirstOrDefault(c => c.Id == responesData.CommandId);

                _commandSendHistory.Add(new SendCommandResult()
                {
                    TimeCreated = responesData.TimeCreated,
                    CommandName = commandName.Name,
                    Parameter1 = responesData.Parameter1,
                    Parameter2 = responesData.Parameter2,
                    Parameter3 = responesData.Parameter3,
                    StateName = responesData.StateName,
                });
            }

            //Мапинг в дроп лист
            var resultList = commands?.Select(x => new SelectListItem($"{x.Name}", x.Id.ToString())).ToList();

            //Значение для "Все"
            resultList.Insert(0, new SelectListItem(" Не выбрано ", "", true));
            ViewData["Commands"] = resultList;
            ViewData["CommandParameters"] = JsonSerializer.Serialize(new List<dynamic>(resultCommandParameters));

            return View(_commandSendHistory);
        }
    }
}
