﻿/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using Easy.Extend;
using System.Text.RegularExpressions;

namespace ZKEACMS.SpiderLog.Models
{
    public class SearchEngine
    {
        public string Name { get; set; }
        public string Regex { get; set; }

        private Regex _regex;
        public bool IsMatch(string userAgent)
        {
            if (userAgent.IsNullOrEmpty()) return false;

            if (_regex == null) _regex = new Regex(Regex, RegexOptions.IgnoreCase);

            return _regex.IsMatch(userAgent);
        }
    }
}
