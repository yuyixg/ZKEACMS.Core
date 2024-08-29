﻿/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

namespace Easy.Mvc.DataAnnotations
{
    class Mapping
    {
        public Mapping(string validatorName, string property)
        {
            ValidatorName = validatorName;
            Property = property;
        }
        public string ValidatorName { get; set; }
        public string Property { get; set; }
    }
}
