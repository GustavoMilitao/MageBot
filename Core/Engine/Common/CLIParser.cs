using MageBot.Util.IO;
using MageBot.Protocol.Messages.Game.Chat;
using MageBot.Protocol.Types.Game.Context.Roleplay;
using MageBot.Util.Enums.Internal;
using Util.Util.Text.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MageBot.Core.Monsters;
using MageBot.Core.Fight;
using System.Threading.Tasks;
using MageBot.Core.Account;
using MageBot.Core.Path;

namespace MageBot.Core.Engine.Common
{
    public class CLIParser
    {
        /* Command Line Parser */

        #region Fields
        private Account.Account account;

        /// <summary>
        /// Stores the commands history.
        /// </summary>
        public static List<string> CommandsHistory = new List<string>();

        /// <summary>
        /// The character used to distinguish which command lines parameters.
        /// </summary>
        private const string PARAM_SEPARATOR = "-";

        /// <summary>
        /// Stores the name=value required parameters.
        /// </summary>
        private static Dictionary<string, string> requiredParameters;

        /// <summary>
        /// Optional parameters.
        /// </summary>
        private static Dictionary<string, string> optionalParameters;

        /// <summary>
        /// Stores the list of the supported switches.
        /// </summary>
        private static Dictionary<string, bool> switches;

        /// <summary>
        /// Store the list of missing required parameters.
        /// </summary>
        private static List<string> missingRequiredParameters;

        /// <summary>
        /// Store the list of missing values of parameters.
        /// </summary>
        private static List<string> missingValue;

        /// <summary>
        /// Contains the raw arguments.
        /// </summary>
        private static Dictionary<string, string> rawArguments;

        /// <summary>
        /// Store the result of the command in order to display it.
        /// </summary>
        private List<string> result;
        #endregion

        #region Constructors
        public CLIParser(Account.Account Account)
        {
            account = Account;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Main function that parse a given line.
        /// </summary>
        /// <param name="expectedParams">
        /// The list of the required parameters.
        /// </param>
        /// <returns>The string to display.</returns>
        public List<string> Parse(string cmdLine)
        {
            CLIParser.CommandsHistory.Add(cmdLine);
            result = new List<string>();
            List<string> split = cmdLine.Split(' ').ToList();
            string mainComand = split[0];
            split.RemoveAt(0);
            string passedCommands = String.Join(" ", split);
            switch (mainComand)
            {
                case "/help":
                    if (split.Count > 0)
                        return Usage(split[0]);
                    else
                        return Usage();
                case "/move":
                    DefineRequiredParameters(new string[] { });
                    DefineOptionalParameter(new string[] { "-cell = 0", "-dir = null" });
                    ParseArguments(passedCommands);
                    return Move().Result;
                case "/mapid":
                    return new List<string>() { "L'id de la map est : " + account.MapData.Id };
                case "/cellid":
                    return new List<string>() { "Le joueur se trouve sur la cellule " + account.MapData.Character.Disposition.CellId };
                case "/cell":
                    DefineRequiredParameters(new string[] { });
                    DefineOptionalParameter(new string[] { "-npc = 0", "-elem = 0", "-player = null" });
                    ParseArguments(passedCommands);
                    return Cell();
                case "/say":
                    DefineRequiredParameters(new string[] { "-c", "-m" });
                    DefineOptionalParameter(new string[] { "-dest = null" });
                    ParseArguments(passedCommands);
                    return Say();
                case "/entities":
                    DefineRequiredParameters(new string[] { });
                    DefineOptionalParameter(new string[] { });
                    DefineSwitches(new string[] { "-v" });
                    ParseArguments(passedCommands);
                    return DispEntities();
                case "/path":
                    DefineRequiredParameters(new string[] { });
                    DefineOptionalParameter(new string[] { "-load = null", "-name = Unknown" });
                    DefineSwitches(new string[] { "-start", "-stop" });
                    ParseArguments(passedCommands);
                    return Path();
                case "/fight":
                    DefineRequiredParameters(new string[] { });
                    DefineOptionalParameter(new string[] { });
                    DefineSwitches(new string[] { "-launch", "-lock", "-l", "-v", "-t", "-me", "-i" });
                    ParseArguments(passedCommands);
                    return Fight().Result;
                case "/gather":
                    DefineRequiredParameters(new string[] { });
                    DefineOptionalParameter(new string[] { });
                    DefineSwitches(new string[] { "-launch", "-stats" });
                    DefineRequiredParameters(new string[] { });
                    DefineOptionalParameter(new string[] { });
                    DefineSwitches(new string[] { "-launch", "-stats" });
                    ParseArguments(passedCommands);
                    return Gather();
            }
            return Usage();
        }




        #endregion

        #region Private Methods
        /// <summary>
        /// Define the required parameters that the user of the program
        /// must provide.
        /// </summary>
        /// <param name="expectedParams">
        /// The list of the required parameters.
        /// </param>
        private void DefineRequiredParameters(string[] requiredParameterNames)
        {
            CLIParser.requiredParameters = new Dictionary<string, string>();

            foreach (string param in requiredParameterNames)
            {
                string temp = param;
                temp = temp.Trim();
                if (string.IsNullOrEmpty(param))
                {
                    string ERRORMessage = "ERROR: The required command line parameter '" + param + "' is empty.";
                    account.Log(new ErrorTextInformation(ERRORMessage), 0);
                }

                CLIParser.requiredParameters.Add(param, string.Empty);
            }
        }

        /// <summary>
        /// Define the optional parameters. The parameters must be provided with their
        /// default values in the following format "paramName=paramValue".
        /// </summary>
        /// <param name="optionalParameters">
        /// The list of the optional parameters with their default values.
        /// </param>
        private void DefineOptionalParameter(string[] optionalPerams)
        {
            CLIParser.optionalParameters = new Dictionary<string, string>();

            foreach (string param in optionalPerams)
            {
                string[] tokens = param.Split('=');

                if (tokens.Length != 2)
                {
                    string ERRORMessage = "ERROR: The optional command line parameter '" + param + "' has wrong format.\n Expeted param=value.";
                    account.Log(new ErrorTextInformation(ERRORMessage), 0);
                }

                tokens[0] = tokens[0].Trim();
                if (string.IsNullOrEmpty(tokens[0]))
                {
                    string ERRORMessage = "ERROR: The optional command line parameter '" + param + "' has empty name.";
                    account.Log(new ErrorTextInformation(ERRORMessage), 0);
                }

                tokens[1] = tokens[1].Trim();
                if (string.IsNullOrEmpty(tokens[1]))
                {
                    string ERRORMessage = "ERROR: The optional command line parameter '" + param + "' has no value.";
                }

                CLIParser.optionalParameters.Add(tokens[0], tokens[1]);
            }
        }

        /// <summary>
        /// Define the optional parameters. The parameters must be provided with their
        /// default values.
        /// </summary>
        /// <param name="optionalParameters">
        /// The list of the optional parameters with their default values.
        /// </param>
        private void DefineOptionalParameter(KeyValuePair<string, string>[] optionalParameters)
        {
            CLIParser.optionalParameters = new Dictionary<string, string>();

            foreach (KeyValuePair<string, string> param in optionalParameters)
            {
                string key = param.Key;
                key = key.Trim();

                string value = param.Value;
                value = value.Trim();

                if (string.IsNullOrEmpty(key))
                {
                    string ERRORMessage = "ERROR: The name of the optional parameter '" + param.Key + "' is empty.";
                    account.Log(new ErrorTextInformation(ERRORMessage), 0);
                }

                if (string.IsNullOrEmpty(value))
                {
                    string ERRORMessage = "ERROR: The value of the optional parameter '" + param.Key + "' is empty.";
                    account.Log(new ErrorTextInformation(ERRORMessage), 0);
                }

                CLIParser.optionalParameters.Add(param.Key, param.Value);
            }
        }

        /// <summary>
        /// Defines the supported command line switches. Switch is a parameter
        /// without value. When provided it is used to switch on a given feature or
        /// functionality provided by the application. For example a switch for tracing.
        /// </summary>
        /// <param name="switches"></param>
        private void DefineSwitches(string[] switches)
        {
            CLIParser.switches = new Dictionary<string, bool>(switches.Length);

            foreach (string sw in switches)
            {
                string temp = sw;
                temp = temp.Trim();

                if (string.IsNullOrEmpty(temp))
                {
                    string ERRORMessage = "ERROR: The switch '" + sw + "' is empty.";
                    account.Log(new ErrorTextInformation(ERRORMessage), 0);
                }

                CLIParser.switches.Add(temp, false);
            }
        }

        /// <summary>
        /// Parse the command line arguments.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        private void ParseArguments(string commandLine)
        {
            List<string> paramsToSplitInCL = requiredParameters.Keys.ToList();
            paramsToSplitInCL.AddRange(optionalParameters.Keys.ToList());
            if (switches != null)
            {
                paramsToSplitInCL.AddRange(switches.Keys.ToList());
            }
            paramsToSplitInCL = paramsToSplitInCL.Select(param => "(" + param + ")").ToList();
            string regex = String.Join("|", paramsToSplitInCL);
            string[] splitedParams = Regex.Split(commandLine, regex);
            List<string> paramsList = splitedParams.Where(item => !String.IsNullOrEmpty(item))
                .Select(item2 => item2.TrimStart())
                .Select(item3 => item3.TrimEnd()).ToList();
            Dictionary<string, string> passedCommands = new Dictionary<string, string>();
            for (int i = 0; i < paramsList.Count; i++)
            {
                if (paramsList.Count == 1)
                {
                    passedCommands.Add(paramsList[0], String.Empty);
                }
                else
                {
                    passedCommands.Add(paramsList[0], paramsList[1]);
                    paramsList.RemoveAt(0);
                }
                paramsList.RemoveAt(0);
            }

            rawArguments = passedCommands;

            missingRequiredParameters = new List<string>();
            missingValue = new List<string>();

            ParseRequiredParameters();
            ParseOptionalParameters();
            ParseSwitches();

            rawArguments.Clear();

            ThrowIfERRORs();
        }

        /// <summary>
        /// Returns the value of the specified parameter.
        /// </summary>
        /// <param name="paramName">The name of the perameter.</param>
        /// <returns>The value of the parameter.</returns>
        private string GetParamValue(string paramName)
        {
            string paramValue = string.Empty;

            if (requiredParameters.ContainsKey(paramName))
            {
                paramValue = requiredParameters[paramName];
            }
            else if (optionalParameters.ContainsKey(paramName))
            {
                paramValue = optionalParameters[paramName];
            }
            else
            {
                string ERRORMessage = "ERROR: The parameter '" + paramName + "' is not supported.";
                account.Log(new ErrorTextInformation(ERRORMessage), 0);
            }

            return paramValue;
        }

        private bool IsSwitchOn(string switchName)
        {
            bool switchValue = false;

            if (switches.ContainsKey(switchName))
            {
                switchValue = switches[switchName];
            }
            else
            {
                string ERRORMessage = "ERROR: switch '" + switchName + "' not supported.";
                account.Log(new ErrorTextInformation(ERRORMessage), 0);
            }

            return switchValue;
        }

        private void ParseRequiredParameters()
        {
            if (CLIParser.requiredParameters == null
                || CLIParser.requiredParameters.Count == 0)
            {
                return;
            }

            List<string> requiredParams = new List<string>(CLIParser.requiredParameters.Keys);

            foreach (string reqPar in requiredParams)
            {
                if (!rawArguments.Keys.Contains(reqPar))
                {
                    missingRequiredParameters.Add(reqPar);
                }
                else
                {
                    if (String.IsNullOrEmpty(rawArguments[reqPar]))
                    {
                        missingValue.Add(reqPar);
                    }
                    else
                    {
                        requiredParameters[reqPar] = rawArguments[reqPar];
                    }
                }
            }
        }

        private void ParseOptionalParameters()
        {
            if (CLIParser.optionalParameters == null || CLIParser.optionalParameters.Count == 0)
            {
                return;
            }

            List<string> optionalParams = new List<string>(CLIParser.optionalParameters.Keys);

            foreach (string paramName in optionalParams)
            {
                if (rawArguments.Keys.Contains(paramName))
                {
                    if (String.IsNullOrEmpty(rawArguments[paramName]))
                    {
                        missingValue.Add(paramName);
                    }
                    else
                    {
                        optionalParameters[paramName] = rawArguments[paramName];
                    }
                }
            }
        }

        private void ParseSwitches()
        {
            if (CLIParser.switches == null || CLIParser.switches.Count == 0)
            {
                return;
            }

            List<string> paramNames = new List<string>(CLIParser.switches.Keys);

            foreach (string paramName in paramNames)
            {
                if (rawArguments.Keys.ToList().Contains(paramName))
                {
                    CLIParser.switches[paramName] = true;
                    rawArguments.Remove(paramName);
                }
            }
        }

        private void ThrowIfERRORs()
        {
            StringBuilder ERRORMessage = new StringBuilder();

            if (missingRequiredParameters.Count > 0 || missingValue.Count > 0 || rawArguments.Count > 0)
            {
                ERRORMessage.Append("ERROR: Processing Command Line Arguments\n");
            }

            if (missingRequiredParameters.Count > 0)
            {
                ERRORMessage.Append("Missing Required Parameters\n");
                foreach (string missingParam in missingRequiredParameters)
                {
                    ERRORMessage.Append("\t" + missingParam + "\n");
                }
            }

            if (missingValue.Count > 0)
            {
                ERRORMessage.Append("Missing Values\n");
                foreach (string value in missingValue)
                {
                    ERRORMessage.Append("\t" + value + "\n");
                }
            }

            if (rawArguments.Count > 0)
            {
                ERRORMessage.Append("Unknown Parameters");
                foreach (string unknown in rawArguments.Keys)
                {
                    ERRORMessage.Append("\t" + unknown + "\n");
                }
            }

            if (ERRORMessage.Length > 0)
            {
                account.Log(new ErrorTextInformation(ERRORMessage.ToString()), 0);
            }
        }

        private string[] DeleteCommand(string[] cmd)
        {
            List<string> args = cmd.ToList();
            args.RemoveAt(0);
            return args.ToArray();
        }

        /// <summary>
        /// Returns the help of a given command, or the general help if no command is set.
        /// </summary>
        /// <param name="cmd">The name of the command.</param>
        /// <returns>The general help/the command help</returns>
        private static List<string> Usage(string cmd = "")
        {
            List<string> ls = new List<string>() { "USAGE:" };
            switch (cmd)
            {
                case "":
                    ls.Add("/command -arg1 -arg2 ... -argn Value -switch1");
                    ls.Add("");
                    ls.Add("Below are the available commands. Type /help with the name of the command for a specific help.");
                    ls.Add("  - move");
                    ls.Add("  - cell");
                    ls.Add("  - say");
                    ls.Add("  - entities");
                    ls.Add("  - mapid");
                    ls.Add("  - cellid");
                    ls.Add("  - path");
                    ls.Add("  - fight");
                    ls.Add("EXAMPLE:");
                    ls.Add("1. > /help;move");
                    ls.Add("   - Display the help of the move command.");
                    return ls;
                case "move":
                    ls.Add("/move [-cell <int>] [-dir <string>]");
                    ls.Add("Move to a cell and/or a direction.");
                    ls.Add("OPTIONS:");
                    ls.Add("  - cell: move to the specified cell.");
                    ls.Add("  - dir : move to the specified direction (right, left, bottom or up).");
                    ls.Add("EXAMPLE:");
                    ls.Add("1. > /move -cell 150");
                    ls.Add("   - Move to the cell 150.");
                    ls.Add("");
                    ls.Add("2. > /move -cell 150 -dir right");
                    ls.Add("   - Move to the cell 150 and then move to the map at the right");
                    return ls;
                case "cell":
                    ls.Add("/cell [-npc <int>] [-elem <int>] [-player <string>]");
                    ls.Add("Get the cell of an element.");
                    ls.Add("OPTIONS:");
                    ls.Add("  - npc: Get the cell of the specified npc id.");
                    ls.Add("  - elem : Get the cell of the specified element id.");
                    ls.Add("  - player : Get the cell of the player name.");
                    ls.Add("EXAMPLE:");
                    ls.Add("1. > /cell -npc 10001");
                    ls.Add("   - Get the cell of the npc which has the id 10001.");
                    ls.Add("");
                    ls.Add("2. > /cell -player Mage");
                    ls.Add("   - Get the cell of the player named Mage.");
                    return ls;
                case "say":
                    ls.Add("/say -c <char> -m <string> [-dest <string>]");
                    ls.Add("Say something in the chat");
                    ls.Add("OPTIONS:");
                    ls.Add("  - c   : Canal where the message will be displayed (ex: s for general canal).");
                    ls.Add("  - m : Message that will be sent.");
                    ls.Add("  - dest    : The dest. player of the message. Only on private message (-canal w)");
                    ls.Add("EXAMPLE:");
                    ls.Add("1. > /say -c b -m I/sell/some/things/pm/me");
                    ls.Add("   - Send the message \"I sell some things pm me \"in the in the business canal.");
                    ls.Add("");
                    ls.Add("2. > /say -c w -m hi -dest Mage");
                    ls.Add("   - Send the private message \"hi\" to the player named Mage.");
                    return ls;
                case "entities":
                    ls.Add("/entities [-v]");
                    ls.Add("Display informations about entities on the map.");
                    ls.Add("OPTIONS:");
                    ls.Add("  - v   : Display a more verbose output.");
                    ls.Add("EXAMPLE:");
                    ls.Add("1. > /entities");
                    ls.Add("   - Display informations about entities on the map.");
                    ls.Add("");
                    ls.Add("2. > /entities -v");
                    ls.Add("   - Display detailed informations about entities on the map");
                    return ls;
                case "mapid":
                    ls.Add("/mapid");
                    ls.Add("Returns the current mapid.");
                    return ls;
                case "cellid":
                    ls.Add("/cellid");
                    ls.Add("Returns the current player's cellid.");
                    return ls;
                case "path":
                    ls.Add("/path [-start] [-stop] [-load <string>] [-name <string>]");
                    ls.Add("Interface to manage path.");
                    ls.Add("OPTIONS:");
                    ls.Add("  - start   : Start the path.");
                    ls.Add("  - stop    : Stop the path.");
                    ls.Add("  - load    : Load the path in the specified file.");
                    ls.Add("  - name    : [To use with load] Specify the loaded path's name. Default is Unknown");
                    ls.Add("EXAMPLE:");
                    ls.Add("1. > /path -load C:\\Users\\Mage\\path.txt -name MyPath");
                    ls.Add("   - Load the path \"path.txt\" located in Mage's folder, and shows it as \"MyPath\".");
                    ls.Add("");
                    ls.Add("2. > /path -start");
                    ls.Add("   - Start the current loaded path.");
                    return ls;
                case "fight":
                    ls.Add("/fight [-launch] [-l] [-lock] [-me] [-v] [-t]");
                    ls.Add("Interface to manage fights.");
                    ls.Add("OPTIONS:");
                    ls.Add("  - launch  : Research for a fight on the map.");
                    ls.Add("  - l   : List all fighters.");
                    ls.Add("  - v   : Display a more verbose output.");
                    ls.Add("  - me   : Display informations about you in the fight.");
                    ls.Add("  - t   : Display the current turn number.");
                    ls.Add("  - lock   : Lock the fight.");
                    ls.Add("EXAMPLE:");
                    ls.Add("1. > /fight -me -l -v -t");
                    ls.Add("   - Display a verbose output with the informations about all fighters and display the current turn.");
                    ls.Add("");
                    ls.Add("2. > /fight -launch");
                    ls.Add("   - Research and launch a fight on the map.");
                    return ls;
                case "gather":
                    ls.Add("/gather [-launch] [-stats]");
                    ls.Add("Interface to manage gather.");
                    ls.Add("OPTIONS:");
                    ls.Add("  - launch  : Perform gathering on the map.");
                    ls.Add("EXAMPLE:");
                    ls.Add("1. > /gather -launch");
                    ls.Add("   - Launch the gathering");
                    return ls;
            }
            return ls = new List<string>() { "" };
        }

        /// <summary>
        /// Do the moving action.
        /// </summary>
        /// <returns>A display string of the action</returns>
        private async Task<List<string>> Move()
        {
            int cell = Int32.Parse(GetParamValue("-cell"));
            string dir = GetParamValue("-dir");

            try
            {
                if (cell != 0)
                {
                    result.Add("Déplacement vers la cellid : " + cell);
                    await account.Map.MoveToCell(cell);

                }

                if (dir != "null" && new List<string>() { "bottom", "up", "left", "right" }.Contains(dir))
                {
                    result.Add("Déplacement : " + dir);
                    account.Map.ChangeMap(dir);
                }
            }
            catch (Exception ex)
            {
                result.Add("[ERROR] " + ex.Message + "\n");
                return result;
            }
            if (!(result.Count > 0))
                return Usage("move");
            else
                return result;
        }

        /// <summary>
        /// Get the cell of a specified element.
        /// </summary>
        /// <returns>The cell of the specified element.</returns>
        private List<string> Cell()
        {
            int npcId = int.Parse(GetParamValue("-npc"));
            int elemid = Int32.Parse(GetParamValue("-elem"));
            string player = GetParamValue("-player");

            try
            {
                if (npcId != 0)
                {
                    string name = account.Npc.GetNpcName(npcId);
                    int cell = account.MapData.GetCellFromContextId(npcId);
                    if (cell != 0)
                        result.Add("Le pnj " + name + " est à la cellule " + cell + ". \n");
                    else
                        result.Add("Pnj introuvable.");
                }
                else if (elemid != 0)
                {
                    int cell = account.MapData.GetCellFromContextId(elemid);
                    if (cell != 0)
                        result.Add("L'élement " + elemid + " est à la cellule " + cell + ". \n");
                    else
                        result.Add("Cellule introuvable.");
                }
                else if (player != "null")
                {
                    int cell = 0;
                    foreach (GameRolePlayCharacterInformations p in account.MapData.Players)
                    {
                        if (p.Name == player)
                        {
                            cell = account.MapData.GetCellFromContextId(p.ContextualId);
                        }
                    }
                    if (cell != 0)
                        result.Add("Le joueur " + player + " est à la cellule " + cell + ". \n");
                    else
                        result.Add("Joueur introuvable.");
                }
            }
            catch (Exception ex)
            {
                result.Add("[ERROR] " + ex.Message + "\n");
                return result;
            }
            if (!(result.Count > 0))
                return Usage("cell");
            else
                return result;
        }

        /// <summary>
        /// Say a message in the specified canal.
        /// </summary>
        /// <returns>The result.</returns>
        private List<string> Say()
        {
            char canal = char.Parse(GetParamValue("-c"));
            string message = GetParamValue("-m").Replace('/', ' ');
            string dest = GetParamValue("-dest");

            try
            {
                if (canal == 'w' && dest != "null")
                {
                    using (BigEndianWriter writer = new BigEndianWriter())
                    {
                        ChatClientPrivateMessage msg = new ChatClientPrivateMessage(message, dest);
                        msg.Serialize(writer);
                        writer.Content = account.HumanCheck.Hash_function(writer.Content);
                        msg.Pack(writer);
                        account.SocketManager.Send(writer.Content);
                        result.Add("à " + dest + " : " + message + "\n");
                        account.Log(new DebugTextInformation("[SND] 851 (ChatClientPrivateMessage)"), 0);
                    }
                }
                else
                {
                    switch (canal)
                    {
                        case 'g':
                            account.Config.Flood.SendMessage(2, message);
                            result.Add("Message envoyé. \n");
                            break;
                        case 'r':
                            account.Config.Flood.SendMessage(6, message);
                            result.Add("Message envoyé. \n");
                            break;
                        case 'b':
                            account.Config.Flood.SendMessage(5, message);
                            result.Add("Message envoyé. \n");
                            break;
                        case 'a':
                            account.Config.Flood.SendMessage(3, message);
                            result.Add("Message envoyé. \n");
                            break;
                        case 's':
                            account.Config.Flood.SendMessage(0, message);
                            result.Add("Message envoyé. \n");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Add("[ERROR] " + ex.Message + "\n");
                return result;
            }
            if (!(result.Count > 0))
                return Usage("say");
            else
                return result;
        }

        /// <summary>
        /// Display informations about the entities
        /// </summary>
        /// <returns>The informations</returns>
        private List<string> DispEntities()
        {
            bool verbose = IsSwitchOn("-v");

            try
            {
                foreach (GameRolePlayNpcInformations n in account.MapData.Npcs)
                {
                    string name = account.Npc.GetNpcName(n.NpcId);
                    int cell = account.MapData.GetCellFromContextId(n.ContextualId);
                    result.Add("[PNJ] " + name + " -> id : +" + n.NpcId + " contextual id : " + n.ContextualId + " cell : " + cell + ".");

                }
                foreach (KeyValuePair<MageBot.Core.Map.Elements.InteractiveElement, int> pair in account.MapData.InteractiveElements)
                {
                    if (verbose)
                        result.Add("[INTERACTIVE ELEMENT] " + pair.Key.Name + " -> id " + pair.Key + " cell : " + pair.Value + " IsUsable : " + pair.Key.IsUsable + ".");
                    else
                        result.Add("[INTERACTIVE ELEMENT] " + pair.Key.Name + " -> id " + pair.Key + " cell : " + pair.Value + ".");
                }
                foreach (GameRolePlayCharacterInformations p in account.MapData.Players)
                {
                    int cell = account.MapData.GetCellFromContextId(p.ContextualId);
                    if (verbose)
                        result.Add("[PLAYER] " + p.Name + " -> contextual id " + p.ContextualId + " cell : " + cell + " Sex : " + (p.HumanoidInfo.Sex ? "Female" : "Male") + ".");
                    else
                        result.Add("[PLAYER] " + p.Name + " -> contextual id " + p.ContextualId + " cell : " + cell + ".");

                }
                foreach (MonsterGroup m in account.MapData.Monsters)
                {
                    if (verbose)
                        result.Add("[MONSTERS] " + " -> (" + m.monstersLevel + ") " + m.monstersName(true) + " contextual id " + m.m_contextualId + " cell : " + m.m_cellId + ".");
                    else
                        result.Add("[MONSTERS] " + " -> " + m.monstersName(false) + " contextual id " + m.m_contextualId + ".");
                }

            }
            catch (Exception ex)
            {
                result.Add("[ERROR] " + ex.Message + "\n");
                return result;
            }
            if (!(result.Count > 0))
                return Usage("entities");
            else
                return result;
        }

        /// <summary>
        /// Interface to manage path
        /// </summary>
        private List<string> Path()
        {
            string path = GetParamValue("-load");
            string name = GetParamValue("-name");
            bool start = IsSwitchOn("-start");
            bool stop = IsSwitchOn("-stop");

            try
            {
                if (path != "null")
                {
                    account.Config.Path = new PathManager(account, path, name);
                    account.Config.Path.StopPath();
                    result.Add("Trajet chargé : " + name);
                }
                if (start)
                {
                    account.Config.Path.Start();
                    //account.Path.ParsePath();
                    result.Add("Lancement du trajet.");
                }
                if (stop)
                {
                    account.Config.Path.StopPath();
                    result.Add("Arrêt du trajet...");
                }
            }
            catch (Exception ex)
            {
                result.Add("[ERROR] " + ex.Message + "\n");
                return result;
            }
            if (!(result.Count > 0))
                return Usage("path");
            else
                return result;
        }

        /// <summary>
        /// Interface to manage fights
        /// </summary>
        private async Task<List<string>> Fight()
        {
            bool launch = IsSwitchOn("-launch");
            bool Lock = IsSwitchOn("-lock");
            bool fighters = IsSwitchOn("-l");
            bool verbose = IsSwitchOn("-v");
            bool turn = IsSwitchOn("-t");
            bool me = IsSwitchOn("-me");
            bool infinite = IsSwitchOn("-i");
            try
            {
                if (turn && account.State == Status.Fighting)
                {
                    result.Add("[TURN] " + account.FightData.TurnId);
                }
                if (fighters && account.State == Status.Fighting)
                {
                    foreach (BFighter f in account.FightData.Fighters)
                    {
                        if (verbose)
                        {
                            bool isAlly = f.TeamId == account.FightData.Fighter.TeamId;
                            result.Add(String.Format("[{1}] LP : {2}/{3} AP:{4} MP:{5} Cell: {6} Alive ? {7}",
                                isAlly ? "ALLY" : "ENNEMY", f.LifePoints, f.MaxLifePoints,
                                f.ActionPoints, f.MovementPoints, f.CellId, f.IsAlive));
                        }
                        else
                        {
                            bool isAlly = f.TeamId == account.FightData.Fighter.TeamId;
                            result.Add(String.Format("[{1}] LP : {2}/{3} Alive ? {4} ",
                                isAlly ? "ALLY" : "ENNEMY", f.LifePoints, f.MaxLifePoints, f.IsAlive));
                        }
                    }
                }
                if (me && account.State == Status.Fighting)
                {
                    BFighter m = account.FightData.Fighter;
                    if (verbose)
                        result.Add(String.Format("[ME] LP : {1}/{2} AP:{3} MP:{4} Cell: {5} Alive ? {6}", m.LifePoints, m.MaxLifePoints
                            , m.ActionPoints, m.MovementPoints, m.IsAlive));
                    else
                        result.Add(String.Format("[ME] LP : {1}/{2} Alive ? {3} \n", m.LifePoints, m.MaxLifePoints, m.IsAlive));
                }
                if (account.Fight != null && (launch || Lock))
                {
                    if (launch && account.State != Status.Fighting)
                    {
                        account.Fight.Infinite = infinite;
                        await account.Fight.SearchFight();
                        result.Add("Recherche d'un combat...");
                    }
                    if (Lock && account.FightData.WaitForReady)
                    {
                        account.Fight.LockFight();
                        result.Add("Fermeture du combat.");
                    }
                }
                else
                    throw new Exception("L'utilisation de cette commande nécessite d'avoir une IA chargée.");
            }
            catch (Exception ex)
            {
                result.Add("[ERROR] " + ex.Message + "\n");
                return result;
            }
            if (!(result.Count > 0))
                return Usage("fight");
            else
                return result;
        }

        /// <summary>
        /// Interface to manage gather.
        /// </summary>
        private List<string> Gather()
        {
            bool launch = IsSwitchOn("-launch");
            bool stats = IsSwitchOn("-stats");
            try
            {
                if (launch)
                {
                    account.PerformGather();
                    result.Add("Récolte de la map...");
                }
                if (stats)
                {
                    Dictionary<string, int> newStats = account.Gather.Stats;
                    foreach (KeyValuePair<string, int> key in newStats)
                    {
                        result.Add(String.Format("[{1}] : {2}", key.Key, key.Value));
                    }
                }
            }
            catch (Exception ex)
            {
                result.Add("[ERROR] " + ex.Message + "\n");
                return result;
            }
            if (!(result.Count > 0))
                return Usage("gather");
            else
                return result;
        }
        #endregion
    }





}
