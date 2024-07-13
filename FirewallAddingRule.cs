using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using NetFwTypeLib;

namespace PersonFix
{
    internal class FirewallAddingRule
    {
        private static void Main(string[] args)
        {
            // Setup Variables
            string[] IPtoBlock = ["0.0.0.0", "1.1.1.1", "2.2.2.2"];
            string FirewallRuleName = "TestRule";
            string FirewallRuleDescription = "Description";

            try
            {
                Type Policy = Type.GetTypeFromProgID("HNetCfg.FwPolicy2", false);
                INetFwPolicy2 FwPolicy = (INetFwPolicy2)Activator.CreateInstance(Policy);
                INetFwRules rules = FwPolicy.Rules;
                Type RuleType = Type.GetTypeFromProgID("HNetCfg.FWRule");
                INetFwRule rule = (INetFwRule)Activator.CreateInstance(RuleType);

                rule.Name = FirewallRuleName;
                rule.Description = FirewallRuleDescription;
                rule.Protocol = 6;
                rule.RemoteAddresses = IPtoBlock[0] + "," + IPtoBlock[1]; // For few IP Addresses
                // rule.RemoteAddresses = "0.0.0.0"; // For solo IP Addresses 
                rule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
                rule.Enabled = true;
                rules.Add(rule);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            Console.ReadLine();
        }
    }
}
