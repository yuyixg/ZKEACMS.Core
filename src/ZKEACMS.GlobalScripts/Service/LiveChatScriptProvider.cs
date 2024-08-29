/* http://www.zkea.net/ 
 * Copyright (c) ZKEASOFT. All rights reserved. 
 * http://www.zkea.net/licenses */

using ZKEACMS.GlobalScripts.Models;
using ZKEACMS.Setting;

namespace ZKEACMS.GlobalScripts.Service
{
    public class LiveChatScriptProvider : GlobalScriptsProviderService<LiveChatScript>
    {
        public LiveChatScriptProvider(IApplicationContextAccessor applicationContextAccessor, IApplicationSettingService applicationSettingService)
            : base(applicationContextAccessor, applicationSettingService)
        {
        }
    }
}
