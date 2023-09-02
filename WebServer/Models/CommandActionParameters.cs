using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataApiService.Models;
using Microsoft.AspNetCore.Authentication;

namespace WebServer.Models
{
    /// <summary>
    /// Набор параметров для вывода событий
    /// </summary>
    public class CommandActionParameters
    {
        public string TerminalIds { get; set; }
        /// <summary>
        /// Поле сортировки
        /// </summary>
        public int Order_num { get; set; }

        /// <summary>
        /// Порядок сортировки
        /// </summary>
        public string Order_direction { get; set; }

        /// <summary>
        /// Идентификатор команды ??
        /// </summary>
        public string Command_id { get; set; }

        public string parameter_name1 { get; set; }
        public string parameter_name2 { get; set; }
        public string parameter_name3 { get; set; }
    }
}
