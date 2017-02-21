#define dev
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfinityScript;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Globalization;
using noKnife;
using MemHax;
using System.Threading;

namespace SinAdminv4
{

    public class SinAdmin : BaseScript
    {
        public static string sincfgname = @"scripts\\sinadmin";
        public static string cfgname = InfinityScript.Function.Call<string>("getdvar", "sv_config");
        public static string adminpath = @"scripts\\sinadmin\\admins.cfg";
        public static string cfgpath = @"scripts\\sinadmin\\" + cfgname +"\\sinconfig.cfg";

        // Ban message, temp message, kick message, warn message, unwarn message, connect message
        public static string bm, tm, km, wm, um, cm;
        // Admin list
        public static string[] al;
        // Warn limit, warn count
        public static int wl, wc;
        // Bot name replaces generic "console" bot, pm is used as bot for all public messages
        public static string bot, pm;
        public static string banbot;

        // Admins in this list are used to echo all commands to if chatspy is turned on in config
        public static List<Entity> Admins = new List<Entity>();
        //Useless fucking list
        //public static List<Entity> Dead = new List<Entity>();
        // DSPL file used to change game mode
        public static string dspl;
        // Custom name of the axis team
        //public static string AxisName;
        // Custom name of the allies team
       // public static string AlliesName;
        // A command list generated each time a player attemps to use a command, used to check if a player is allowed to use the command
        public static List<string> commlist = new List<string>();
        // GUIDs which cannot be warned, kicked, banned, tempbanned, or have rcon used against
        public static List<long> immuneguid = new List<long>();
        // Contains player names and their aliases in the format: [player]=[alias]
        public static List<string> aliasplayers = new List<string>();
        // Used to keep track of which players have a chat alias
        public static List<string> aliasplayersv2 = new List<string>();
        // List used to keep track of which players have hitmarkergod enabled
        public static List<long> hitmarkergods = new List<long>();
        // Hitmarker gamemode list (fuck this one, not my idea)
        public static List<long> hitmarkergmlist = new List<long>();
        // List of admins to display when the !admins command is called
        public static List<string> admindisplaylist = new List<string>();
        public static List<string> admindisplaylistV2 = new List<string>();
        // IDK why but it shows to the sinscript config file
       // public static string adminlistpath;
        // Path for muted players file
        public static string mutepath;
        // List of muted players
        public static List<long> mutedplayers = new List<long>();
        // List of blocked command players
        public static List<long> blockedplayers = new List<long>();
        // Max server clients
        public static int maxclients = 18;
     //   public static int reservedslots = 0;

        public static Entity gypsy = null;
        public static bool Faller = false;
        public static bool Bio = false;

        public static bool aliasname = false;

        private bool interval1 = false, interval2 = false;

        // Variable blanked and re-used to check if a player is immune
        public static string immuneplayer;
        // Kickmessage blanked and re-used when a player is kicked. Is also used for any other command that is > 2 words
        public static string kickmessage;
        // Blanked and re-used. Stores that players guid
        public static long guid;
        // Useless piece of shit (bio is sleepy)
        public static string messages;
        // Bool is set to false every time a message is said, will be set to true if aliasplayers list contains the player name
        public static bool showalias;
        // Temporary variable to store the player alias
        public static string stringalias;
        // I don't even know what I was thinking
        public static string mes1;
        // Admin list to display (fucking glitches, man)
        public static string todisplay;
        // Initial health of player to re-set to 
        public static int defaultHealth = 100;
        // Clantag is used for the !clanvsall command
        public static string clantag;
        // Int for timed messages
        public static int timeint;

        // Path to track players name changed
        public static string nametrack;
        public static string _dspl = "";
        public static string errorpath = @"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\errorLog.txt";
        public static string dsplpath = @"admin\\SinXNextMap.dspl";

        public static FileStream fs;
        public static string badpath;
        public static FileStream bs;
        public static FileStream IP;
        public static FileStream ipd;
        public static FileStream swit;
        public FileStream fark;
        // Bad names to kick players
        public static List<string> badnames = new List<string>();

        //public static string websiteName;

        public static string serverName;

        public static Entity[] entCN = new Entity[17];

        public static List<Entity> LoggedIn = new List<Entity>();

        //public static float MaxScopeTime = 0.2f;

        //public static double maxhalftime = 0.4;

        //Vote shit
        public static bool voting = false;
        public static bool inprogress = false;
        public static string votecommands = "";
        public double votethreshold = 100.0;
        public static List<Entity> yesvotes = new List<Entity>();
        public static string votecommand = "";
        public Entity votecaller;
        public static bool justvoted = false;
        public static bool cancelvote = false;
        public static bool forcevote = false;
        public static bool AntiHax = false;
        public static bool blockallads = false;

        //AntiCamp||SpawnProtection
        public static List<Entity> spawned = new List<Entity>();
        public static List<Entity> _safeTriggers = new List<Entity>();
        public static List<Entity> doneAnti = new List<Entity>();
        public static bool _prematch = false;


       // public static string alliesIco = "";
        //public static string axisIco = "";

        public static List<Entity> gainedaccess = new List<Entity>();
        // Decides the action taken when a bad name is detected
       // public static string badnameaction = "kick";


        // speed shit idek why im still doing this fuck everything
       // double speed = 1;

        //TkMode
        //public static bool tk = false;
        public bool nextMapSet = false;
        // doesnt continue chat from unknown commands from overlords
      //  public static bool breakunknown = true;

        // Pretty self explanitory
        public static bool rpgbullet = false;
        public static string ammo = "";

        public static string mesRep = "";

        // Fancy team chat
       // public static bool ftc = true;

        public static List<string> blockedcommands = new List<string>();

        // Fuck streamwriters & bio
        //public StreamWriter writer2 = new StreamWriter("logs/cmds.log", true);
        //public StreamWriter writer3 = new StreamWriter("logs/chat.log", true);
        //public StreamWriter writer4 = new StreamWriter("logs/all.log", true);
        // Same shit here
        public static string globalpath;
        public static string globalpath2;
        public static string globalpath3;

        // Penis
        public static List<Entity> Axis = new List<Entity>();
        public static List<Entity> Allies = new List<Entity>();
        public static List<Entity> DeadAxis = new List<Entity>();
        public static List<Entity> DeadAllies = new List<Entity>();
        public static List<Entity> BotList = new List<Entity>();
        public static List<Entity> hasrecoil = new List<Entity>();
        public static string recoilpath;
        public static List<string> rlr = new List<string>();
        public static List<Entity> fuckdis = new List<Entity>();
        //Multi"" + "SinAdmin\\" + cfgname+ "\\" + cfgnameUse

        //LIST FOR CMD ALIAS
        string[] cmdAl;

        //Lines for Connect Admins
        string[] ablines;

        public bool usingaimbot = false;

        //Custom Dvar
        //public static string _cdvar;

        //AntiAim
        public static string headshots;
        public static string neckshots;
        public static string torso_upper;
        public static string torso_lower;
        public static string right_arm_upper;
        public static string right_arm_lower;
        public static string left_arm_upper;
        public static string left_arm_lower;
        public static string left_leg_upper;
        public static string left_leg_lower;
        public static string right_leg_upper;
        public static string right_leg_lower;

        //KILLSTREAK MESSAGES
        //public static int AmmKills = -1;
        //public static string ksMes;
        //public static string hsMes;

        //HUD
        //public static string POS;
        //public static string xCoord;
        //public static string yCoord;

        //Weapons list
        public static string[] AllWeapons /*credits to jaydi & yamato*/ = { "iw5_l96a1_mp", 
                                    "iw5_44magnum_mp",
                                    "iw5_usp45_mp",
                                    "iw5_deserteagle_mp",
                                    "iw5_mp412_mp",
                                    "iw5_g18_mp",
                                    "iw5_fmg9_mp",
                                    "iw5_mp9_mp",
                                    "iw5_skorpion_mp",
                                    "iw5_p99_mp",
                                    "iw5_fnfiveseven_mp",
                                    "rpg_mp",
                                    "iw5_smaw_mp",
                                    "stinger_mp",
                                    "javelin_mp",
                                    "xm25_mp",
                                    "iw5_usp45jugg_mp",
                                    "iw5_mp412jugg_mp",
                                    "iw5_m60jugg_mp",
                                    "iw5_riotshieldjugg_mp",
                                    "iw5_m4_mp",
                                    "riotshield_mp",
                                    "iw5_ak47_mp",
                                    "iw5_m16_mp",
                                    "iw5_fad_mp",
                                    "iw5_acr_mp",
                                    "iw5_type95_mp",
                                    "iw5_mk14_mp",
                                    "iw5_scar_mp",
                                    "iw5_g36c_mp",
                                    "iw5_cm901_mp",
                                    "iw5_mp5_mp",
                                    "iw5_mp7_mp",
                                    "iw5_m9_mp",
                                    "iw5_p90_mp",
                                    "iw5_pp90m1_mp",
                                    "iw5_ump45_mp",
                                    "iw5_barrett_mp",
                                    "iw5_rsass_mp",
                                    "iw5_dragunov_mp",
                                    "iw5_msr_mp",
                                    "iw5_as50_mp",
                                    "iw5_ksg_mp",
                                    "iw5_1887_mp",
                                    "iw5_striker_mp",
                                    "iw5_aa12_mp",
                                    "iw5_usas12_mp",
                                    "iw5_spas12_mp",
                                    "iw5_m60_mp",
                                    "iw5_mk46_mp",
                                    "iw5_pecheneg_mp",
                                    "iw5_sa80_mp",
                                    "emp_grenade_mp",
                                    "iw5_mg36_mp", };
        //COUNTRY MESSAGE
        public System.Collections.ArrayList ContinentList;
        public Hashtable Continents = new Hashtable();
        public Hashtable CountryNameToISO = new Hashtable();
        public IPToCountry Ip2Country = new IPToCountry();
        public Dictionary<string, string> IsoCountries = new Dictionary<string, string>();
        public Hashtable ISOToCountryName = new Hashtable();

        // List of all active players in the server. DC = player removed, C = player added
        private List<Entity> Playerz = new List<Entity>();
        public SinAdmin()
        {
            sinSet();
  
                    //StreamWriter erros = new StreamWriter(fs);

                        Log.Write(LogLevel.All, "Server cfgname: " + cfgname + " :: SinXClan.com :: SinAdmin v4.0");
                        setDvars();
                        init();
                        serverName = Call<string>("getdvar", "sv_hostname");
                        // Begins timed messages
                        timedmessages();
                        notifys();
                        loadbadnames();
                        //Action taken when a player is connecting (see connecting)
                        PlayerConnecting += new Action<Entity>(connecting);
                        // Action taken on player connected (see C)
                        PlayerConnected += new Action<Entity>(c);
                        // Action taken on player connected (see DC)
                        PlayerDisconnected += new Action<Entity>(dc);

                        //rcon pipe = new rcon();

                        if (File.Exists(@"scripts\\SinUpdater.exe"))
                        {
                            Process.Start(@"scripts\\SinUpdater.exe");
                            Log.Write(LogLevel.Info, "Updater running...");
                        }

               
        }

        #region Config
        public void sinSet()
        {
            if (!Directory.Exists(sincfgname))
            {
                Directory.CreateDirectory(sincfgname);
            }
            if (!File.Exists(@"scripts\\sinadmin\\" + cfgname + "\\logs\\Fuckingspeciallogsforinit.txt"))
            {
                File.Create(@"scripts\\sinadmin\\" + cfgname + "\\logs\\Fuckingspeciallogsforinit.txt");
            }
            fark = new FileStream(@"scripts\\sinadmin\\" + cfgname + "\\logs\\Fuckingspeciallogsforinit.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            if (!Directory.Exists(cfgname))
            {
                Directory.CreateDirectory(cfgname);
            }

            string[] cfgnames = {
                                   @"scripts\" + "SinAdmin\\" + cfgname + "\\logs",
                                   @"scripts\" + "SinAdmin\\" + cfgname + "\\promod",
                                   @"scripts\" + "SinAdmin\\" + cfgname + "\\ScriptFiles"
                               };

            string[] files = {
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\promod\\0.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\promod\\1.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\promod\\2.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\promod\\3.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\promod\\4.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\promod\\5.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\promod\\6.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\promod\\7.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\promod\\8.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\promod\\9.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\promod\\10.txt",

                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\Sv_name.cfg",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\badnames.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\blocked.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\bots.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\chataliases.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\ChatReports.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\currentwarns.warn",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\errorLog.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\FuckRogue.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\GroupWelcome.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\ipban.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\killstreak.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\LoggedIn.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\muted.mute",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\nametracker.track",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\recoil.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\PlayCount.txt",
                                 "scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\cmdalias.txt"
                             };

            foreach (string s in cfgnames)
            {
                if (!Directory.Exists(s))
                {
                    Directory.CreateDirectory(s);
                }
            }

            foreach (string s in files)
            {
                if (!File.Exists(s))
                {
                    File.WriteAllBytes(s, new byte[0]);
                }
            }
      
            string ippath = @"scripts\\" + "SinAdmin\\ipbans.txt";
            string ippaths = @"scripts\\" + "SinAdmin\\ipban.txt";
            fs = new FileStream(errorpath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            badpath = @"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\badnames.txt";
            recoilpath = @"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\recoil.txt";
            dsplpath = @"admin\\SinXNextMap.dspl";
            ipd = new FileStream(ippath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            bs = new FileStream(badpath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            IP = new FileStream(ippaths, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            mutepath = @"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\muted.mute";
    
            nametrack = @"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\nametracker.track";
            ablines = File.ReadAllLines(adminpath);
            swit = new FileStream(@"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\isSwitch.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            globalpath = @"scripts\\" + "SinAdmin\\" + cfgname + "\\logs\\cmds.log";
            globalpath2 = @"scripts\\" + "SinAdmin\\" + cfgname + "\\logs\\chat.log";
            globalpath3 = @"scripts\\" + "SinAdmin\\" + cfgname + "\\logs\\all.log";
            File.Delete(@"scripts\\" + "SinAdmin\\ver.txt");
            StreamWriter nublike = new StreamWriter((@"scripts\\" + "SinAdmin\\ver.txt"));
            nublike.WriteLine("V3.3");
            nublike.Close();

            if (!File.Exists(@"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\antiaim.cfg"))
            {
                string[] derpson = {
                                        "[headshots]=0",
                                        "[neckshots]=0",
                                        "[torso_upper]=0",
                                        "[torso_lower]=0",
                                        "[right_arm_upper]=0",
                                        "[right_arm_lower]=0",
                                        "[left_arm_upper]=0",
                                        "[left_arm_lower]=0",
                                        "[left_leg_upper]=0",
                                        "[left_leg_lower]=0",
                                        "[right_leg_upper]=0",
                                        "[right_leg_lower]=0"
                                    };
                File.WriteAllLines(@"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\antiaim.cfg", derpson);
            }



            if (!File.Exists(@"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\rules.txt"))
            {
                //File.WriteAllBytes(@"scripts\\" +"SinAdmin\\" + cfgname + "\\ScriptFiles\\rules.txt", new byte[0]);
                StreamWriter imLeTired = new StreamWriter(@"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\rules.txt", true);
                imLeTired.WriteLine("rule1");
                imLeTired.WriteLine("rule2");
                imLeTired.WriteLine("rule3");
                imLeTired.Close();
            }


            if (!File.Exists(adminpath))
            {
                string[] adminConf = 
                      {
                    "[ADMINS]:",
                    "[AdminList]",
                    "[Overlord];:;-2222222222,-1111111111,0000000000",
                    "[Admin];:;-1111111111,0000000000,1111111111",
                    "[Moderator];:;0000000000,1111111111,2222222222",
                    Environment.NewLine,
                    "[Overlord=Commands=[^1MA^7]];:;*ALL*,ban,tmpban,kick,warn,unwarn,map,mode,setteam,afk,add,remove,reloadsinscript,admins,@admins,rcon,balance,res,pm,rotate",
                    "[Admin=Commands=[^3A^7]];:;admins,@admins,ban,kick,tmpban,warn,unwarn,map,admins,pm,afk",
                    "[Moderator=Commands=[^7MOD^7]];:;admins,tmpban,kick,warn,unwarn,admins,pm,afk",
                    "[User=Commands=];:;admins,afk,pm",
                    "[SECURITY]:",
                    "[UseLogin];:;1",
                    "[Login=Overlord];:;SonOfAGypsy",
                    "[Login=Admin];:;TeknoSlave",
                    "[Login=Moderator];:;abc123",
                    "[WarnLimit];:;3",
                    "[BadNameAction];:;tmpban",
                    "[NameCharacterFilter];:;1",
                    "[AliasWithName];:;0",
                    "[BlockUnknownCommands];:;0",
                    "[BlockOverlordAdding];:;0",
                    "[BlockedCommands];:;aimbot,wallhack,invisible",
                    "[AutoIPBan];:;1",
                    "[MemberProtect];:;0",
                    "[Spy];:;1",
                    "[AntiHax];:;0",
                    "[ImmunePlayerz];:;",
                    Environment.NewLine,
               };
                File.WriteAllLines(adminpath, adminConf);
            }

            if (!File.Exists(cfgpath))
            {
                string[] newWrite = 
                {
                    "[MESSAGES]:",
                    "[BanMessage];:;^1<client> was banned by ^5<issuer> ^3Reason: <message>",
                    "[TempMessage];:;^1<client> was tmpbanned by ^5<issuer> ^3Reason: <message>",
                    "[KickMessage];:;^1<client> was kicked by ^5<issuer> ^3Reason: <message>",
                    "[WarnMessage];:;^1<client> was warned by ^5<issuer> ^3Reason: <message>. ^7Warning <count> of <limit>",
                    "[UnwarnMessage];:;^1<client> was unwarned by ^5<issuer> ^3Reason: <message>. ^7Warning <count> of <limit>",
                    "[BotName];:;Console: ^7",
                    "[PMBotName];:;^0[^5PM^0]^7 : ",
                    "[ConnectMessage];:;^5<rank> <client> Connected [<country>]. ^2Played <playcount> times.",
                    "[KillstreakMessage];:;^1<player> is on a ^5<ammount> ^0Killstreak!",
                    "[HeadShotMessage];:;^1[player>] killed ^5[<enemy>] by ^0[Headshot]!",
                    "[KillstreakAmount];:;5",
                    "[UseKillstreakMessages];:;0",
                    "[CustomICON];:;0",
                    "[AlliesIcon];:;",
                    "[AxisIcon];:;",
                    "[AlliesName];:;SinX",
                    "[AxisName];:;Gypsies",
                    "[Advertising];:;1",
                    Environment.NewLine,
                    "[FUN]:",
                    "[SpeedMultiplier];:;1.0",
                    "[UnlimitedAmmo];:;0",
                    "[tkmode];:;0",
                    "[BotMod];:;0",
                    "[ExplosiveBullets];:;0",
                    "[SnDKillstreak];:;1",
                    "[HitMarkerGM];:;0",
                    "[ClanvsAll];:;SinX",
                    "[ClanIdent];:;0",
                    "[CustomKillstreak];:;0",
                    "[Forgive];:;0",
                    Environment.NewLine,
                    "[VOTE]:",
                    "[Voting];:;1",
                    "[VoteCommands];:;kick,rotate,res",
                    "[VoteThreshold];:;60",
                    Environment.NewLine,                    
                    "[SERVER SETTINGS]:",
                    "[HUDAdv];:;1",
                      "[POSITION];:;TOPLEFT",
                        "[xCoord];:;20",
                          "[yCoord];:;160",
                    "[WebsiteName];:;^5SinX^0Clan.^1com",
                    "[DeadChat];:;1",
                    "[Promod];:;0",
                    "[CustomSvName];:;0",
                    "[CustomDvar];:;0",
                    "[HardcoreTeamKill];:;0",
                    "[ReservedSlots];:;1",
                     "[ModdedTeamChat];:;1",
                    "[DSPL];:;default",
                     Environment.NewLine,
                    "[ANTI's]:",
                    "[AntiHardscope];:;0",
                    "[EmptyPistolSecondary];:;0",
                    "[AntiSpray];:;0",
                    "[AntiPlant];:;0",
                    "[AntiNoScope];:;0",
                    "[AntiHalf];:;0",
                    "[AntiCrtk];:;1",
                    "[AntiHitmarker];:;0",
                    "[AntiBoltCancel];:;0",
                    "[MaxHalfTime];:;0.45",
                    "[Knife];:;1",
                    "[FallDamage];:;0",
                    "[AntiAimbot];:;1",
                    "[AntiNoRecoil];:;1",
                     "[RemoveColours];:;1",
                    "[AntiCamp];:;0",
                    "[AntiCampTime];:;4000",
                    "[SpawnProtection];:;0",
                    "[SpawnProtectionTime];:;0",
                    Environment.NewLine,
                    "[LOGS]:",
                    "[LogChat];:;1",
                    "[LogCommands];:;1",
                    "[LogPlayerz];:;1",
                    Environment.NewLine,
                };
                File.WriteAllLines(cfgpath, newWrite);

            }

        }

        public void setDvars()
        {
            List<string> nono = new List<string>();
                      
                   
              string[] tempi = 
              {
                   "[ADMINS]:",
                     "[ADMINS]:",
                    "[AdminList]",
                    "[Overlord];:;-2222222222,-1111111111,0000000000",
                    "[Admin];:;-1111111111,0000000000,1111111111",
                    "[Moderator];:;0000000000,1111111111,2222222222",
                    "//Seperate guid's with a comma",
                    "[Overlord=Commands=[^1MA^7]];:;*ALL*,ban,tmpban,kick,warn,unwarn,map,mode,setteam,afk,add,remove,reloadsinscript,admins,@admins,rcon,balance,res,pm,rotate",
                    "[Admin=Commands=[^3A^7]];:;admins,@admins,ban,kick,tmpban,warn,unwarn,map,admins,pm,afk",
                    "[Moderator=Commands=[^7MOD^7]];:;admins,tmpban,kick,warn,unwarn,admins,pm,afk",
                    "[User=Commands=];:;admins,afk,pm",
                    "//Seperate commands with a comma",
                    "//User commands can be used by all non-admins in the server",
                    "//NOTE: Rights of a lower ranking group are not automatically given to a higher ranking group",
                    "[AdminList]",
                    "[SECURITY]:",
                    "[MESSAGES]:",
                    "[FUN]:",
                    "[VOTE]:",            
                    "[SERVER SETTINGS]:",
                    "[ANTI's]:",
                    "[LOGS]:", 
              };
            foreach(string x in tempi)
                nono.Add(x);
            string[] dvarlist = File.ReadAllLines(adminpath);
            string[] dvarlist2 = File.ReadAllLines(cfgpath);

                foreach (string line in dvarlist)
                {
                    if(line.Contains(";:;"))
                    {
                        string temp = String.Join("", line.Split('[', ']'));
                    initDvar(temp.Split(new string[] { ";:;" }, StringSplitOptions.None)[0], temp.Split(new string[] { ";:;" }, StringSplitOptions.None)[1]);
                    }
                }
                foreach (string line in dvarlist2)
                {
                    if (line.Contains(";:;"))
                    {
                        string temp = String.Join("", line.Split('[', ']'));
                        initDvar(temp.Split(new string[] { ";:;" }, StringSplitOptions.None)[0], temp.Split(new string[] { ";:;" }, StringSplitOptions.None)[1]);
                    }
                }
                cm = Call<string>("getdvar", "connectmessage");
                bot = Call<string>("getdvar", "botname");
                bot = stringDvar("botname");
                pm = stringDvar("PMBotName");
                wm = stringDvar("warningmessage");
                wl = intDvar("warninglimit");
                bm = stringDvar("banmessage");
                tm = stringDvar("tempbanmessage");
                km = stringDvar("kickmessage");
                um = stringDvar("unwarnmessage");
                clantag = stringDvar("clanvsall");
                dspl = stringDvar("DSPL");
                _dspl = stringDvar("DSPL");
        }
        public void initDvar(string dvar, string val)
        {
            var value = val;
#if dev
            Log.Write(LogLevel.All, dvar + " " + val);
#endif
            Call("setdvarifuninitialized", dvar, value);
        }

        public bool dvarCheck(string dvar)
        {
            string checker = Call<string>("getdvar", dvar);
            if (checker == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int intDvar(string dvar)
        {
            try
            {
                int val = Convert.ToInt32(Call<string>("getdvar", "ERROR:" + dvar));
                return val;
            }
            catch (Exception e)
            {
                Log.Write(LogLevel.All, dvar);
                return 0;
            }
        }

        public float floatDvar(string dvar)
        {
            float val = Convert.ToSingle(Call<string>("getdvar", dvar));
            return val;
        }

        public string stringDvar(string dvar)
        {
            string val = Call<string>("getdvar", dvar);
            return val;
        }

        public void initAdmins()
        {
            string path = @"scripts\\" + "SinAdmin\\" + "Admins.cfg";
            StreamWriter erros = new StreamWriter(fs);
            string[] adminlines = File.ReadAllLines(path);

            foreach (string s in adminlines)
            {
                try
                {
                    string[] set0 = s.Split(new string[] { ";:;" }, StringSplitOptions.None);
                    string set1 = set0[0];

                    switch (set1.ToLower())
                    {

                        case ("[blockedcommands]"):
                            set0[1] = set0[1].Replace(" ", "");
                            string[] blo = set0[1].Split(',');

                            foreach (string sb in blo)
                            {
                                if (!blockedcommands.Contains(sb))
                                {
                                    blockedcommands.Add(sb);
                                }
                            }
                            break;
                    }
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
            }
        }

        private unsafe void init()
        {
#if dev
            Log.Write(LogLevel.Debug, "init");
#endif
            StreamWriter erros = new StreamWriter(fark);
            try
            {


                Thread ips = new Thread(MainIPS);
                ips.Start();
                try
                {
                    cmdAl = File.ReadAllLines(@"scripts\\" + "SinAdmin\\" + cfgname + "\\scriptfiles\\cmdalias.txt");
                }
                catch { }

                if (dvarCheck("sndkillstreak"))
                {
                    var xD = Call<string>("getdvar", "g_gametype").ToLower();
                    if (xD != "sd")
                    {
                        File.WriteAllText(@"scripts\\" + "SinAdmin\\" + cfgname + "\\killstreak.txt", String.Empty);
                    }
                }

                if (dvarCheck("anticamp"))
                {
                    AntiCamp();
                }
                try
                {
                    StreamReader reade = new StreamReader(swit);

                    string x = reade.ReadLine();

                    {
                        File.Delete(dsplpath);
                        swit.Dispose();
                        swit.Close();
                        AfterDelay(200, () =>
                        {
                            File.Delete(@"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\isSwitch");
                        });
                        Utilities.ExecuteCommand("sv_maprotation " + _dspl);
                        reade.Close();
                    }
                }

                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                if (dvarCheck("knife"))
                {
                    NoKnife knife = new NoKnife();
                    knife.DisableKnife();
                }


                if (File.Exists(@"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\FuckRogue.txt"))
                {
                    string[] RoguesAbitch = File.ReadAllLines(@"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\FuckRogue.txt");
                    string xx = RoguesAbitch[0];
                }
                else
                {
                    getDefault();
                }

                if (dvarCheck("botmod"))
                {
                    try
                    {
                        OnInterval(1000, () =>
                        {

                            string path = @"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\bots.txt";
                            if (!File.Exists(path))
                            {
                                File.WriteAllText(path, String.Empty);
                            }
                            string[] place = File.ReadAllLines(path);
                            Random rnd = new Random();
                            foreach (Entity ent in BotList)
                            {

                                int x = rnd.Next(0, 17);
                                string temp = place[x];
                                string[] final = temp.Split(' ');
                                var p2 = ent.Origin;
                                p2.X = float.Parse(final[0], CultureInfo.InvariantCulture.NumberFormat);
                                //  Utilities.SayAll(p2.X.ToString());
                                p2.Y = float.Parse(final[1], CultureInfo.InvariantCulture.NumberFormat);
                                // Utilities.SayAll(p2.Y.ToString());
                                p2.Z = float.Parse(final[2], CultureInfo.InvariantCulture.NumberFormat);
                                // Utilities.SayAll(p2.Z.ToString());
                                ent.Call("setorigin", p2);
                                ent.Call("freezecontrols", true);
                                ent.TakeAllWeapons();
                                ent.GiveWeapon("iw5_msr_mp");
                                ent.SwitchToWeapon("iw5_msr_mp");
                            }


                            //PlayerConnected += new Action<Entity>(connect);
                            return true;
                        });
                    }
                    catch (Exception error)
                    { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

                }


                disSpeed();

                if (dvarCheck("antiaimbot"))
                {
                    datAntiAim();
                }

                maxclients = base.Call<int>("getdvarint", new Parameter[] { "sv_maxclients" });

                BioRox();

                bioizhere();

                //OnNotify("end", (player) =>
                //     {
                //         if (roundUnfreeze)
                //         {
                //             foreach (Entity entity in Playerz)
                //             {
                //                 AfterDelay(400, () =>
                //                 {
                //                     unfreezee(entity);
                //                 });
                //             }
                //         }
                //     });

                try
                {

                    if (!File.Exists(recoilpath))
                    {
                        //File.WriteAllBytes(recoilpath, new byte[0]);
                        StreamWriter sw = new StreamWriter(recoilpath, true);
                        sw.WriteLine("");
                        sw.Close();
                    }
                    string[] temprecoil = File.ReadAllLines(recoilpath);
                    foreach (string s in temprecoil)
                    {
                        foreach (Entity ent in Playerz)
                        {
                            if (s.StartsWith(ent.Name + ":" + ent.GUID.ToString() + ":"))
                            {
                                string[] temp = s.Split(':');
                                rlr.Add(temp[0] + ":" + temp[1]);
                            }
                            break;
                        }
                    }
                }
                catch (Exception error)
                {

                    erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true);
                    Call("setdvar", "antinorecoil", "0");

                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }



    
        // It's true

        private void BioRox()
        {
#if dev
            Log.Write(LogLevel.Debug, "biorox");
#endif
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                OnInterval(30000, () =>
                {
                    if (interval1 == true)
                    {
                        // Was gonna name this one 'Cleanse Player List', but I think this name is more suiting...
                        RogueSuxAtCoding();
                        recheckLogins();
                        removeOldWarns();
                        return true;
                    }
                    else
                    {
                        interval1 = true;
                        return true;
                    }
                });
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }
        private void bioizhere()
        {
#if dev
            Log.Write(LogLevel.Debug, "bioizhere");
#endif
            bool one = false, two = false;
            OnInterval(2500, () =>
            {
                if (one == true && two == false)
                {
                    foreach (Entity player in Playerz)
                    {
                        basePromod(player);
                    }
                    /*
                        foreach (Entity admin in Admins)
                        {
                            if (Bio == true)
                            {
                                Utilities.RawSayTo(admin, pm + "^2Bio bool is enabled");
                            }
                            else if (Bio == false)
                            {
                                Utilities.RawSayTo(admin, pm + "^1Bio bool is disabled");
                            }
                        }
                     * */

                    two = true;
                    return true;
                }
                else if (one == false)
                {
                    one = true;
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }
        #endregion

        #region Admins

        public void listAdmins(Entity ent, string pmorall)
        {
            StreamWriter erros = new StreamWriter(fs);
            todisplay = "";
            int counter = 0;
            try
            {
                string found;

                string tempadmin = "";

                foreach (string admin in admindisplaylist)
                {
                    tempadmin = admin;

                    found = "notfound";
                    foreach (string temp in admindisplaylistV2)
                    {
                        if (tempadmin == temp.Split(new string[] { ";:;" }, StringSplitOptions.None)[0] + temp.Split(new string[] { ";:;" }, StringSplitOptions.None)[1])
                        {
                            found = temp;
                        }
                    }

                    if (found != "notfound" && dvarCheck("uselogin") == true)
                    {
                        Entity checklogged = FindByName(found.Split(new string[] { ";:;" }, StringSplitOptions.None)[0]);

                        if (checklogged != null)
                        {
                            if (!LoggedIn.Contains(checklogged))
                            {
                                tempadmin = found.Split(new string[] { ";:;" }, StringSplitOptions.None)[0] + "^7[^1X^7]";
                            }
                        }
                    }

                    if (counter == 0)
                    {
                        todisplay += "^7" + tempadmin;
                    }
                    else if (counter == 1)
                    {
                        todisplay += "^7; :: ^7" + tempadmin;
                    }
                    else if (counter == 2)
                    {
                        todisplay += "^7; :: ^7" + tempadmin;
                    }

                    counter++;

                    if (counter > 2)
                    {
                        if (pmorall.ToLower() == "pm")
                        {
                            Utilities.RawSayTo(ent, pm + "^7[^5ADMINS^7] : " + todisplay);
                        }
                        else if (pmorall.ToLower() == "all")
                        {
                            Utilities.RawSayAll(bot + "^7[^5ADMINS^7] : " + todisplay);
                        }
                        todisplay = "";
                        counter = 0;
                    }
                }
                if (admindisplaylist.Count < 1)
                {
                    if (pmorall.ToLower() == "pm")
                    {
                        Utilities.RawSayTo(ent, pm + "^7[^5ADMINS^7] : " + "^1There are no admins online.");
                    }
                    else if (pmorall.ToLower() == "all")
                    {
                        Utilities.RawSayAll(bot + "^7[^5ADMINS^7] : " + "^1There are no admins online.");
                    }
                }
                if (counter != 0)
                {
                    if (pmorall.ToLower() == "pm")
                    {
                        Utilities.RawSayTo(ent, pm + "^7[^5ADMINS^7] : " + todisplay);
                    }
                    else if (pmorall.ToLower() == "all")
                    {
                        Utilities.RawSayAll(bot + "^7[^5ADMINS^7] : " + todisplay);
                    }
                    todisplay = "";
                    counter = 0;
                }
            }
            catch
            {
                if (pmorall.ToLower() == "pm")
                {
                    Utilities.RawSayTo(ent, "^7[^5ADMINS^7] : " + "^1Error loading admins");
                }
                else if (pmorall.ToLower() == "all")
                {
                    Utilities.RawSayAll("^7[^5ADMINS^7] : " + "^1Error loading admins");
                }
            }
        }

        public void kickComms(Entity ent, string target, string kicker, string message, string kickaction)
        {

            string client = "";

            Entity entit = FindByName(target);

            if (entit != null)
            {
                client = entit.Name;

                if (message == "DefaultKickMessage")
                {
                    message = "Player kicked.";
                }

                Utilities.ExecuteCommand(kickaction + " " + "\"" + client + "\"" + " " + "\"" + message + "\"");
                //Utilities.RawSayAll(kickaction + " " + "\"" + client + "\"" + " " + "\"" + message + "\"");
                string kickecho = "";
                switch (kickaction)
                {
                    case ("ban"):
                        kickecho = bm;
                        Utilities.ExecuteCommand("banclient " + entit.Call<int>("getentitynumber", new Parameter[0]));
                        Utilities.ExecuteCommand("dropclient" + " " + "\"" + entit.EntRef + "\"" + " " + "\"" + message + "\"");
                        kickecho = kickecho.Replace("<client>", client);
                        kickecho = kickecho.Replace("<issuer>", kicker);
                        kickecho = kickecho.Replace("<message>", message);
                        Utilities.RawSayAll(bot + kickecho);
                        break;
                    case ("kick"):
                        kickecho = tm;
                        kickecho = kickecho.Replace("<client>", client);
                        kickecho = kickecho.Replace("<issuer>", kicker);
                        kickecho = kickecho.Replace("<message>", message);
                        Utilities.RawSayAll(bot + kickecho);
                        break;
                    case ("drop"):
                        kickecho = km;
                        kickecho = kickecho.Replace("<client>", client);
                        kickecho = kickecho.Replace("<issuer>", kicker);
                        kickecho = kickecho.Replace("<message>", message);
                        Utilities.RawSayAll(bot + kickecho);
                        break;
                }
            }
            else
            {
                Utilities.RawSayTo(ent, pm + "^1Either multiple users were found or SinScript was unable to locate user.");
            }

        }

        public void addAdmins(Entity ent, string player, string group, string action)
        {
            StreamWriter erros = new StreamWriter(fs);
            guid = ent.GUID;
            string path = @"scripts\\" + "SinAdmin\\" + "Admins.cfg";
            string[] lines = File.ReadAllLines(path);

            int counter = 0;

            string fullname = "";

            Entity entit = FindByName(player);

            if (entit != null)
            {
                if ((getRank(entit) == "User" && action.ToLower() == "add") || (action.ToLower() == "remove"))
                {
                    if (blockallads == false || isAllowed(null, group, "*ALL") == false)
                    {
                        guid = entit.GUID;
                        fullname = entit.Name;
                        foreach (string s in lines)
                        {
                            if (s.ToLower().StartsWith("[" + group.ToLower() + "];:;"))
                            {
                                if (action.ToLower() == "add")
                                {
                                    if (!s.Contains(guid.ToString()))
                                    {
                                        lines[counter] = s + "," + guid.ToString();
                                        File.Delete(path);
                                        File.WriteAllLines(path, lines);
                                        Utilities.RawSayAll(bot + fullname + " was successfully added to the " + group + " group by " + ent.Name);
                                        break;
                                    }
                                    else
                                    {
                                        Utilities.RawSayTo(ent, pm + "^1User is already contained in that user group.");
                                        break;
                                    }
                                }
                                else if (action.ToLower() == "remove")
                                {
                                    if (s.Contains(guid.ToString()))
                                    {
                                        string temp = s;
                                        temp = temp.Replace(guid.ToString(), "");
                                        temp = temp.Replace(",,", ",");
                                        lines[counter] = temp;
                                        File.WriteAllLines(path, lines);
                                        Utilities.RawSayAll(bot + fullname + " was successfully removed from the " + group + " group by " + ent.Name);
                                        break;
                                    }
                                    else
                                    {
                                        Utilities.RawSayTo(ent, pm + "^1User was not found in specified group.");
                                        break;
                                    }
                                }
                            }
                            counter++;
                        }
                    }
                    else
                    {
                        Utilities.RawSayTo(ent, pm + "^1Server administrator has not enabled adding or removing from that user group");
                    }
                }
                else
                {
                    Utilities.RawSayTo(ent, pm + "Player is already in the " + getRank(entit) + " permissions group. Please remove player from this group before adding to a new group.");
                }
            }
            else
            {
                Utilities.RawSayTo(ent, pm + "^1Either multiple users were found or SinScript was unable to locate user.");
            }

        }

        public void adminchat(Entity caller, string message)
        {
            message = message.Substring(1);

            foreach (Entity admin in Admins)
            {
                if (getAlias(caller.Name) != "GhostyBeTrippin.........." && aliasname == true)
                {
                    Utilities.RawSayTo(admin, "^1~" + caller.Name + " ^7(" + getAlias(caller.Name) + "^7)" + "^7: ^3" + message);
                }
                else
                {
                    if (getAlias(caller.Name) != "GhostyBeTrippin..........")
                    {
                        Utilities.RawSayTo(admin, "^1~" + getAlias(caller.Name) + "^7: ^3" + message);
                    }
                    else
                    {
                        Utilities.RawSayTo(admin, "^1~" + caller.Name + "^7: ^3" + message);
                    }
                }
            }
            if (!Admins.Contains<Entity>(caller))
            {
                Utilities.RawSayTo(caller, pm + "^2Message sent to all server overlords.");
            }
        }

        public void epicLogin(Entity loginster, string saidpass)
        {
            StreamWriter erros = new StreamWriter(fs);
            string temp = saidpass.Replace("DefaultKickMessage", "");

            string rank = getRank(loginster);
            bool disbool = checkpass(rank, temp);

            if ((disbool == true || LoggedIn.Contains(loginster)) && dvarCheck("uselogin") == true)
            {
                if (!LoggedIn.Contains(loginster))
                {
                    Utilities.RawSayTo(loginster, pm + "^5You have ^0successfuly ^1logged in.");
                    string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\LoggedIn.txt";
                    StreamWriter write = new StreamWriter(path, true);
                    LoggedIn.Add(loginster);
                    write.WriteLine(loginster.GUID.ToString() + "=" + loginster.IP.ToString(), true);
                    write.Close();
                }
                else
                {
                    Utilities.RawSayTo(loginster, pm + "^2You are already logged in.");
                }
            }
            else
            {
                if (disbool == false)
                {
                    string path = @"scripts\\" + "SinAdmin\\" + "Admins.cfg";
                    string[] lines = File.ReadAllLines(path);
                    bool found = false;

                    foreach (string s in lines)
                    {
                        if (s.StartsWith("[Login=" + rank + "]"))
                        {
                            found = true;
                        }
                    }
                    if (found == true)
                    {
                        Utilities.RawSayTo(loginster, pm + "^1Incorrect Password, ^5Try again.");
                    }
                    else
                    {
                        Utilities.RawSayTo(loginster, pm + "^1Server administrator has not set a password for this group. Group will be unusable");
                    }
                }
                else if (dvarCheck("uselogin") == false)
                {
                    Utilities.RawSayTo(loginster, pm + "^4No password required.");
                }
            }
        }

        public bool checkpass(string rank, string pass)
        {
            StreamWriter erros = new StreamWriter(fs);
            string path = @"scripts\\" + "SinAdmin\\" + "Admins.cfg";
            string[] clines = File.ReadAllLines(path);
            bool found = false;

            string temp1 = "";

            foreach (string s in clines)
            {
                if (s.StartsWith("[Login=" + rank + "]"))
                {
                    string[] temp0 = s.Split(new string[] { ";:;" }, StringSplitOptions.None);
                    try
                    {
                        temp1 = temp0[1];
                        found = true;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            if (pass == temp1)
            {
                return true;
            }
            else if (found == false)
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        public void spawnLoginCheck(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\LoggedIn.txt";
            string[] logged = File.ReadAllLines(path);
            if (logged.Contains<string>(player.GUID.ToString() + "=" + player.IP.ToString()))
            {
                LoggedIn.Add(player);
            }
        }

        public void recheckLogins()
        {
            StreamWriter erros = new StreamWriter(fs);
            LoggedIn.Clear();
            foreach (Entity et in Playerz)
            {
                spawnLoginCheck(et);
            }
            string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\LoggedIn.txt";
            File.WriteAllText(path, String.Empty);
            StreamWriter write = new StreamWriter(path, true);

            foreach (Entity e in Playerz)
            {
                if (LoggedIn.Contains(e))
                {
                    write.WriteLine(e.GUID.ToString() + "=" + e.IP.ToString(), true);
                }
            }
            write.Close();
        }

        // Fills the admindisplay list
        public void populateadmins()
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                // Same code as on player connect, but this fixes a bug
                foreach (Entity player in Playerz)
                {
                    string Rank = getRank(player);
                    if (Rank != "User")
                    {
                        string[] lines = File.ReadAllLines(adminpath);
                        foreach (string line in lines)
                        {
                            if (line.Contains("[" + Rank + "=Commands="))
                            {
                                string[] temp0 = line.Split(new string[] { ";:;" }, StringSplitOptions.None);
                                string temp1 = temp0[0];
                                temp1 = temp1.Replace("[" + Rank + "=Commands=", "");
                                temp1 = temp1.Remove(temp1.Length - 1);
                                if (!admindisplaylist.Contains(player.Name + temp1))
                                {
                                    admindisplaylist.Add(player.Name + temp1);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void warn(Entity ent, string target, string issuer, string message, string action)
        {

            string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\currentwarns.warn";
            if (!File.Exists(path))
            {
                //File.WriteAllBytes(path, new byte[0]);
                StreamWriter sw = new StreamWriter(path, true);
                sw.WriteLine("");
                sw.Close();
            }

            guid = 0;

            string client = "";
            Entity entit = FindByName(target);
            if (entit != null)
            {
                if (message == "DefaultKickMessage")
                {
                    message = "Null";
                }

                guid = entit.GUID;

                client = entit.Name;
                string kickecho = "";
                if (action.ToLower() == "warn")
                {
                    kickecho = wm;
                    kickecho = kickecho.Replace("<client>", client);
                    kickecho = kickecho.Replace("<issuer>", issuer);
                    kickecho = kickecho.Replace("<message>", message);
                    kickecho = kickecho.Replace("<limit>", wl.ToString());
                    entit.Call("iprintlnbold", new Parameter[]
		            	{
			            	"^7You have been ^1warned."
		            	});

                    string[] lines = File.ReadAllLines(path);

                    bool found = false;
                    foreach (string s in lines)
                    {
                        if (s.Contains(guid.ToString()))
                        {
                            found = true;

                        }
                    }

                    if (found == true)
                    {
                        int counter = 0;
                        foreach (string s in lines)
                        {
                            if (s.StartsWith(guid.ToString()))
                            {
                                string[] spl = s.Split(';');
                                try
                                {
                                    wc = Convert.ToInt32(spl[1]) + 1;
                                }
                                catch
                                {
                                    wc = 0;
                                }
                                spl[1] = Convert.ToString(wc);
                                lines[counter] = spl[0] + ";" + spl[1];

                                File.Delete(path);
                                File.WriteAllLines(path, lines);
                            }
                            counter++;
                        }
                    }
                    else
                    {
                        var removewhites = File.ReadAllLines(path).Where(arg => !string.IsNullOrWhiteSpace(arg));
                        File.WriteAllLines(path, removewhites);

                        StreamWriter sw = new StreamWriter(path, true);
                        sw.WriteLine("");
                        sw.WriteLine("");
                        sw.WriteLine(guid.ToString() + ";1");
                        sw.Close();
                        wc = 1;
                    }
                    kickecho = kickecho.Replace("<count>", wc.ToString());
                    Utilities.RawSayAll(bot + kickecho);
                }
                else if (action.ToLower() == "unwarn")
                {
                    kickecho = um;
                    kickecho = kickecho.Replace("<client>", client);
                    kickecho = kickecho.Replace("<issuer>", issuer);
                    kickecho = kickecho.Replace("<message>", message);
                    kickecho = kickecho.Replace("<limit>", wl.ToString());
                    entit.Call("iprintlnbold", new Parameter[]
		            	{
			            	"^7You have been ^2unwarned."
		            	});

                    string[] lines = File.ReadAllLines(path);

                    bool found = false;
                    foreach (string s in lines)
                    {
                        if (s.Contains(guid.ToString()))
                        {
                            found = true;

                        }
                    }

                    if (found == true)
                    {
                        int counter = 0;
                        foreach (string s in lines)
                        {
                            if (s.StartsWith(guid.ToString()))
                            {
                                string[] spl = s.Split(';');
                                try
                                {
                                    wc = Convert.ToInt32(spl[1]) - 1;
                                    if (wc < 0)
                                    {
                                        wc = 0;
                                    }
                                }
                                catch
                                {
                                    wc = 0;
                                }
                                spl[1] = Convert.ToString(wc);
                                lines[counter] = spl[0] + ";" + spl[1];

                                File.Delete(path);
                                File.WriteAllLines(path, lines);
                            }
                            counter++;
                        }
                    }
                    else
                    {
                        StreamWriter sw = new StreamWriter(path, true);
                        sw.WriteLine(guid.ToString() + ";0");
                        sw.Close();
                        wc = 0;
                    }
                    kickecho = kickecho.Replace("<count>", wc.ToString());
                    Utilities.RawSayAll(bot + kickecho);
                }

                if (wc >= wl)
                {
                    if (message == "Null")
                    {
                        message = "Too many warnings";
                    }
                    Utilities.ExecuteCommand("kick" + " " + "\"" + client + "\"" + " " + "\"" + message + "\"");
                }
            }
            else
            {
                Utilities.RawSayTo(ent, pm + "^1Either multiple users were found or SinScript was unable to locate user.");
            }


        }

        public bool isAllowed(Entity user, string rank, string command)
        {
            commlist.Clear();

            string group = rank;

            bool readgroups = false;

            string path = @"scripts\\" + "SinAdmin\\" + "Admins.cfg";
            string[] configlines = File.ReadAllLines(path);

            foreach (string s in configlines)
            {
                if (readgroups == true)
                {
                    if (s.Contains("[" + group + "=Commands="))
                    {
                        string[] listem0 = s.Split(new string[] { ";:;" }, StringSplitOptions.None);
                        string templist = listem0[1];
                        string[] listem1 = templist.Split(',');
                        foreach (string str in listem1)
                        {
                            if (! commlist.Contains(str))
                            {
                                 commlist.Add(str);
                            }
                        }
                    }
                }
                else if (s.ToLower() == "[adminlist]")
                {
                    readgroups = true;
                }
            }

            if ( commlist.Contains(command) ||  commlist.Contains("*ALL*"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isBlockedCommand(string command)
        {
            bool toreturn = false;

            foreach (string s in blockedcommands)
            {
                if (s.ToLower() == command.ToLower())
                {
                    toreturn = true;
                }
            }

            return toreturn;
        }

        public void addSpy(Entity user)
        {

            string group = getRank(user);

            string path = @"scripts\\" + "SinAdmin\\" + "Admins.cfg";
            string[] configlines = File.ReadAllLines(path);

            foreach (string s in configlines)
            {
                if (s.Contains("[" + group + "=Commands="))
                {
                    string[] listem0 = s.Split(new string[] { ";:;" }, StringSplitOptions.None);
                    string templist = listem0[1];
                    string[] listem1 = templist.Split(',');
                    foreach (string str in listem1)
                    {
                        if (!Admins.Contains(user))
                        {
                            if (group.Contains("Overlord") || templist.Contains("*ALL*"))
                            {
                                Admins.Add(user);
                            }
                        }
                    }
                }
            }
        }

        public void map(Entity ent, string map)
        {

            switch (map.ToLower())
            {
                case ("lockdown"):
                    Utilities.ExecuteCommand("map mp_alpha");
                    break;
                case ("bootleg"):
                    Utilities.ExecuteCommand("map mp_bootleg");
                    break;
                case ("mission"):
                    Utilities.ExecuteCommand("map mp_bravo");
                    break;
                case ("carbon"):
                    Utilities.ExecuteCommand("map mp_carbon");
                    break;
                case ("dome"):
                    Utilities.ExecuteCommand("map mp_dome");
                    break;
                case ("downturn"):
                    Utilities.ExecuteCommand("map mp_exchange");
                    break;
                case ("hardhat"):
                    Utilities.ExecuteCommand("map mp_hardhat");
                    break;
                case ("interchange"):
                    Utilities.ExecuteCommand("map mp_interchange");
                    break;
                case ("fallen"):
                    Utilities.ExecuteCommand("map mp_lambeth");
                    break;
                case ("bakaara"):
                    Utilities.ExecuteCommand("map mp_mogadishu");
                    break;
                case ("resistance"):
                    Utilities.ExecuteCommand("map mp_paris");
                    break;
                case ("arkaden"):
                    Utilities.ExecuteCommand("map mp_plaza2");
                    break;
                case ("outpost"):
                    Utilities.ExecuteCommand("map mp_radar");
                    break;
                case ("seatown"):
                    Utilities.ExecuteCommand("map mp_seatown");
                    break;
                case ("underground"):
                    Utilities.ExecuteCommand("map mp_underground");
                    break;
                case ("village"):
                    Utilities.ExecuteCommand("map mp_village");
                    break;
                case ("piazza"):
                    Utilities.ExecuteCommand("map mp_italy");
                    break;
                case ("liberation"):
                    Utilities.ExecuteCommand("map mp_park");
                    break;
                case ("blackbox"):
                    Utilities.ExecuteCommand("map mp_morningwood");
                    break;
                case ("overwatch"):
                    Utilities.ExecuteCommand("map mp_overwatch");
                    break;
                case ("aground"):
                    Utilities.ExecuteCommand("map mp_aground_ss");
                    break;
                case ("erosion"):
                    Utilities.ExecuteCommand("map mp_courtyard_ss");
                    break;
                case ("foundation"):
                    Utilities.ExecuteCommand("map mp_cement");
                    break;
                case ("getaway"):
                    Utilities.ExecuteCommand("map mp_hillside_ss");
                    break;
                case ("sanctuary"):
                    Utilities.ExecuteCommand("map mp_meteora");
                    break;
                case ("oasis"):
                    Utilities.ExecuteCommand("map mp_qadeem");
                    break;
                case ("lookout"):
                    Utilities.ExecuteCommand("map mp_restrepo_ss");
                    break;
                case ("terminal"):
                    Utilities.ExecuteCommand("map mp_terminal_cls");
                    break;
                case ("intersetion"):
                    Utilities.ExecuteCommand("map mp_crosswalk");
                    break;
                case ("u-turn"):
                    Utilities.ExecuteCommand("map mp_six");
                    break;
                case ("uturn"):
                    Utilities.ExecuteCommand("map mp_six");
                    break;
                case ("vortex"):
                    Utilities.ExecuteCommand("map mp_burn");
                    break;
                case ("moab"):
                    Utilities.ExecuteCommand("map mp_moab");
                    break;
                case ("boardwalk"):
                    Utilities.ExecuteCommand("map mp_boardwalk");
                    break;
                default:
                    Utilities.RawSayTo(ent, pm + "^1Unable to locate desired map.");
                    break;
            }

        }

        public void rcon(string command)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                Utilities.ExecuteCommand(command);
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void restart()
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                Utilities.ExecuteCommand("fast_restart");
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void rotate()
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                Utilities.ExecuteCommand("map_rotate");
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void immune(Entity e, string player, string action)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                Entity ent = FindByName(player);
                if (ent != null)
                {
                    int poscounter = -1;
                    string path = @"scripts\\" + "SinAdmin\\" + "\\Admins.cfg";

                    string availguids = "";

                    string[] lines = File.ReadAllLines(path);
                    foreach (string s in lines)
                    {
                        poscounter++;
                        if (s.StartsWith("[ImmunePlayerz]"))
                        {
                            string[] FSplit = s.Split(new string[] { ";:;" }, StringSplitOptions.None);
                            availguids = FSplit[1];
                            break;
                        }
                    }


                    switch (action.ToLower())
                    {
                        case ("add"):
                            if (isImmune(e, player) == true)
                            {
                                Utilities.RawSayTo(e, pm + "^1Player is already immune.");
                                break;
                            }
                            lines[poscounter] += "," + ent.GUID;
                            File.WriteAllLines(path, lines);
                            Utilities.RawSayAll(bot + ent.Name + " has been added to the immune group by: " + e.Name);
                            break;
                        case ("remove"):
                            if (isImmune(e, player) == false)
                            {
                                Utilities.RawSayTo(e, pm + "^1Player is already not immune.");
                                break;
                            }
                            lines[poscounter] = lines[poscounter].Replace(ent.GUID.ToString(), "");
                            lines[poscounter] = lines[poscounter].Replace(",,", ",");
                            File.WriteAllLines(path, lines);
                            Utilities.RawSayAll(bot + ent.Name + " has been removed from the immune group by: " + e.Name);
                            break;
                    }
                }
                else
                {
                    Utilities.RawSayTo(e, pm + "^1Invalid name to add modify immunity.");
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public bool isImmuneClientNumber(Entity enti, string number)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                Entity blank = null;
                Entity eplayer = entCN[Convert.ToInt32(number)];
                bool toreturn = isImmune(blank, eplayer.Name);
                return toreturn;
            }
            catch
            {
                return false;
            }
        }

        public Entity findByClientNumber(string number)
        {
            try
            {
                Entity eplayer = entCN[Convert.ToInt32(number)];

                return eplayer;
            }
            catch
            {
                return null;
            }
        }


        public bool isImmune(Entity enti, string player)
        {
            StreamWriter erros = new StreamWriter(fs);
            //  try
            //{
            bool isim = false;
            Entity ent = FindByName(player);
            if (ent != null)
            {
                string path = @"scripts\\" + "SinAdmin\\" + "Admins.cfg";

                string availguids = "";

                string[] lines = File.ReadAllLines(path);
                foreach (string s in lines)
                {
                    if (s.StartsWith("[ImmunePlayerz]"))
                    {
                        string[] FSplit = s.Split(new string[] { ";:;" }, StringSplitOptions.None);
                        availguids = FSplit[1];
                        break;
                    }
                }
                if (availguids.Contains(ent.GUID.ToString()))
                {
                    isim = true;
                }
            }
            else
            {
                try
                {
                    Utilities.RawSayTo(enti, pm + "^1Invalid name to check.");
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
            }
            if (ent != null)
            {
                if (gainedaccess.Contains(ent))
                {
                    isim = true;
                }
            }
            return isim;
            //}
            //   catch { }
        }

        public void yell(Entity player, string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                char x = ' ';
                string[] tempConv = message.Split(x);
                string finalMessage = "";

                if (tempConv[1].ToLower() == "all")
                {
                    finalMessage = message.Replace("!yell", "").Replace("all", "");
                    foreach (Entity ent in Playerz)
                    {
                        ent.Call("iprintlnbold", new Parameter[]
			    {
			    	finalMessage
			    });
                    }
                }
                else
                {
                    try
                    {
                        finalMessage = message.Replace("!yell", "").Replace(tempConv[1], "");
                        Entity pmUser = FindByName(tempConv[1]);
                        pmUser.Call("iprintlnbold", new Parameter[]
		            	{
			            	finalMessage
		            	});
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void clanvsall()
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                Entity player = null;
                sayAsBot(player, "^5" + stringDvar("clanvsall") + " ^1vs All");
                foreach (Entity ent in Playerz)
                {
                    if (ent.Name.Contains(stringDvar("clanvsall")))
                    {
                        if (ent.GetField<string>("sessionteam") != "spectator" && ent.GetField<string>("sessionteam") != "axis")
                        {
                            ent.SetField("team", "axis");
                            ent.SetField("sessionteam", "axis");
                            ent.Notify("menuresponse", new Parameter[] { "team_marinesopfor", "axis" });
                        }
                    }
                    else
                    {
                        if (ent.GetField<string>("sessionteam") != "spectator" && ent.GetField<string>("sessionteam") != "allies")
                        {
                            ent.SetField("team", "allies");
                            ent.SetField("sessionteam", "allies");
                            ent.Notify("menuresponse", new Parameter[] { "team_marinesopfor", "allies" });
                        }
                    }
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void freeze(string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                char x = ' ';
                string[] tempConv = message.Split(x);
                Entity target = FindByName(tempConv[1]);
                target.Call("freezecontrols", true);
                //sayAsBot(target, "^5" + target.Name + " ^1Has been frozen");
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void unfreeze(string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                char x = ' ';
                string[] tempConv = message.Split(x);
                Entity target = FindByName(tempConv[1]);
                target.Call("freezecontrols", false);
                //sayAsBot(target, "^5" + target.Name + " ^1Has been unfrozen");
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void setFx(Entity player, string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                char x = ' ';
                string[] tempConv = message.Split(x);
                Entity target = FindByName(tempConv[1]);
                string fx = tempConv[2];
                Call("playfx", Call<int>("loadfx", fx), target.Origin);
                Utilities.RawSayTo(player, pm + "^5" + target.Name + " ^0FX was set to  ^1" + fx);
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void setFxPerm(Entity player, string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                char x = ' ';
                string[] tempConv = message.Split(x);
                Entity target = FindByName(tempConv[1]);
                string fx = tempConv[2];
                OnInterval(200, () =>
                {
                    Call("playfx", Call<int>("loadfx", fx), target.Origin);
                    return true;
                });
                Utilities.RawSayTo(player, pm + "^5" + target.Name + " ^0FX was permset to  ^1" + fx);
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void fire(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                int fxid = Call<int>("loadfx", "misc/flares_cobra");
                OnInterval(200, () =>
                {
                    Call("playfx", fxid, player.Origin);
                    return true;
                });
                Utilities.RawSayTo(player, pm + "^1FIREEEEEEEEEEEE");
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void playSound(Entity player, string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                char x = ' ';
                string[] tempConv = message.Split(x);
                Entity soundEnt = FindByName(tempConv[1]);
                string sound = tempConv[2];
                soundEnt.Call("playlocalsound", sound);
                Utilities.RawSayTo(player, pm + "^5" + soundEnt.Name + " ^0Sound played  ^1" + sound);
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void fakeSay(Entity player, string target, string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                if (message != "DefaultKickMessage")
                {
                    if (message.EndsWith(" "))
                    {
                        message = message.Remove(message.Length, 1);
                    }
                    Entity playersay = FindByName(target);
                    if (playersay != null)
                    {
                        OnSay3(playersay, ChatType.All, "ghosty1234567890..........", ref message);
                    }
                    else
                    {
                        Utilities.RawSayTo(player, pm + "^1Unable to locate player");
                    }
                }
                else
                {
                    Utilities.RawSayTo(player, pm + "^1Please enter a message to send");
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void setServerName(string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                string temp = message.Replace("!svname", String.Empty);
                Call("setdvar", "sv_hostname", temp.Split(' ')[1]);
                Entity player = null;
                sayAsBot(player, "^5Server name ^1set to:" + temp);
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void setVision(Entity player, string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                //sayAsBot(player, "Intial Health:" + defaultHealth);
                char x = ' ';
                string[] tempConv = message.Split(x);
                Entity visEnt = FindByName(tempConv[1]);
                string vision = tempConv[2];
                visEnt.Call("visionsetnakedforplayer", vision);
                Utilities.RawSayTo(player, pm + "^5" + visEnt.Name + " ^0Vision was set to  ^1" + vision);
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void pingPlayer(Entity ent)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                Call("pingplayer", ent);
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

            try
            {
                ent.Call("pingplayer");
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

            try
            {
                ent.Call("pingplayer", 1);
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
            try
            {
                ent.Call("pingplayer", true);
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }


        public void invisiblegod(Entity issuer, string target)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                Entity player = FindByName(target);
                if (player != null)
                {
                    if (player.Health > 0)
                    {
                        player.Health = 0;
                        Utilities.RawSayTo(player, pm + "^2Invisible godmode enabled");
                        if (player.Name != issuer.Name)
                        {
                            Utilities.RawSayTo(issuer, pm + "^2Invisible godmode enabled for: ^7" + player.Name);
                        }
                    }
                    else
                    {
                        player.Health = 100;
                        Utilities.RawSayTo(player, pm + "^1Invisible godmode disabled");
                        if (player.Name != issuer.Name)
                        {
                            Utilities.RawSayTo(issuer, pm + "^1Invisible godmode disabled for: ^7" + player.Name);
                        }
                    }
                }
                else
                {
                    Utilities.RawSayTo(issuer, pm + "^1Unable to locate player.");
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void hitmarkerGM(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                if (!hitmarkergmlist.Contains(player.GUID))
                {
                    hitmarkergmlist.Add(player.GUID);
                }
                foreach (Entity ent in Playerz)
                {
                    ent.SetField("killstreak", "0");
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void afkgod(Entity issuer, string target)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                Entity player = FindByName(target);
                if (player != null)
                {
                    string field = player.GetField<string>("sessionteam");
                    if (field == "spectator")
                    {
                        afk(player);
                        Utilities.RawSayTo(player, pm + "^1Afk godmode disabled");
                        if (issuer.Name != player.Name)
                        {
                            Utilities.RawSayTo(issuer, pm + "^1Afk godmode disabled for: ^7" + player.Name);
                        }
                    }
                    else
                    {
                        player.SetField("team", "spectator");
                        player.SetField("sessionteam", "spectator");
                        Utilities.RawSayTo(player, pm + "^2Afk godmode enabled");
                        if (issuer.Name != player.Name)
                        {
                            Utilities.RawSayTo(issuer, pm + "^2Afk godmode enabled for: ^7" + player.Name);
                        }
                    }
                }
                else
                {
                    Utilities.RawSayTo(issuer, pm + "^1Unable to locate player.");
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void hitmarkergod(Entity issuer, string target)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                Entity player = FindByName(target);
                if (player != null)
                {
                    if (hitmarkergods.Contains(player.GUID))
                    {
                        hitmarkergods.Remove(player.GUID);
                        Utilities.RawSayTo(player, pm + "^1Hitmarker godmode disabled");
                        if (player.Name != issuer.Name)
                        {
                            Utilities.RawSayTo(issuer, pm + "^1Hitmarker godmode disabled for: ^7" + player.Name);
                        }
                    }
                    else
                    {
                        hitmarkergods.Add(player.GUID);
                        Utilities.RawSayTo(player, pm + "^2Hitmarker godmode enabled");
                        if (player.Name != issuer.Name)
                        {
                            Utilities.RawSayTo(issuer, pm + "^2Hitmarker godmode enabled for: ^7" + player.Name);
                        }
                    }
                }
                else
                {
                    Utilities.RawSayTo(issuer, pm + "^1Unable to locate target.");
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void rules(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            int tempcount = -1;

            int displaycount = 0;

            string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\rules.txt";

            string[] lines = File.ReadAllLines(path);

            foreach (string s0 in lines)
            {
                tempcount++;
                //displaycount++;
            }

            OnInterval(1000, () =>
            {
                if (tempcount > -1)
                {
                    while (lines[displaycount] == "")
                    {
                        tempcount--;
                        displaycount++;
                        if (tempcount < 0)
                        {
                            break;
                        }
                    }
                    Utilities.RawSayTo(player, pm + lines[displaycount]);
                    tempcount--;
                    displaycount++;
                    return true;
                }
                else
                {
                    return false;
                }
            });

        }

        public void _rules()
        {
            StreamWriter erros = new StreamWriter(fs);
            if (!dvarCheck("antilag"))
            {
                int tempcount = -1;

                int displaycount = 0;

                string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\rules.txt";

                string[] lines = File.ReadAllLines(path);

                foreach (string s0 in lines)
                {
                    tempcount++;
                    //displaycount++;
                }

                OnInterval(700, () =>
                {
                    if (tempcount > -1)
                    {
                        while (lines[displaycount] == "")
                        {
                            tempcount--;
                            displaycount++;
                            if (tempcount < 0)
                            {
                                break;
                            }
                        }
                        Utilities.RawSayAll(bot + lines[displaycount]);
                        tempcount--;
                        displaycount++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
            }
        }

        public void teleport(Entity player, string player1, string destPlayer)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                if (player1.ToLower() != "all")
                {
                    Entity p1 = FindByName(player1);
                    Entity p2 = FindByName(destPlayer);

                    try
                    {
                        p1.Call("setorigin", p2.Origin);
                    }
                    catch (Exception error)
                    { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                }
                else
                {
                    Entity p2 = FindByName(destPlayer);

                    foreach (Entity p1 in Playerz)
                    {
                        try
                        {
                            p1.Call("setorigin", p2.Origin);
                        }
                        catch (Exception error)
                        { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                    }
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void teleportoptions(Entity player, string target, string option, int distance)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                Entity found = FindByName(target);
                if (found != null)
                {
                    switch (option.ToLower())
                    {
                        case ("tx"):
                            {
                                var dest1 = found.Origin;
                                dest1.X += distance;
                                found.Call("setorigin", dest1);
                                break;
                            }
                        case ("ty"):
                            {
                                var dest2 = found.Origin;
                                dest2.Y += distance;
                                found.Call("setorigin", dest2);
                                break;
                            }
                        case ("tz"):
                            {
                                var dest3 = found.Origin;
                                dest3.Z += distance;
                                found.Call("setorigin", dest3);
                                break;
                            }
                    }
                }
                else
                {
                    try
                    {
                        distance = Convert.ToInt32(target);
                        target = player.Name;
                        found = FindByName(target);
                        switch (option.ToLower())
                        {
                            case ("tx"):
                                {
                                    var dest1 = found.Origin;
                                    dest1.X += distance;
                                    found.Call("setorigin", dest1);
                                    break;
                                }
                            case ("ty"):
                                {
                                    var dest2 = found.Origin;
                                    dest2.Y += distance;
                                    found.Call("setorigin", dest2);
                                    break;
                                }
                            case ("tz"):
                                {
                                    var dest3 = found.Origin;
                                    dest3.Z += distance;
                                    found.Call("setorigin", dest3);
                                    break;
                                }
                        }
                    }
                    catch
                    {
                        Utilities.RawSayTo(player, pm + "^1Unable to locate player.");
                    }
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void sortbyCN()
        {
            StreamWriter erros = new StreamWriter(fs);
            foreach (Entity ent in Playerz)
            {
                try
                {
                    int clientnum = ent.Call<int>("getentitynumber");
                    entCN[clientnum] = ent;
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
            }
        }

        public void kickCN(Entity player, string cn)
        {
            StreamWriter erros = new StreamWriter(fs);
            int intCN = Convert.ToInt32(cn);
            Entity kicker = entCN[intCN];
            Utilities.ExecuteCommand("dropclient " + kicker.Call<int>("getentitynumber") + " \"" + "You have been kicked, Have a great day!" + "\"");
            sayAsBot(player, pm + "^1" + kicker.Name + " was kicked by " + "^5" + player.Name);
        }

        public void banCN(Entity player, string cn)
        {
            StreamWriter erros = new StreamWriter(fs);
            int intCN = Convert.ToInt32(cn);
            Entity kicker = entCN[intCN];
            Utilities.ExecuteCommand("banclient " + kicker.Call<int>("getentitynumber"));
            Utilities.ExecuteCommand("dropclient " + kicker.Call<int>("getentitynumber") + " \"" + "You have been banned, Have a great day!" + "\"");
            sayAsBot(player, pm + "^1" + kicker.Name + " was banned by " + "^5" + player.Name);
        }

        public void PlayerIndex(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            int errc = 0;
            int count = -1;
            int x = 0;
            Entity temp = null;
            foreach (Entity e in Playerz)
            {
                count++;
            }
            OnInterval(750, () =>
            {
            Start:
                try
                {
                    if (count > -1)
                    {
                        temp = entCN[x];
                        Utilities.RawSayTo(player, pm + temp.Name + " [^3" + temp.Call<int>("getentitynumber") + "^7]: " + temp.GUID);
                        Utilities.RawSayTo(player, " : ^2" + temp.IP);
                        x++;
                        count--;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    errc++;
                    x++;
                    if (errc > 18)
                    {
                        Utilities.RawSayTo(player, pm + "^1Error.");
                        return false;
                    }
                    else
                    {
                        goto Start;
                    }
                }
            });
        }

        public void status(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            int errc = 0;
            int count = -1;
            int x = 0;
            Entity temp = null;
            foreach (Entity e in Playerz)
            {
                count++;
            }
            OnInterval(500, () =>
            {
            Start:
                try
                {
                    if (count > -1)
                    {
                        temp = entCN[x];
                        Utilities.RawSayTo(player, pm + temp.Name + " [^3" + temp.Call<int>("getentitynumber") + "^7] ");
                        x++;
                        count--;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    errc++;
                    x++;
                    if (errc > 18)
                    {
                        Utilities.RawSayTo(player, pm + "^1Error.");
                        return false;
                    }
                    else
                    {
                        goto Start;
                    }
                }
            });
        }

        public void _status()
        {
            int errc = 0;
            int count = -1;
            int x = 0;
            Entity temp = null;
            foreach (Entity e in Playerz)
            {
                count++;
            }
            OnInterval(500, () =>
            {
            Start:
                try
                {
                    if (count > -1)
                    {
                        temp = entCN[x];
                        Utilities.RawSayAll(bot + temp.Name + " [^3" + temp.Call<int>("getentitynumber") + "^7] ");
                        x++;
                        count--;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    errc++;
                    x++;
                    if (errc > 18)
                    {
                        Utilities.RawSayAll(bot + "^1Error.");
                        return false;
                    }
                    else
                    {
                        goto Start;
                    }
                }
            });
        }

        public void findimmunes()
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                foreach (Entity e in Playerz)
                {
                    if (isImmune(e, e.Name) == true)
                    {
                        if (!immuneguid.Contains(e.GUID))
                        {
                            immuneguid.Add(e.GUID);
                        }
                    }
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public string getRank(Entity user)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                string outstringF = "User";
                bool readgroups = false;

                string path = @"scripts\\" + "SinAdmin\\" + "Admins.cfg";
                string[] configlines = File.ReadAllLines(path);

                foreach (string s in configlines)
                {
                    if (readgroups == true)
                    {
                        if (s.Contains(user.GUID.ToString()))
                        {
                            string temp = s;
                            string outstring = "";
                            bool sdel = false;
                            foreach (char c in temp)
                            {
                                if (c == '=')
                                {
                                    sdel = true;
                                }
                                if (sdel != true)
                                {
                                    outstring += c;
                                }
                            }
                            outstring = outstring.Replace("]", "");
                            outstring = outstring.Replace("[", "");
                            string[] tempout = outstring.Split(new string[] { ";:;" }, StringSplitOptions.None);
                            outstringF = tempout[0];
                            break;
                        }
                    }
                    else if (s.ToLower() == "[adminlist]")
                    {
                        readgroups = true;
                    }
                }



                return outstringF;
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
            return null;
        }

        #endregion

        #region GroupWelcome
        // Group welcomer is mostely useful to prevent brain damage during SnD
        public void GroupWelcome(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\GroupWelcome.txt";
                if (!File.Exists(path))
                {
                    string[] contents = new string[] { "[Names]=" };
                    File.WriteAllLines(path, contents);
                }

                string[] Ponline = File.ReadAllLines(path);

                string check = "";

                foreach (string s in Ponline)
                {
                    check += " " + s + " ";
                }

                if (!check.Contains(player.Name))
                {
                    addToPlayerz(player);
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void addToPlayerz(Entity player)
        {
#if dev
            Log.Write(LogLevel.Debug, "Add2Playerz");
#endif
            AfterDelay(750, () =>
            {
                StreamWriter erros = new StreamWriter(fs);
                try
                {
                    String newValue = "";
                    string countryX = this.Ip2Country.GetCountry(player.IP.ToString().Split(new char[] { ':' })[0]);
                    foreach (KeyValuePair<string, string> pair in this.IsoCountries)
                    {
                        if (pair.Value == countryX)
                        {
                            newValue = pair.Key;
                            break;
                        }
                    }
                    string rank = getRank(player);
                    rank = rank.Replace("[", "");
                    rank = rank.Replace("]", "");

                    string connecttemp = "";
                    connecttemp = cm.Replace("<client>", player.Name);
                    connecttemp = connecttemp.Replace("<rank>", rank);
                    connecttemp = connecttemp.Replace("<country>", newValue);

                    Utilities.RawSayAll(bot + connecttemp);

                    Log.Write(LogLevel.All, "Connected:" + player.Name + "(" + player.GUID + ")");
                    string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\GroupWelcome.txt";

                    StreamWriter sw = new StreamWriter(path, true);
                    sw.WriteLine(player.Name, true);
                    sw.Close();
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
            });
        }

        // You fucking whore
        public void RogueSuxAtCoding()
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\GroupWelcome.txt";
                File.Delete(path);
                //File.WriteAllBytes(path, new byte[0]);
                StreamWriter sw = new StreamWriter(path, true);
                sw.WriteLine("", true);
                sw.Close();
                StreamWriter sw2 = new StreamWriter(path, true);
                foreach (Entity ent in Playerz)
                {
                    sw2.WriteLine(ent.Name, true);
                    sw2.Flush();
                }
                sw2.Close();
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }
        #endregion

        #region Notifys
        public void notifys()
        {

            StreamWriter erros = new StreamWriter(fs);
            //try
            //{
            //    OnNotify("round_win", winner =>
            //       {
            //           sayAsBot(gypsy, "Round ended, Winner: " + winner);
            //       });
            //}
            //catch { }

            //try
            //{
            //    OnNotify("round_switch", switchtype =>
            //        {
            //            sayAsBot(gypsy, "Round Switch: " + switchtype);
            //        });
            //}
            //catch { }

            ////gave_killstreak
            ////killcam_ended
            // try
            // {
            //     OnNotify("gave_killstreak", param =>
            //     {
            //         sayAsBot(gypsy, "Killstreak: " + param);
            //     });
            // }
            // catch { }

            //OnNotify("game_over", () =>
            //{
            //    File.WriteAllText(@"scripts\\sinadmin\\" + cfgname+ "\\killstreak.txt", String.Empty);
            //    Utilities.RawSayAll("GAME OVER");
            //});

            //OnNotify("end", () =>
            //{
            //    File.WriteAllText(@"scripts\\sinadmin\\" + cfgname+ "\\killstreak.txt", String.Empty);
            //    Utilities.RawSayAll("END");
            //});




            try
            {
                if (!dvarCheck("botmod"))
                {
                    OnNotify("player_spawned", (param) =>
                    {

                        //sayAsBot(gypsy, param.ToString());
                        string x = param.ToString();
                        string[] scrub = x.Split(':');
                        string[] temp = scrub[2].Split(']');
                        string xx = Regex.Replace(temp[0], @"\s", "");
                        int cNum = Convert.ToInt32(xx);
                        //  sayAsBot(gypsy, xx);
                        Entity datFag = entCN[cNum];

                        if (dvarCheck("AntiCamp"))
                        {
                            if (!_prematch)
                            {
                                datFag.Call("stunplayer", 0);
                            }
                        }
                        //sayAsBot(gypsy, "Spawned: " + datFag.Name);
                        if (dvarCheck("spawnprotection"))
                        {
                            // sayAsBot(gypsy, datFag.Name);
                            if (!spawned.Contains(datFag))
                                spawned.Add(datFag);
                        }
                    });
                }
                else
                {
                    OnNotify("player_spawned", (param) =>
                    {
                        try
                        {
                            string path = @"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\bots.txt";
                            string xx = param.ToString();
                            string[] scrub = xx.Split(':');
                            string[] tempx = scrub[2].Split(']');
                            string xxx = Regex.Replace(tempx[0], @"\s", "");
                            int cNum = Convert.ToInt32(xxx);
                            //  sayAsBot(gypsy, xx);
                            Entity datfag = null;
                            foreach (Entity ent in BotList)
                            {
                                if (ent.EntRef == cNum)
                                    datfag = ent;
                            }
                            string[] place = File.ReadAllLines(path);
                            Random rnd = new Random();

                            int x = rnd.Next(0, place.Length);
                            string temp = place[x];
                            // Utilities.SayAll(temp);
                            string[] final = temp.Split(' ');
                            Vector3 p2;
                            p2.X = float.Parse(final[0], CultureInfo.InvariantCulture.NumberFormat);
                            // Utilities.SayAll(p2.X.ToString());
                            p2.Y = float.Parse(final[1], CultureInfo.InvariantCulture.NumberFormat);
                            //  Utilities.SayAll(p2.Y.ToString());
                            p2.Z = float.Parse(final[2], CultureInfo.InvariantCulture.NumberFormat);
                            //Utilities.SayAll(p2.Z.ToString());
                            datfag.Call("setorigin", p2);
                            datfag.Call("freezecontrols", true);
                            datfag.TakeAllWeapons();
                        }
                        catch { }

                    });
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

            //destroyed_car
            //try
            //{
            //    player.OnNotify("destroyed_car", delegate(Entity del)
            //    {
            //        sayAsBot(del, "^5Destroyed Car: ^1" + del.Name);
            //    });
            //}
            //catch { }

            //try
            //{
            //    player.OnNotify("killcam_ended", delegate(Entity del)
            //    {
            //        sayAsBot(del, "Killcam Ended: " + del.Name);
            //    });
            //}
            //catch { }
            try
            {
                OnNotify("prematch_done", () =>
                {
                    var mapname = Call<string>("getdvar", "mapname");
                    //sayAsBot(gypsy, "^5Round Started!");
                    if (dvarCheck("spawnprotection"))
                    {
                        foreach (Entity mynig in spawned)
                        {
                            //player.Call("hide");
                            mynig.Health = -1;
                            mynig.Call("stunplayer", 1);
                            bool temp = true;
                            OnInterval(100, () =>
                            {
                                if (temp)
                                {
                                    setVision(mynig, mapname);
                                    return true;
                                }
                                return true;
                            });

                            AfterDelay(intDvar("spawnprotectionTime"), () =>
                            {
                                //sayAsBot(gypsy, _spawnproc.ToString());
                                mynig.Health = defaultHealth;
                                mynig.Call("stunplayer", 0);
                                setVision(mynig, mapname);
                            });

                            AfterDelay(intDvar("spawnprotectionTime") + 1000, () =>
                            {
                                temp = false;
                            });
                        }
                    }
                    _prematch = true;
                    foreach (Entity player in Playerz)
                    {
                        if (dvarCheck("antiboltcancel"))
                        {
                            antiBoltCancel(player);
                        }

                        if (dvarCheck("antihardscope") && !dvarCheck("antilag"))
                        {
                            antiHS(floatDvar("maxhardscopetime"), player);
                        }
                        if (dvarCheck("antihalf") && !dvarCheck("antilag"))
                        {
                            antiHalf(player);

                        }
                        if (dvarCheck("antinoscope") && !dvarCheck("antilag"))
                        {
                            antiNoscope(player);
                        }

                        doneAnti.Add(player);
                    }
                });
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

            //host_migration_begin

            //try
            //{
            //    AfterDelay(1000, () =>
            //        {
            //            Notify("host_migration_begin");
            //        });
            //}
            //catch { }

        }
        #endregion

        #region Entity Handling


        private string checkString = "0123456789abcdefghijklmnopqrstuvwxyz[]()'!@#$%^&*,./<>?;:|{}=+-~`_";
        // Run whenever a player connect


        private void connecting(Entity player)
        {
#if dev
            Log.Write(LogLevel.Debug, "Connecting:" + player.Name + " " +player.GUID);
#endif
            StreamWriter erros = new StreamWriter(fs);
            try
            {

                if (player.Name.Contains(bot) && !player.GUID.ToString().StartsWith("76"))
                {
                    rcon("drop " + player.Name);
                    Utilities.RawSayAll(player.Name + " was kicked for bad GUID");
                }
                else
                {
                    if (AntiHax)
                    {
                        player.SetField("MeinName", player.Name);
                        //sayAsBot(player, "Here1");
                        OnInterval(60000, () =>
                        {
                            string nameCheck = player.GetField<string>("MeinName");
                            nameCheck = Regex.Replace(nameCheck, @"\s", "");
                            string dot = ".";

                            if (!(player.Name == player.GetField<string>("MeinName")))
                            {
                                Utilities.ExecuteCommand("banclient " + player.Call<int>("getentitynumber", new Parameter[0]));
                                Utilities.ExecuteCommand("dropclient" + " " + "\"" + player.EntRef + "\"" + " " + "\"" + "Cheerio ^_^" + "\"");
                                return false;
                            }

                            int dots = 0;
                            while (dots != 15)
                            {
                                if (nameCheck == dot)
                                {
                                    Utilities.ExecuteCommand("banclient " + player.Call<int>("getentitynumber", new Parameter[0]));
                                    Utilities.ExecuteCommand("dropclient" + " " + "\"" + player.EntRef + "\"" + " " + "\"" + "Cheerio ^_^" + "\"");
                                    return false;
                                }
                                else
                                {
                                    //sayAsBot(player, "Dots: " + dot);
                                    dot = dot + ".";
                                    dots++;
                                }
                            }


                            //string clientNum = player.EntRef.ToString();

                            //if (clientNum == "0")
                            //{
                            //    Utilities.ExecuteCommand("banclient " + player.Call<int>("getentitynumber", new Parameter[0]));
                            //    Utilities.ExecuteCommand("drop" + " " + "\"" + player + "\"" + " " + "\"" + "Cheerio ^_^" + "\"");
                            //    return false;
                            //}

                            int idkwhat = 1;
                            if (idkwhat == 1)
                            {
                                foreach (char Char in nameCheck.ToLower())
                                {
                                    foreach (char CheckChar in checkString.ToLower())
                                    {
                                        if ((Char == CheckChar))
                                        {
                                            return false;
                                        }
                                    }
                                }
                                Utilities.ExecuteCommand("banclient " + player.Call<int>("getentitynumber", new Parameter[0]));
                                Utilities.ExecuteCommand("dropclient" + " " + "\"" + player.EntRef + "\"" + " " + "\"" + "Cheerio ^_^" + "\"");
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        });
                    }

                    try
                    {
                        StreamReader roguesaderp = new StreamReader(ipd);
                        string ip;
                        while ((ip = roguesaderp.ReadLine()) != null)
                        {
                            string playerip = player.IP.ToString();
                            string[] pIP = playerip.Split(':');
                            playerip = pIP[0];
                            if (ip.Contains(playerip))
                            {
                                Utilities.ExecuteCommand("banclient " + player.EntRef);
                                Utilities.ExecuteCommand("dropclient" + " " + "\"" + player.EntRef + "\"" + " " + "\"" + "Cheerio ^_^" + "\"");
                            }
                        }
                    }

                    catch (Exception error)
                    { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

                }

            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }


        private void c(Entity player)
        {
#if dev
            Log.Write(LogLevel.All, "connected " + player.Name);
#endif

            StreamWriter erros = new StreamWriter(fs);
            if (!player.Name.Contains("bot") && player.GUID.ToString().StartsWith("76"))
            {
                try
                {
                    // Ya know, porn 'n' stuff
                    if (!Playerz.Contains(player))
                    {
                        Playerz.Add(player);
                    }

                    try
                    {
                        badnameshit(player);
                    }
                    catch (Exception error)
                    { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }


                    sortbyCN();

                    if (dvarCheck("clanident"))
                    {
                        if (player.Name.Contains(clantag))
                        {
                            string name = player.Name.Split(new string[] { "||" }, StringSplitOptions.None)[1];
                            writeMemname(player, "^5" + player.Name);
                        }
                    }
                    //CHECK FOR IPBAN

#if dev
                    Log.Write(LogLevel.All, "Firsht check");
#endif
                    try
                    {
                        if (_prematch)
                        {
                            if (!doneAnti.Contains(player))
                            {

                                if (dvarCheck("antiboltcancel"))
                                {
                                    antiBoltCancel(player);
                                }

                                if (dvarCheck("antihardscope") && !dvarCheck("antilag"))
                                {
                                    antiHS(floatDvar("maxhardscopetime"), player);
                                }


                                if (dvarCheck("antihalfscope") && !dvarCheck("antilag"))
                                {
                                    antiHalf(player);

                                }


                                if (dvarCheck("antinoscope") && !dvarCheck("antilag"))
                                {
                                    antiNoscope(player);
                                }
                            }
                        }


                        if (dvarCheck("anticamp"))
                        {
                            antiCamper(player);
                        }
                        //OnInterval(500, () =>
                        //{
                        //    Utilities.SayTo(player, "^1Health ^7" + player.Health);
                        //    return true;
                        //});

#if dev
                        Log.Write(LogLevel.All, "second " + player.Name);
#endif

                        //IF BOTMOD
                        if (dvarCheck("botmod"))
                        {
                            try
                            {

                                player.Call("notifyOnPlayerCommand", new Parameter[] { "fu", "+actionslot 2" });
                                player.OnNotify("fu", delegate(Entity entt)
                                {
                                    string path = @"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\bots.txt";
                                    if (!File.Exists(path))
                                    {
                                        File.WriteAllText(path, String.Empty);
                                    }

                                    OnInterval(2500, () =>
                                    {
                                        string[] place = File.ReadAllLines(path);
                                        Random rnd = new Random();
                                        foreach (Entity ent in BotList)
                                        {

                                            int x = rnd.Next(0, place.Length);
                                            string temp = place[x];
                                            // Utilities.SayAll(temp);
                                            string[] final = temp.Split(' ');
                                            Vector3 p2;
                                            p2.X = float.Parse(final[0], CultureInfo.InvariantCulture.NumberFormat);
                                            // Utilities.SayAll(p2.X.ToString());
                                            p2.Y = float.Parse(final[1], CultureInfo.InvariantCulture.NumberFormat);
                                            //  Utilities.SayAll(p2.Y.ToString());
                                            p2.Z = float.Parse(final[2], CultureInfo.InvariantCulture.NumberFormat);
                                            //Utilities.SayAll(p2.Z.ToString());
                                            ent.Call("setorigin", p2);
                                            ent.Call("freezecontrols", true);
                                            ent.TakeAllWeapons();
                                        }
                                        return true;
                                    });
                                });

                                player.Call("notifyOnPlayerCommand", new Parameter[] { "kickb", "+actionslot 3" });
                                player.OnNotify("kickb", delegate(Entity ent)
                                {
                                    for (int g = 0; g < 1000; g++)
                                    {
                                        rcon("kick bot" + g);
                                    }
                                    sayAsBot(player, "^5Kicked Bots!");
                                });

                                player.Call("notifyOnPlayerCommand", new Parameter[] { "clear", "+actionslot 4" });
                                player.OnNotify("clear", delegate(Entity ent)
                                {
                                    string path = @"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\bots.txt";
                                    File.WriteAllText(path, String.Empty);
                                    sayAsBot(player, "Spawn list cleared");
                                });

                                player.Call("notifyOnPlayerCommand", new Parameter[] { "here", "+actionslot 5" });
                                player.OnNotify("here", delegate(Entity ent)
                                {
                                    string path = @"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\bots.txt";
                                    StreamWriter write = new StreamWriter(path, true);
                                    var p2 = player.Origin;
                                    string x = p2.X.ToString();
                                    string y = p2.Y.ToString();
                                    string z = p2.Z.ToString();
                                    Utilities.SayAll(x + " " + y + " " + z);
                                    write.WriteLine(x + " " + y + " " + z);
                                    write.Close();
                                });

                                player.Call("notifyOnPlayerCommand", new Parameter[] { "ohnoes", "+actionslot 6" });
                                player.OnNotify("ohnoes", delegate(Entity ent)
                                {
                                    Entity entity = Utilities.AddTestClient();
                                    if (entity != null)
                                    {
                                        player.SetField("isBot", true);
                                        entity.OnNotify("joined_spectators", delegate(Entity tc)
                                        {
                                            tc.Notify("menuresponse", new Parameter[] { "team_marinesopfor", "allies" });
                                            tc.AfterDelay(500, meh => meh.Notify("menuresponse", new Parameter[] { "changeclass", "class1" }));
                                        });
                                    }
                                    foreach (Entity entt in BotList)
                                    {
                                        Vector3 temp;
                                        temp.X = 100;
                                        temp.Y = 100;
                                        temp.Z = 100;
                                        Vector3 X;
                                        X.X = player.Origin.X + temp.X;
                                        X.Y = player.Origin.Y + temp.Y;
                                        X.Z = player.Origin.Z + temp.Z;
                                        entt.Call("setorigin", X);
                                    }
                                });
                            }
                            catch (Exception error)
                            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                        }



                        if (dvarCheck("advertising"))
                        {
                            HudElem LabelF = HudElem.CreateFontString(player, "hudbig", 0.6f);
                            LabelF.SetPoint("BOTTOMRIGHT", "BOTTOMRIGHT");
                            LabelF.SetText("^5Sin^0Admin^7::^1v3");
                            LabelF.HideWhenInMenu = true;
                        }

                        if (dvarCheck("antiaimbot"))
                        {
                            player.SetField("headshots", 0);
                            player.SetField("neckshots", 0);
                            player.SetField("torso_upper", 0);
                            player.SetField("torso_lower", 0);
                            player.SetField("right_arm_upper", 0);
                            player.SetField("right_arm_lower", 0);
                            player.SetField("left_arm_upper", 0);
                            player.SetField("left_arm_lower", 0);
                            player.SetField("left_leg_upper", 0);
                            player.SetField("left_leg_lower", 0);
                            player.SetField("right_leg_upper", 0);
                            player.SetField("right_leg_lower", 0);
                        }

                        if (dvarCheck("tkmode"))
                        {
                            OnInterval(300, () =>
                            {
                                player.GiveWeapon("throwingknife_mp");
                                string weapon = player.CurrentWeapon;
                                player.Call("setWeaponAmmoStock", weapon, "0");
                                player.Call("setWeaponAmmoClip", weapon, "0");
                                return true;
                            });
                        }


                        //if (Antihm)
                        //{
                        //    try
                        //    {
                        //        string norm = player.Call<string>("getnormalhealth");
                        //        Utilities.SayTo(player, "Normal health = " + norm);
                        //    }
                        //    catch (Exception error)
                        //    { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

                        //    try
                        //    {
                        //        player.Call("setnormalhealth", "15");
                        //    }
                        //    catch (Exception error)
                        //    { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                        //}

                        string[] mlines = File.ReadAllLines(mutepath);
                        if (mlines.Contains(player.GUID.ToString()))
                        {
                            mutedplayers.Add(player.GUID);
                        }

                        string[] blines = File.ReadAllLines(@"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\blocked.txt");
                        if (blines.Contains(player.GUID.ToString()))
                        {
                            blockedplayers.Add(player.GUID);
                        }


                        if (dvarCheck("explosivebullets"))
                        {
                            Utilities.RawSayTo(player, "^6Explosive ^0bullets ^1Loaded");
                            ebshot(player);
                        }


                        if (dvarCheck("hudadv"))
                        {
                            HudElem LabelB = HudElem.CreateFontString(player, "hudbig", 0.8f);
                            LabelB.SetPoint(stringDvar("POSITION"), stringDvar("POSITION"), intDvar("xCoord"), intDvar("yCoord"));
                            LabelB.SetText(serverName);
                            LabelB.HideWhenInMenu = true;

                            HudElem LabelC = HudElem.CreateFontString(player, "hudbig", 0.8f);
                            LabelC.SetPoint(stringDvar("POSITION"), stringDvar("POSITION"), intDvar("xCoord"), intDvar("yCoord") + 20);
                            LabelC.SetText(stringDvar("websitename"));
                            LabelC.HideWhenInMenu = true;

                        }


                        if (dvarCheck("emptypistolsecondary"))
                        {
                            player.TakeWeapon("stinger_mp");
                            player.Call("giveweapon", "iw5_44magnum_mp");
                            player.Call("setWeaponAmmoStock", "iw5_44magnum_mp", "0");
                            player.Call("setWeaponAmmoClip", "iw5_44magnum_mp", "0");
                            OnInterval(1000, () =>
                            {
                                player.Call("setWeaponAmmoStock", "iw5_44magnum_mp", "0");
                                player.Call("setWeaponAmmoClip", "iw5_44magnum_mp", "0");
                                return true;
                            });

                        }


                        // Changes team names to the custom ones in the config... I THINK
                        changeTeamNames(player);
                        // Group welcomer haz no nuggets
                        GroupWelcome(player);
                        // Logger, duh
                        log(player);
                        // K den
                        if (dvarCheck("sndkillstreak"))
                        {
                            player.SetField("killstreak", 0);
                            // HAH GAAAAAAAAAY
                            killstreakHUD(player);
                            // Go read a book
                            spawnKillstreak(player);
                        }
                        // IMMA NINJA
                        if (dvarCheck("spy"))
                        {
                            addSpy(player);
                        }
                        // Promod thingy

                    
                            CustomDvar(player, stringDvar("customdvar"));
                        


                        // getRank will find the player rank using guid, going through the config, and return the proper string
                        string Rank = getRank(player);

                        // "User" (yes with a capital 'U') is returned if guid is not in the config
                        if (Rank != "User")
                        {
                          
                            foreach (string line in ablines)
                            {
                                if (line.Contains("[" + Rank + "=Commands="))
                                {
                                    string[] temp0 = line.Split(new string[] { ";:;" }, StringSplitOptions.None);
                                    string temp1 = temp0[0];
                                    temp1 = temp1.Replace("[" + Rank + "=Commands=", "");
                                    temp1 = temp1.Remove(temp1.Length - 1);
                                    if (!admindisplaylist.Contains(player.Name + temp1))
                                    {
                                        admindisplaylist.Add(player.Name + temp1);
                                        admindisplaylistV2.Add(player.Name + ";:;" + temp1);
                                    }
                                }
                            }
                        }

                        if (player.Name.ToLower() == "sinx||black" || player.Name.ToLower() == "sinx||bio")
                        {
                            Bio = true;
                        }

                        if (dvarCheck("antiplant"))
                        {
                            player.OnInterval(500, (ent) =>
                            {
                                if (player.CurrentWeapon.Equals("briefcase_bomb_mp"))
                                {
                                    player.TakeWeapon("briefcase_bomb_mp");
                                    //Switch back the the player's weapon here.....Kinda optional
                                    player.Call("iprintlnbold", "^1You are not allowed to plant the bomb!");
                                }
                                return true;
                            });
                        }


                        if (dvarCheck("antispray"))
                        {
                            player.OnNotify("weapon_fired", (dude, weaponName) =>
                            {
                                if (weaponName.ToString().Contains("dragunov") || weaponName.ToString().Contains("rsas") || weaponName.ToString().Contains("barrett"))
                                {
                                    dude.Call("stunplayer", 1);
                                    AfterDelay(500, () =>
                                    {
                                        dude.Call("stunplayer", 0);
                                    });
                                }
                            });
                        }
                    }
                    catch (Exception error)
                    { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

                    // Used to track player name changes
                    trackPlayerz(player);

                    // Used to track admins who have logged in
                    if (dvarCheck("uselogin"))
                    {
                        spawnLoginCheck(player);
                    }

                    // Action taken on player spawned (used to set default health over, and over, and over, and over again)
                    player.SpawnedPlayer += new Action(() =>
                    {
                        defaultHealth = player.Health;
                        if (defaultHealth <= 0)
                        {
                            defaultHealth = 30;
                        }
                    });

                    if (dvarCheck("namecharacterfilter"))
                    {
                        nameformatcheck(player);
                    }

                    try
                    {
                        player.OnNotify("weapon_fired", (self, weapon) =>
                        {

                            if (dvarCheck("antinorecoil"))
                            {
                                string weapons = weapon.ToString();
                                if ((weapons.Contains("iw5_l96a1") || weapons.Contains("iw5_msr")) && !weapons.Contains("silen"))
                                {
                                    recoildet(player);
                                }
                            }

                            swapAmmo(player);
                            if (dvarCheck("unlimitedammo"))
                            {
                                unlimitedammo(player);
                            }

                        });
                    }

                    catch (Exception error)
                    { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }



                    try
                    {
                        if (maxclients - intDvar("reservedslots") < Playerz.Count + BotList.Count && getRank(player) == "User")
                        {
                            int temp = 0;
                            foreach (Entity toget in Playerz)
                            {
                                if (getRank(toget) != "User")
                                {
                                    temp++;
                                }
                            }
                            if (temp < intDvar("reservedslots"))
                            {
                                AfterDelay(1000, () =>
                                {
                                    reservedComm2idkwhy(player, "Kicked to free a slot for server members, please come back!");
                                });
                            }
                        }
                    }
                    catch (Exception error)
                    { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }


                }
                catch { }
            }

            else if (player.Name.Contains("bot"))
            {
                try
                {
                    if (dvarCheck("botmod"))
                    {
                        BotList.Add(player);
                        Utilities.RawSayAll(player.Name);
                    }
                    else
                    {
                        Utilities.ExecuteCommand("banclient " + player.Call<int>("getentitynumber", new Parameter[0]));
                        Utilities.ExecuteCommand("dropclient" + " " + "\"" + player.EntRef + "\"" + " " + "\"" + "Cheerio ^_^" + "\"");
                    }
                }
                catch
                {
                    Utilities.ExecuteCommand("banclient " + player.Call<int>("getentitynumber", new Parameter[0]));
                    Utilities.ExecuteCommand("dropclient" + " " + "\"" + player.EntRef + "\"" + " " + "\"" + "Cheerio ^_^" + "\"");
                }
            }
            else
            {
                Utilities.ExecuteCommand("banclient " + player.Call<int>("getentitynumber", new Parameter[0]));
                Utilities.ExecuteCommand("dropclient" + " " + "\"" + player.EntRef + "\"" + " " + "\"" + "Cheerio ^_^" + "\"");
            }
        }



        // Run whenever a player disconnects
        private void dc(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                if (!player.Name.Contains("bot"))
                {
                    // Removes player from the player list
                    Playerz.Remove(player);

                    guid = player.GUID;
                    // Warn file used to keep track of which players are and arent warned.
                    string path = @"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\currentwarns.warn";
                    string[] lines = File.ReadAllLines(path);

                    bool found = false;
                    foreach (string s in lines)
                    {
                        if (s.Contains(guid.ToString()))
                        {
                            found = true;
                            break;
                        }
                    }

                    if (found == true)
                    {
                        int counter = 0;
                        foreach (string s in lines)
                        {
                            if (s.StartsWith(guid.ToString()))
                            {
                                lines[counter] = "";
                                File.Delete(path);
                                File.WriteAllLines(path, lines);
                                break;
                            }
                            counter++;
                        }
                    }

                    //Remove from Lists
                    try
                    {
                        Admins.Remove(player);
                        immuneguid.Remove(player.GUID);
                        admindisplaylist.Remove(player.Name);
                        admindisplaylistV2.Remove(player.Name);
                        mutedplayers.Remove(player.GUID);
                        blockedplayers.Remove(player.GUID);
                        LoggedIn.Remove(player);
                        spawned.Remove(player);
                        Axis.Remove(player);
                        Allies.Remove(player);
                    }
                    catch (Exception error)
                    { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                    if (dvarCheck("uselogin"))
                    {
                        if (LoggedIn.Contains(player))
                        {
                            try
                            {
                                LoggedIn.Remove(player);
                                string logpath = @"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\LoggedIn.txt";
                                string[] loglines = File.ReadAllLines(logpath);
                                int counter = -1;
                                foreach (string st in loglines)
                                {
                                    counter++;
                                    if (st.StartsWith(player.GUID.ToString() + "=" + player.IP.ToString()))
                                    {
                                        loglines[counter] = "";
                                        File.WriteAllLines(logpath, loglines);
                                        break;
                                    }
                                }
                            }
                            catch (Exception error)
                            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                        }
                    }

                    foreach (string s in admindisplaylist)
                    {
                        if (s.StartsWith(player.Name))
                        {
                            admindisplaylist.Remove(s);
                        }
                    }
                }
                else
                {
                    BotList.Remove(player);
                }
            }

            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

        }


        public void setPlayer(Entity player, string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                //!setplayer [case] [player] [value]
                char x = ' ';
                string[] splitteh = message.Split(x);
                string msg = splitteh[1];
                Entity nub = FindByName(splitteh[2]);
                switch (msg.ToLower())
                {

                    case "clientdvar":
                        string dvar = splitteh[3];
                        string value = splitteh[4];
                        nub.SetClientDvar(dvar, value);
                        Utilities.RawSayTo(player, pm + " ^5Client dvar ^1" + dvar + " ^0was set to ^1" + value);
                        break;

                    case "getwepname":
                        Utilities.RawSayTo(player, nub.CurrentWeapon);
                        break;

                    case "setperk":
                        string perk = splitteh[3];
                        nub.SetPerk(perk, true, false);
                        Utilities.RawSayTo(player, pm + " ^5Perk  ^1" + perk + " ^0was given to ^1" + nub.Name);
                        break;

                    case "setfield":
                        string field = splitteh[3];
                        string param = splitteh[4];
                        nub.SetField(field, param);
                        Utilities.RawSayTo(player, pm + " ^5Field  ^1" + field + " ^0was set to ^1" + param);
                        break;

                    case "givewep":
                        string wep = splitteh[3];
                        nub.GiveWeapon(wep);
                        Utilities.RawSayTo(player, pm + " ^5Weapon  ^1" + wep + " ^0was given to ^1" + nub.Name);
                        break;

                    case "switchwep":
                        string wepo = splitteh[3];
                        nub.SwitchToWeapon(wepo);
                        Utilities.RawSayTo(player, pm + "^5" + nub.Name + " ^0Weapon was switched to  ^1" + wepo);
                        break;

                    case "takewep":
                        string wepon = splitteh[3];
                        nub.TakeWeapon(wepon);
                        Utilities.RawSayTo(player, pm + "^5" + nub.Name + " ^0Weapon was taken  ^1" + wepon);
                        break;

                    case "takeallwep":
                        nub.TakeAllWeapons();
                        Utilities.RawSayTo(player, pm + "^5" + nub.Name + " ^1Weapons were taken");
                        break;
                    case "call":
                        string callVal = splitteh[3];
                        nub.Call("\"" + callVal + "\"");
                        Utilities.RawSayTo(player, pm + "^5" + nub.Name + " ^1Weapons were taken");
                        break;
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        #endregion

        #region Overrides

        public override void OnStartGameType()
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                File.WriteAllText(@"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\killstreak,txt", String.Empty);
                if (dvarCheck("customsvname"))
                {
                    var gametype = Call<string>("getdvar", "g_gametype");
                    string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\Sv_name.cfg";
                    string[] readAll = File.ReadAllLines(path);
                    foreach (string game in readAll)
                    {
                        if (game.Contains(gametype))
                        {
                            string[] splitteh = game.Split('=');
                            base.Call("setdvar", "sv_hostname", splitteh[1]);

                        }
                    }
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }{ }
        }

        public override void OnPlayerDamage(Entity player, Entity inflictor, Entity attacker, int damage, int dFlags, string mod, string weapon, Vector3 point, Vector3 dir, string hitLoc)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                if (!dvarCheck("botmod"))
                {
                    /*
                    if (!weapon.Contains("iw5_l96a1") || weapon.Contains("iw5_msr"))
                    {
                        player.Health = defaultHealth + damage;
                        damage = 0;
                    }
                     * */

                    if (Faller)
                        if ((mod == "MOD_FALLING"))
                        {
                            player.Health += 1000 + damage;
                            player.Health = defaultHealth;
                        }

                    if (dvarCheck("anticrtk"))
                    {
                        if (weapon == "throwingknife_mp")
                        {
                            if (attacker.Origin.DistanceTo2D(player.Origin) < 150)
                            {
                                player.Health += 1000 + damage;
                                player.Health = defaultHealth;
                                Utilities.SayTo(attacker, "^1No: CRTK " + player.Health);
                            }
                        }
                    }
                    var xD = Call<string>("getdvar", "g_gametype").ToLower();
                    //Log.Write(LogLevel.All, "player: " + player.Name + ", inflictor: " + inflictor.Name + ", attacker: " + attacker.Name + ", damage: " + damage.ToString() + ", dflags: " + dFlags.ToString() + ", mod: " + mod + ", weapon: " + weapon + ", hitloc: " + hitLoc);
                    //Log.Write(LogLevel.All, "vector 3 point xyz: " + point.X.ToString() + ", " + point.Y.ToString() + ", " + point.Z.ToString() + " : vector 3 dir xyz: " + dir.X.ToString() + ", " + dir.Y.ToString() + ", " + dir.Z.ToString());
                    if (hitmarkergods.Contains(player.GUID))
                    {
                        player.Health = damage + defaultHealth;
                    }
                    //bool flash = false;
                    //bool concussion = false;

                    //if (weapon.Contains("flash"))
                    //{
                    //    flash = true;
                    //    Utilities.RawSayAll("Flash");
                    //}

                    //if (weapon.Contains("concussion"))
                    //{
                    //    concussion = true;
                    //    Utilities.RawSayAll("Concussion");
                    //}

                    //if (!weapon.Contains("flash"))
                    //{
                    //    flash = false;
                    //    Utilities.RawSayAll("Not Flash");
                    //}

                    //if (!weapon.Contains("concussion"))
                    //{
                    //    concussion = false;
                    //    Utilities.RawSayAll("Not Concussion " + weapon);
                    //}

                    if (dvarCheck("Forgive"))
                    {
                        if (xD == "sd")
                        {
                            gypsy = attacker;
                            int currentForg = attacker.GetField<int>("forgive");
                            attacker.SetField("forgive", currentForg + 100);
                            player.SetField("killer", attacker.Name);
                            currentForg = attacker.GetField<int>("forgive");
                            if (currentForg == 300)
                            {
                                Utilities.ExecuteCommand("dropclient " + attacker.Call<int>("getentitynumber") + " \"" + "Anti-Teamkiller: NO TEAMKILL!" + "\"");
                            }
                        }
                    }

                    if (dvarCheck("antihitmarker"))
                    {
                        string noobs = player.GetField<string>("sessionteam");
                        string enemys = attacker.GetField<string>("sessionteam");
                        if (noobs != enemys)
                        {
                            var oldHealth = player.Health;
                            player.Health /= 2;
                            player.Notify("damage", (oldHealth - player.Health), player, new Vector3(0, 0, 0), new Vector3(0, 0, 0), hitLoc, "", "", "", 0, weapon);
                        }

                    }

                    if (dvarCheck("hardcoreteamkill"))
                    {

                        if (xD.ToString() != "dm")
                        {
                            //sayAsBot(player, xD.ToString());
                            if (weapon.Contains("concussion") || weapon.Contains("flash"))
                            {
                                //Utilities.SayAll("lol");
                            }
                            else
                            {
                                string noob = player.GetField<string>("sessionteam");
                                string enemy = attacker.GetField<string>("sessionteam");
                                string name = attacker.Name;
                                string noname = player.Name;
                                //Utilities.SayAll("gOT HERE");
                                //Utilities.SayAll(name + " " + noname);
                                //Utilities.SayAll(noob + " " + enemy);
                                if (!name.Contains(noname))
                                {
                                    if (noob.Contains(enemy))
                                    {
                                        //Utilities.SayAll("gOT HERE");
                                        player.Health = defaultHealth;
                                        if (!(player.Health == defaultHealth))
                                        {
                                            player.Health = defaultHealth;
                                        }
                                        //damage = 0;
                                        Utilities.RawSayTo(attacker, pm + "^1No Attacking Team");

                                    }
                                    //suicide();
                                }
                            }

                        }
                    }
                    try
                    {
                        if (player.GetField<string>("sessionteam") != attacker.GetField<string>("sessionteam"))
                        {

                            //if (knifing == false && mod.ToString() == "MOD_MELEE")
                            //{
                            //    player.Health = damage + defaultHealth;
                            //}
                            if (hitmarkergmlist.Contains(player.GUID))
                            {
                                player.Health = defaultHealth;
                                player.SetField("killstreak", player.GetField<int>("killstreak") + 1);
                                if (player.GetField<int>("killstreak") == 100)
                                {
                                    rcon("fast_restart");
                                }
                            }
                        }
                    }

                    catch (Exception error)
                    { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                    base.OnPlayerDamage(player, inflictor, attacker, damage, dFlags, mod, weapon, point, dir, hitLoc);
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public override BaseScript.EventEat OnSay3(Entity player, BaseScript.ChatType type, string name, ref string message)
        {

            if (gainedaccess.Contains(player))
            {
                justvoted = true;
            }

            StreamWriter erros = new StreamWriter(fs);
            mesRep = "";

            if (message.Contains('='))
            {
                mesRep = message.ToLower();

                if ((mesRep.Contains("=ip") || mesRep.Contains("=guid") || mesRep.Contains("=rank")) && name != "ghosty1234567890.........." && isAllowed(player, getRank(player), "inforep"))
                {
                    try
                    {
                        infoReplace(player, message);
                        return EventEat.EatGame;
                    }
                    catch (Exception error)
                    { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                }
            }
            else if (message.Contains("<r") && message.Contains("\\r>") && name != "ghosty1234567890.........." && !message.ToLower().StartsWith("!setalias ") && dvarCheck("removecolours") == false)
            {
                try
                {
                    rainbowText(player, message);
                    return EventEat.EatGame;
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
            }

            bool ghosty = false;
            if (name == "ghosty1234567890..........")
            {
                name = player.Name;
                ghosty = true;
            }

            messages = message.ToString();
            if (isMuted(player) == true && !message.StartsWith("!") && !message.StartsWith("@"))
            {
                Utilities.RawSayTo(player, pm + "^4You are muted, you cannot speak.");
                return EventEat.EatGame;
            }
            else if (message.StartsWith("!") && isBlocked(player) == true && !message.ToLower().StartsWith("!unblock") && !message.ToLower().StartsWith("!remove") && !message.ToLower().StartsWith("!login"))
            {
                Utilities.RawSayTo(player, pm + "^1Your commands have been blocked. ^3Administrators can still access the !remove & !unblock commands.");
                return EventEat.EatGame;
            }

            if (message.StartsWith("/") || message.StartsWith("\\"))
            {
                adminchat(player, message);
                return EventEat.EatGame;
            }
            else if (message.StartsWith("#"))
            {
                pmuser(player, player.Name.Split(' ')[0], message.Substring(1));
                return EventEat.EatGame;
            }

            Logger(player, messages);

            if (type == ChatType.Team && dvarCheck("ModdedTeamChat") == true)
            {
                sayTeam(player, message);
                return EventEat.EatGame;
            }

            showalias = false;

            try
            {
                if (aliasplayersv2.Contains(player.Name))
                {
                    showalias = true;

                    stringalias = "";

                    foreach (string aliasstring in aliasplayers)
                    {
                        if (aliasstring.StartsWith(player.Name + "="))
                        {
                            string[] aliastemp = aliasstring.Split('=');
                            stringalias = aliastemp[1];
                            if (stringalias.StartsWith("<r") && stringalias.EndsWith("\\r>"))
                            {
                                try
                                {

                                    stringalias = stringalias.Substring(2);
                                    stringalias = stringalias.Remove(stringalias.Length - 3);

                                    if (stringalias.Split(new string[] { "{\\0}" }, StringSplitOptions.None)[0].Contains("{0}"))
                                    {
                                        for (int x = 0; x <= 999; x++)
                                        {
                                            try
                                            {
                                                string temp = stringalias.Split(new string[] { "{\\" + x.ToString() + "}" }, StringSplitOptions.None)[0].Split(new string[] { "{" + x.ToString() + "}" }, StringSplitOptions.None)[1];

                                                string temp2 = temp;

                                                if (temp.Contains("<c>"))
                                                {
                                                    string colours = temp.Split(new string[] { "<c>" }, StringSplitOptions.None)[1];
                                                    temp = temp.Split(new string[] { "<c>" }, StringSplitOptions.None)[0];

                                                    // The pirates go aaarr
                                                    // Math pirate go chaaarr?
                                                    
                                                    char[] chaaaarrr = new char[colours.Length];

                                                    for (int xi = colours.Length - 1; xi >= 0; xi--)
                                                    {
                                                        try
                                                        {
                                                            chaaaarrr[xi] = colours.ToCharArray()[xi];
                                                        }
                                                        catch
                                                        { }
                                                    }

                                                    int[] sendme = new int[colours.Length];

                                                    int counter = -1;

                                                    foreach (char c in chaaaarrr)
                                                    {
                                                        counter++;
                                                        try
                                                        {
                                                            if (c != ';' && c != ':')
                                                            {
                                                                sendme[counter] = Convert.ToInt32(c.ToString());
                                                            }
                                                            else if (c == ';')
                                                            {
                                                                sendme[counter] = 98;
                                                            }
                                                            else if (c == ':')
                                                            {
                                                                sendme[counter] = 99;
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            try
                                                            {
                                                                sendme[counter] = 7;
                                                            }
                                                            catch
                                                            { }
                                                        }
                                                    }

                                                    if (temp.EndsWith("<s>"))
                                                    {
                                                        int[] sendme2 = new int[sendme.Count() + 1];

                                                        int counter2 = 1;

                                                        sendme2[0] = 100;

                                                        foreach (int i in sendme)
                                                        {
                                                            sendme2[counter2] = i;

                                                            counter2++;
                                                        }

                                                        temp = temp.Remove(temp.Length - 3);

                                                        temp = makeRainbow(temp, sendme2);
                                                    }
                                                    else
                                                    {
                                                        temp = makeRainbow(temp, sendme);
                                                    }
                                                }
                                                else
                                                {
                                                    int[] intarr = new int[6];

                                                    for (int xin = 0; xin < 6; xin++)
                                                    {
                                                        try
                                                        {
                                                            intarr[xin] = xin + 1;
                                                        }
                                                        catch
                                                        { }
                                                    }

                                                    temp = makeRainbow(temp, intarr);
                                                }

                                                stringalias = stringalias.Replace("{" + x.ToString() + "}" + temp2 + "{\\" + x.ToString() + "}", temp);
                                            }
                                            catch
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (stringalias.Contains("<c>"))
                                        {
                                            string colours = stringalias.Split(new string[] { "<c>" }, StringSplitOptions.None)[1];
                                            stringalias = stringalias.Split(new string[] { "<c>" }, StringSplitOptions.None)[0];

                                            // The pirates go aaarr
                                            // Math pirate go chaaarr?
                                            char[] chaaaarrr = new char[colours.Length];

                                            for (int x = colours.Length - 1; x >= 0; x--)
                                            {
                                                try
                                                {
                                                    chaaaarrr[x] = colours.ToCharArray()[x];
                                                }
                                                catch
                                                { }
                                            }

                                            int[] sendme = new int[colours.Length];

                                            int counter = -1;

                                            foreach (char c in chaaaarrr)
                                            {
                                                counter++;
                                                try
                                                {
                                                    if (c != ';' && c != ':')
                                                    {
                                                        sendme[counter] = Convert.ToInt32(c.ToString());
                                                    }
                                                    else if (c == ';')
                                                    {
                                                        sendme[counter] = 98;
                                                    }
                                                    else if (c == ':')
                                                    {
                                                        sendme[counter] = 99;
                                                    }
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        sendme[counter] = 7;
                                                    }
                                                    catch
                                                    { }
                                                }
                                            }

                                            if (stringalias.EndsWith("<s>"))
                                            {
                                                int[] sendme2 = new int[sendme.Count() + 1];

                                                int counter2 = 1;

                                                sendme2[0] = 100;

                                                foreach (int i in sendme)
                                                {
                                                    sendme2[counter2] = i;

                                                    counter2++;
                                                }

                                                stringalias = stringalias.Remove(stringalias.Length - 3);

                                                stringalias = makeRainbow(stringalias, sendme2);
                                            }
                                            else
                                            {
                                                stringalias = makeRainbow(stringalias, sendme);
                                            }

                                            /*
                                             * // ALTERNATIVE FUCKING PIECE OF SHIT... PROBABLY MORE EFFICIENT BUT I DON'T GIVE A FUUUUUUCK
                                             * 
                                             * 
                                             * string colours = stringalias.Split(new string[] { "<c>" }, StringSplitOptions.None)[1];
                                            stringalias = stringalias.Split(new string[] { "<c>" }, StringSplitOptions.None)[0];

                                            char[] schar = colours.ToCharArray();

                                            int[] coloursArr = new int[schar.Count()];

                                            int counter = -1;

                                            foreach (char c in schar)
                                            {
                                                counter++;
                                                coloursArr[counter] = Convert.ToInt32(c.ToString());
                                            }

                                            stringalias = makeRainbow(stringalias, coloursArr);
                                             * */
                                        }
                                        else
                                        {
                                            int[] intarr = new int[6];

                                            for (int x = 0; x < 6; x++)
                                            {
                                                try
                                                {
                                                    intarr[x] = x + 1;
                                                }
                                                catch
                                                { }
                                            }

                                            stringalias = makeRainbow(stringalias, intarr);
                                        }
                                    }
                                }
                                catch (Exception error)
                                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                            }
                            break;
                        }
                    }

                    if (!player.IsAlive)
                    {
                        if (!message.StartsWith("!") && !message.StartsWith("@") && (!message.Contains("^") || dvarCheck("removecolours") == false))
                        {
                            if (dvarCheck("deadchat"))
                            {
                                if (player.GetField<string>("sessionteam") == "spectator")
                                {
                                    Utilities.RawSayAll("(Spectator)" + stringalias + "^7: " + message);
                                }
                                else
                                {
                                    Utilities.RawSayAll("(Dead)" + stringalias + "^7: " + message);
                                }
                                return BaseScript.EventEat.EatGame;
                            }
                        }
                    }
                    else
                    {
                        if (!message.StartsWith("!") && !message.StartsWith("@") && (!message.Contains("^") || dvarCheck("removecolours") == false))
                        {
                            string team = player.GetField<string>("sessionteam");
                            foreach (Entity en in Playerz)
                            {
                                if (en.GetField<string>("sessionteam") == team)
                                {
                                    Utilities.RawSayTo(en, "^8" + stringalias + "^7: " + message);
                                }
                                else
                                {
                                    Utilities.RawSayTo(en, "^9" + stringalias + "^7: " + message);
                                }
                            }
                            //Utilities.RawSayAll("^8" + stringalias + "^7: " + message);
                            return BaseScript.EventEat.EatGame;
                        }
                    }
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

            if (showalias == false)
            {
                if (!player.IsAlive)
                {
                    if (!message.StartsWith("!") && !message.StartsWith("@") && (!message.Contains("^") || dvarCheck("removecolours") == false) && dvarCheck("deadchat"))
                    {
                        if (dvarCheck("deadchat"))
                        {
                            if (player.GetField<string>("sessionteam") == "spectator")
                            {
                                Utilities.RawSayAll("(Spectator)" + player.Name + "^7: " + message);
                            }
                            else
                            {
                                Utilities.RawSayAll("(Dead)" + player.Name + "^7: " + message);
                            }
                            return BaseScript.EventEat.EatGame;
                        }
                    }
                }
            }
            if (message.StartsWith("@"))
            {
                foreach (Entity admin in Admins)
                {
                    if (dvarCheck("spy"))
                    {
                        if (getAlias(name) != "GhostyBeTrippin.........." && aliasname == true)
                        {
                            Utilities.RawSayTo(admin, pm + "^1" + player.Name + " ^7(" + getAlias(name) + "^7)" + ": ^5" + message);
                        }
                        else
                        {
                            Utilities.RawSayTo(admin, pm + "^1" + player.Name + ": ^5" + message);
                        }
                    }
                }
            }
            if (message.StartsWith("!"))
            {
                try
                {

                    foreach (string ali in cmdAl)
                    {
                        string[] aliSplit = ali.Split('=');
                        string tempAl = aliSplit[1];
                        string[] spacSplit = tempAl.Split(' ');
                        string[] msgspl = message.Split(' ');

                        if (message.ToLower().Contains(aliSplit[0]))
                        {
                            message = message.Replace(msgspl[0], tempAl);
                        }
                    }

                    foreach (Entity admin in Admins)
                    {
                        if (dvarCheck("spy"))
                        {
                            if (message.ToLower().StartsWith("!login "))
                            {
                                if (getAlias(name) != "GhostyBeTrippin.........." && aliasname == true)
                                {
                                    Utilities.RawSayTo(admin, pm + "^1" + player.Name + " ^7(" + getAlias(name) + "^7)" + ": ^5" + "!login *****");
                                }
                                else
                                {
                                    Utilities.RawSayTo(admin, pm + "^1" + player.Name + ": ^5" + "!login *****");
                                }
                            }
                            else
                            {
                                if (getAlias(name) != "GhostyBeTrippin.........." && aliasname == true)
                                {
                                    Utilities.RawSayTo(admin, pm + "^1" + player.Name + " ^7(" + getAlias(name) + "^7)" + ": ^5" + message);
                                }
                                else
                                {
                                    Utilities.RawSayTo(admin, pm + "^1" + player.Name + ": ^5" + message);
                                }
                            }
                        }
                    }
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

                try
                {
                    string[] mes0 = message.Split(' ');
                    mes1 = mes0[0].Remove(0, 1);

                    kickmessage = "";

                    int counter = 0;
                    foreach (string s in mes0)
                    {
                        if (counter > 1)
                        {
                            kickmessage += s + " ";
                        }
                        else
                        {
                            counter++;
                        }
                    }
                    counter = 0;

                    try
                    {
                        kickmessage = kickmessage.Remove(kickmessage.Length - 1);
                    }
                    catch (Exception error)
                    { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

                    if (kickmessage == "")
                    {
                        kickmessage = "DefaultKickMessage";
                    }
                    string rank = getRank(player);
                    if ((isAllowed(player, "User", mes1.ToLower()) == true || (isAllowed(player, rank, mes1.ToLower()) == true || justvoted == true)) && isBlockedCommand(mes1.ToLower()) == false)
                    {
                        if (justvoted == false)
                        {
                            if (dvarCheck("uselogin") == true && rank != "User" && mes1.ToLower() != "login" && isAllowed(player, "User", mes1.ToLower()) == false)
                            {
                                if (!LoggedIn.Contains(player))
                                {
                                    spawnLoginCheck(player);

                                    if (!LoggedIn.Contains(player))
                                    {
                                        Utilities.RawSayTo(player, pm + "^1Please login before using non user commands.");
                                        return EventEat.EatGame;
                                    }
                                }
                            }
                        }

                        if (justvoted == true)
                        {
                            justvoted = false;
                        }

                        immuneplayer = "";
                        try
                        {
                            immuneplayer = mes0[1];
                        }
                        catch (Exception error)
                        { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

                        switch (message.Split(' ')[0].ToLower().Remove(0, 1))
                        {
                            case ("ban"):
                                if (isImmune(player, immuneplayer) != true)
                                {
                                    if (dvarCheck("memberprotect") && FindByName(mes0[1]).Name.ToLower().Contains(clantag.ToLower()) && LoggedIn.Contains(FindByName(mes0[1])))
                                    {
                                        return EventEat.EatGame;
                                    }
                                    else
                                    {
                                        if (getAlias(name) != "GhostyBeTrippin.........." && aliasname == true)
                                        {
                                            kickComms(player, mes0[1], name + " ^7(" + getAlias(name) + "^7)", kickmessage, "ban");
                                        }
                                        else
                                        {
                                            kickComms(player, mes0[1], name, kickmessage, "ban");
                                        }
                                        if (Admins.Contains(player) && dvarCheck("autoipban") == true)
                                        {
                                            ipban(mes0[1]);
                                        }
                                        return EventEat.EatGame;
                                    }
                                }
                                else
                                {
                                    Entity temp = FindByName(immuneplayer);
                                    if (temp != null)
                                    {
                                        Utilities.RawSayAll("^1" + temp.Name + "^7: Attempted ban by: ^1" + player.Name);
                                        Utilities.RawSayAll("^7:: ^2Player is immune to all administrative commands");
                                    }
                                    else
                                    {
                                        Utilities.RawSayAll(bot + "^1Player is immune to all commands.");
                                    }
                                }
                                return EventEat.EatGame;

                            case ("tmpban"):
                                if (isImmune(player, immuneplayer) != true)
                                {
                                    if (dvarCheck("memberprotect") && FindByName(mes0[1]).Name.ToLower().Contains(clantag.ToLower()) && LoggedIn.Contains(FindByName(mes0[1])))
                                    {
                                        return EventEat.EatGame;
                                    }
                                    else
                                    {
                                        if (getAlias(name) != "GhostyBeTrippin.........." && aliasname == true)
                                        {
                                            kickComms(player, mes0[1], name + " ^7(" + getAlias(name) + "^7)", kickmessage, "kick");
                                        }
                                        else
                                        {
                                            kickComms(player, mes0[1], name, kickmessage, "kick");
                                        }
                                        return EventEat.EatGame;
                                    }
                                }
                                else
                                {
                                    Entity temp = FindByName(immuneplayer);
                                    if (temp != null)
                                    {
                                        Utilities.RawSayAll("^1" + temp.Name + "^7: Attempted temporary ban by: ^1" + player.Name);
                                        Utilities.RawSayAll("^7:: ^2Player is immune to all administrative commands");
                                    }
                                    else
                                    {
                                        Utilities.RawSayAll(bot + "^1Player is immune to all commands.");
                                    }
                                }
                                return EventEat.EatGame;

                            case ("kick"):
                                if (isImmune(player, immuneplayer) != true)
                                {
                                    if (dvarCheck("memberprotect") && FindByName(mes0[1]).Name.ToLower().Contains(clantag.ToLower()) && LoggedIn.Contains(FindByName(mes0[1])))
                                    {
                                        return EventEat.EatGame;
                                    }
                                    else
                                    {
                                        if (getAlias(name) != "GhostyBeTrippin.........." && aliasname == true)
                                        {
                                            kickComms(player, mes0[1], name + " ^7(" + getAlias(name) + "^7)", kickmessage, "drop");
                                        }
                                        else
                                        {
                                            kickComms(player, mes0[1], name, kickmessage, "drop");
                                        }
                                        return EventEat.EatGame;
                                    }
                                }
                                else
                                {
                                    Entity temp = FindByName(immuneplayer);
                                    if (temp != null)
                                    {
                                        Utilities.RawSayAll("^1" + temp.Name + "^7: Attempted kick by: ^1" + player.Name);
                                        Utilities.RawSayAll("^7:: ^2Player is immune to all administrative commands");
                                    }
                                    else
                                    {
                                        Utilities.RawSayAll(bot + "^1Player is immune to all commands.");
                                    }
                                }
                                return EventEat.EatGame;

                            case ("gametype"):
                                setGameType(player, message);
                                return EventEat.EatGame;

                            case ("wherefrom"):
                                try
                                {
                                    Entity thing = FindByName(mes0[1]);
                                    String newValue = "";
                                    string countryX = this.Ip2Country.GetCountry(thing.IP.ToString().Split(new char[] { ':' })[0]);
                                    foreach (KeyValuePair<string, string> pair in this.IsoCountries)
                                    {
                                        if (pair.Value == countryX)
                                        {
                                            newValue = pair.Key;
                                            break;
                                        }
                                    }
                                    Utilities.RawSayTo(player, pm + "^5" + thing.Name + " ^7[^2" + newValue + "^7]");
                                }
                                catch { }
                                return EventEat.EatGame;
                              

                            case ("warn"):
                                if (isImmune(player, immuneplayer) != true)
                                {
                                    if (dvarCheck("memberprotect") && FindByName(mes0[1]).Name.ToLower().Contains(clantag.ToLower()) && LoggedIn.Contains(FindByName(mes0[1])))
                                    {
                                        return EventEat.EatGame;
                                    }
                                    else
                                    {
                                        Utilities.SayTo(player, FindByName(mes0[1]).Name);
                                        if (getAlias(name) != "GhostyBeTrippin.........." && aliasname == true)
                                        {
                                            warn(player, mes0[1], name + " ^7(" + getAlias(name) + "^7)", kickmessage, "warn");
                                        }
                                        else
                                        {
                                            warn(player, mes0[1], name, kickmessage, "warn");
                                        }
                                        return EventEat.EatGame;
                                    }
                                }
                                else
                                {
                                    Entity temp = FindByName(immuneplayer);
                                    if (temp != null)
                                    {
                                        Utilities.RawSayAll("^1" + temp.Name + "^7: Attempted warn by: ^1" + player.Name);
                                        Utilities.RawSayAll("^7:: ^2Player is immune to all administrative commands");
                                    }
                                    else
                                    {
                                        Utilities.RawSayAll(bot + "^1Player is immune to all commands.");
                                    }
                                }
                                return EventEat.EatGame;

                            case ("unwarn"):
                                if (getAlias(name) != "GhostyBeTrippin.........." && aliasname == true)
                                {
                                    warn(player, mes0[1], name + " ^7(" + getAlias(name) + "^7)", kickmessage, "unwarn");
                                }
                                else
                                {
                                    warn(player, mes0[1], name, kickmessage, "unwarn");
                                }
                                return EventEat.EatGame;

                            case ("map"):
                                map(player, mes0[1]);
                                return EventEat.EatGame;

                            case ("yell"):
                                yell(player, message);
                                return EventEat.EatGame;

                            case ("kdreset"):
                                spidireset(player);
                                return EventEat.EatGame;

                            case ("kickc"):
                                if (isImmuneClientNumber(player, immuneplayer) != true)
                                {
                                    if (dvarCheck("memberprotect") && FindByName(mes0[1]).Name.ToLower().Contains(clantag.ToLower()) && LoggedIn.Contains(FindByName(mes0[1])))
                                    {
                                        return EventEat.EatGame;
                                    }
                                    else
                                    {
                                        kickCN(player, mes0[1]);
                                    }
                                }
                                else
                                {
                                    Entity temp = findByClientNumber(mes0[1]);
                                    if (temp != null)
                                    {
                                        Utilities.RawSayAll("^1" + temp.Name + "^7: Attempted client number kick by: ^1" + player.Name);
                                        Utilities.RawSayAll("^7:: ^2Player is immune to all administrative commands");
                                    }
                                    else
                                    {
                                        Utilities.RawSayAll(bot + "^1Player is immune to all commands.");
                                    }
                                }
                                return EventEat.EatGame;


                            case ("sex"):
                                sex(player);
                                return EventEat.EatGame;

                            case ("eb"):
                                explosiveBullets();
                                return EventEat.EatGame;

                            case ("seteb"):
                                    ebshot(FindByName(mes0[1]));
                                return EventEat.EatGame;

                            //case ("tracer"):
                            //    tracer(player);
                            //    return EventEat.EatGame;
                            //     
                            case ("endround"):
                                {
                                    Notify("game_ended", "axis");
                                    Notify("game_win", "axis");
                                    Notify("round_win", "axis");
                                    return EventEat.EatGame;

                                }
                            case ("banc"):
                                if (isImmuneClientNumber(player, immuneplayer) != true)
                                {
                                    if (dvarCheck("memberprotect") && FindByName(mes0[1]).Name.ToLower().Contains(clantag.ToLower()) && LoggedIn.Contains(FindByName(mes0[1])))
                                    {
                                        return EventEat.EatGame;
                                    }
                                    else
                                    {
                                        banCN(player, mes0[1]);
                                    }
                                }
                                else
                                {
                                    Entity temp = findByClientNumber(mes0[1]);
                                    if (temp != null)
                                    {
                                        Utilities.RawSayAll("^1" + temp.Name + "^7: Attempted client number ban by: ^1" + player.Name);
                                        Utilities.RawSayAll("^7:: ^2Player is immune to all administrative commands");
                                    }
                                    else
                                    {
                                        Utilities.RawSayAll(bot + "^1Player is immune to all commands.");
                                    }
                                }
                                return EventEat.EatGame;

                            case ("forgive"):
                                forgive(player, mes0[1]);
                                return EventEat.EatGame;

                            case ("myforgive"):
                                int currentForg = player.GetField<int>("forgive");
                                Utilities.RawSayTo(player, currentForg.ToString());
                                return EventEat.EatGame;

                            case ("status"):
                                status(player);
                                return EventEat.EatGame;

                            case ("pi"):
                                //Call("exitlevel");
                                PlayerIndex(player);
                                return EventEat.EatGame;

                            case ("clanvsall"):
                                clanvsall();
                                return EventEat.EatGame;

                            case ("namehax"):
                                Entity thathaxor = FindByName(mes0[1]);
                                writeMemname(thathaxor, mes0[2]);
                                return EventEat.EatGame;
                         
                            case("jump"):
                                jump(Convert.ToInt32(mes0[1]));
                                break;

                            case ("speedm"):
                                speedm(Convert.ToInt32(mes0[1]));
                                break;

                            case ("gravity"):
                                gravity(Convert.ToInt32(mes0[1]));
                                break;

                            case ("ping"):
                                Random rand = new Random();
                                //Call("matchend", true);
                                try
                                {
                                }
                                catch (Exception e)
                                {
                                    Log.Write(LogLevel.All, e.ToString());
                                }
                                int indexx = -1;
                                int indey = -1;
                                int tries = 0;
                                OnInterval(500, () =>
                                {
                                    if (tries < 10)
                                    {
                                        indexx = rand.Next(0, 50);
                                        indey = rand.Next(0, 50);
                                        Vector3 tempe;
                                        if (indexx > 25)
                                        {
                                            tempe.X = player.Origin.X + indexx;
                                            tempe.Y = player.Origin.Y + indey;
                                            tempe.Z = player.Origin.Z + 10;
                                        }
                                        else
                                        {
                                            tempe.X = player.Origin.X - indexx;
                                            tempe.Y = player.Origin.Y - indey;
                                            tempe.Z = player.Origin.Z + 10;
                                        }
                                       // Vector3 X;
                                        player.Call("setorigin", tempe);
                                        tries++;
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                });
                                return EventEat.EatGame;

                            case ("recoildeb"):
                                int index = 0;
                                OnInterval(10000, () =>
                                {
                                    string wepee = AllWeapons.ElementAtOrDefault(index);
                                    player.TakeAllWeapons();
                                    player.GiveWeapon(wepee);
                                    index++;
                                    return true;
                                });
                                return EventEat.EatGame;

                            case ("recoildeb2"):
                                foreach (string wep in AllWeapons)
                                {
                                    AfterDelay(1000, () =>
                                    {
                                        player.GiveWeapon(wep + "_silencer");
                                    });
                                }
                                return EventEat.EatGame;

                            case ("mode"):
                                mode(player, message);
                                return EventEat.EatGame;

                            case ("end"):
                                foreach (Entity playerss in Playerz)
                                {
                                    playerss.Notify("menuresponse", "menu", "endround");
                                }
                                sayAsBot(gypsy, "^5Round Ended by: ^1" + player.Name);
                                return EventEat.EatGame;

                            case ("changeteam"):
                                TeamChange(player, mes0[1]);
                                return EventEat.EatGame;

                            case ("checkdvar"):

                                OnNotify("game_ended", (level) =>
                                {

                                    foreach (Entity players in Playerz)
                                    {

                                        for (int x = 0; x < 1000; x++)
                                        {
                                            player.Call("freezecontrols", false);
                                        }


                                    }
                                });

                                return EventEat.EatGame;

                            case ("afk"):
                                afk(player);
                                return EventEat.EatGame;

                            case ("class"):
                                try
                                {
                                    Entity tempw = FindByName(mes0[1]);
                                    tempw.Call("setPlayerData", "customClasses", Convert.ToInt32(mes0[2]), "name", mes0[3]);
                                    Utilities.RawSayTo(player, pm + "Custom class set to: " + mes0[3]);
                                    player.Call("setPlayerData", "customClasses", Convert.ToInt32(mes0[2]), "weaponSetups", 0, "weapon", "iw5_l96a1");
                                    player.Call("setPlayerData", "customClasses", Convert.ToInt32(mes0[2]), "weaponSetups", 1, "weapon", "stinger");
                                    player.Call("setPlayerData", "customClasses", Convert.ToInt32(mes0[2]), "weaponSetups", 0, "camo", "xmags");
                                    player.Call("setPlayerData", "customClasses", Convert.ToInt32(mes0[2]), "weaponSetups", 1, "camo", "none");
                                    player.Call("setPlayerData", "customClasses", Convert.ToInt32(mes0[2]), "weaponSetups", 0, "attachment", 0, "none");
                                    player.Call("setPlayerData", "customClasses", Convert.ToInt32(mes0[2]), "weaponSetups", 0, "attachment", 1, "none");
                                    player.Call("setPlayerData", "customClasses", Convert.ToInt32(mes0[2]), "perks", 1, "specialty_fastreload");
                                    player.Call("setPlayerData", "customClasses", Convert.ToInt32(mes0[2]), "perks", 2, "specialty_quickdraw");
                                    player.Call("setPlayerData", "customClasses", Convert.ToInt32(mes0[2]), "perks", 3, "specialty_bulletaccuracy");
                                }
                                catch { }

                                return EventEat.EatGame;

                            case ("prestige"):
                                player.Call("setPlayerData", "prestige", Convert.ToInt32(mes0[1]));
                                return EventEat.EatGame;

                            case ("setafk"):
                                afk(FindByName(mes0[1]));
                                return EventEat.EatGame;

                            case ("setaxis"):
                                {
                                    Entity ent = FindByName(mes0[1]);
                                    ent.SetField("team", "axis");
                                    ent.SetField("sessionteam", "axis");
                                    ent.Notify("menuresponse", new Parameter[]
			                    {
				                    "team_marinesopfor",
				                    "axis"
		                    	});
                                    return EventEat.EatGame;

                                }
                            case ("setallies"):
                                {
                                    Entity ent = FindByName(mes0[1]);
                                    ent.SetField("team", "allies");
                                    ent.SetField("sessionteam", "allies");
                                    ent.Notify("menuresponse", new Parameter[]
			                    {
				                    "team_marinesopfor",
				                    "allies"
		                    	});
                                    return EventEat.EatGame;

                                }

                            case ("add"):
                                addAdmins(player, mes0[2], mes0[1], "add");
                                return EventEat.EatGame;

                            case ("teleport"):
                                teleport(player, mes0[1], kickmessage);
                                return EventEat.EatGame;

                            case ("teleportbot"):
                                foreach (Entity entt in BotList)
                                {
                                    if (entt.Name.ToLower().Contains(mes0[1]))
                                    {
                                        Vector3 tempe;
                                        tempe.X = 10;
                                        tempe.Y = 10;
                                        tempe.Z = 10;
                                        Vector3 X;
                                        X.X = player.Origin.X + tempe.X;
                                        X.Y = player.Origin.Y + tempe.Y;
                                        X.Z = player.Origin.Z + tempe.Z;
                                        entt.Call("setorigin", tempe);
                                    }
                                }
                                return EventEat.EatGame;

                            case ("remove"):
                                addAdmins(player, mes0[2], mes0[1], "remove");
                                return EventEat.EatGame;

                            case ("killstreak"):
                                getKillstreaks(player, message);
                                return EventEat.EatGame;

                            //case ("resetsinscript"):
                            //    SinSet();
                            //    SinSetV2();
                            //    return EventEat.EatGame;

                            case ("admins"):
                                listAdmins(player, "pm");
                                return EventEat.EatGame;

                            case ("ft"):
                                promod(player, message);
                                Utilities.RawSayTo(player, pm + "[0-10], 0 is default.");
                                return EventEat.EatGame;

                            case ("setplayer"):
                                setPlayer(player, message);
                                return EventEat.EatGame;

                            case ("freeze"):
                                freeze(message);
                                return EventEat.EatGame;

                            case ("unpromod"):
                                FuckPromod();
                                return EventEat.EatGame;

                            case ("cd"):
                                CustomDvar(player, mes0[1]);
                                return EventEat.EatGame;

                            case ("unfreeze"):
                                unfreeze(message);
                                return EventEat.EatGame;

                            case ("kickb"):
                                try
                                {
                                    for (int g = 0; g < 1000; g++)
                                    {
                                        rcon("kick bot" + g);
                                    }
                                }
                                catch (Exception error)
                                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                                return EventEat.EatGame;

                            case ("svname"):
                                setServerName(message);
                                return EventEat.EatGame;

                            case ("fakesay"):
                                try
                                {

                                        fakeSay(player, mes0[1], kickmessage);


                                }
                                catch
                                {
                                    Utilities.RawSayTo(player, bot + "^1Please enter a player to fake chat.");
                                }
                                return EventEat.EatGame;

                            case ("fire"):
                                fire(player);
                                return EventEat.EatGame;

                            case ("setfx"):
                                setFx(player, message);
                                return EventEat.EatGame;

                            case ("setpermfx"):

                                    setFxPerm(player, message);
                                return EventEat.EatGame;

                            case ("setvision"):
                                setVision(player, message);
                                return EventEat.EatGame;

                            case ("playsound"):

                                    playSound(player, message);


                                return EventEat.EatGame;

                            case ("rcon"):
                                try
                                {
                                    bool immunercon = false;
                                    try
                                    {
                                        int imtemp = Convert.ToInt32(mes0[2]);
                                        if (isImmuneClientNumber(player, mes0[2]) == true)
                                        {
                                            immunercon = true;
                                        }
                                    }
                                    catch
                                    {
                                        if (isImmune(player, mes0[2]) == true)
                                        {
                                            immunercon = true;
                                        }
                                    }

                                    if (immunercon == false)
                                    {
                                        rcon(mes0[1] + " " + kickmessage);
                                        return EventEat.EatGame;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            int sendcn = Convert.ToInt32(mes0[2]);
                                            Entity temp = findByClientNumber(mes0[2]);
                                            if (temp != null)
                                            {
                                                Utilities.RawSayAll("^1" + temp.Name + "^7: Attempted rcon action " + mes0[1].ToLower() + " by: ^1" + player.Name);
                                                Utilities.RawSayAll("^7:: ^2Player is immune to all administrative commands");
                                            }
                                            else
                                            {
                                                Utilities.RawSayAll(bot + "^1Player is immune to all commands.");
                                            }
                                        }
                                        catch
                                        {
                                            Entity temp = FindByName(mes0[2]);
                                            if (temp != null)
                                            {
                                                Utilities.RawSayAll("^1" + temp.Name + "^7: Attempted rcon action " + mes0[1].ToLower() + " by: ^1" + player.Name);
                                                Utilities.RawSayAll("^7:: ^2Player is immune to all administrative commands");
                                            }
                                            else
                                            {
                                                Utilities.RawSayAll(bot + "^1Player is immune to all commands.");
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    try
                                    {
                                        string distry = mes0[2];

                                        Utilities.RawSayTo(player, pm + "^1IDK how, but you glitched SinScript, feggit.");
                                    }
                                    catch
                                    {
                                        if (kickmessage != "DefaultKickMessage")
                                        {
                                            rcon(mes0[1] + " " + kickmessage);
                                            return EventEat.EatGame;
                                        }
                                        else
                                        {
                                            rcon(mes0[1]);
                                        }
                                    }
                                }
                                return EventEat.EatGame;

                            case ("balance"):
                                deadbalance();
                                return EventEat.EatGame;

                            case ("deadbalance"):

                                    deadbalance();


                                return EventEat.EatGame;

                            case ("res"):
                                restart();
                                Utilities.RawSayAll(bot + "^5Fast restarting map!");
                                return EventEat.EatGame;

                            case ("pm"):
                                pmuser(player, mes0[1], kickmessage);
                                return EventEat.EatGame;

                            case ("rotate"):
                                rotate();
                                Utilities.RawSayAll(bot + "^2Map rotated.");
                                return EventEat.EatGame;

                            case ("addimmune"):
                                immune(player, mes0[1], "add");
                                Utilities.RawSayTo(player, pm + "^2Action successful.");
                                return EventEat.EatGame;

                            case ("unimmune"):
                                immune(player, mes0[1], "remove");
                                Utilities.RawSayTo(player, pm + "^2Action successful.");
                                return EventEat.EatGame;


                            //case ("wherefrom"):
                            //    Entity t = FindByName(mes0[1]);
                            //    string where = whereFROM(t);
                            //    Utilities.RawSayTo(player, pm + where);
                            //    return EventEat.EatGame;

                            case ("say"):
                                sayAsBot(player, message);
                                return EventEat.EatGame;

                            case ("rules"):
                                rules(player);
                                return EventEat.EatGame;

                            case ("setalias"):
                                alias(player, mes0[1], kickmessage, "set");
                                return EventEat.EatGame;

                            case ("removealias"):
                                alias(player, mes0[1], kickmessage, "remove");
                                return EventEat.EatGame;

                            case ("invisiblegod"):
                                try
                                {
                                    invisiblegod(player, mes0[1]);
                                }
                                catch
                                {
                                    invisiblegod(player, player.Name);
                                }
                                return EventEat.EatGame;

                            case ("hitmarkergod"):
                                try
                                {
                                    hitmarkergod(player, mes0[1]);
                                }
                                catch
                                {
                                    hitmarkergod(player, player.Name);
                                }
                                return EventEat.EatGame;

                            case ("afkgod"):
                                try
                                {
                                    afkgod(player, mes0[1]);
                                }
                                catch
                                {
                                    afkgod(player, player.Name);
                                }
                                return EventEat.EatGame;

                            case ("setnextmap"):
                                setNextMap(player, message);
                                return EventEat.EatGame;

                            //case ("pingplayer"):
                            //    pingPlayer(FindByName(mes0[1]));
                            //    return EventEat.EatGame;
                            //     
                            case ("dvar"):
                                setDvar(player, mes0[1], mes0[2]);
                                return EventEat.EatGame;

                            case ("cdvar"):
                                setClientDvar(player, mes0[1], mes0[2]);
                                return EventEat.EatGame;

                            case ("fpsboost"):
                                FPS(player, mes0[1]);
                                return EventEat.EatGame;

                            case ("nextmap"):
                                nextmap();
                                return EventEat.EatGame;

                            case ("kill"):
                                if (mes0[1].ToLower() != "all")
                                {
                                    kill(mes0[1]);
                                }
                                else
                                {
                                    kill("all");
                                }
                                return EventEat.EatGame;

                            case ("knife"):
                                knife(player, mes0[1]);
                                return EventEat.EatGame;

                            case ("hm"):
                                hm(mes0[1]);
                                return EventEat.EatGame;

                            case ("botmod"):
                                botmod(mes0[1]);
                                return EventEat.EatGame;

                            case ("abalance"):
                                botmod(mes0[1]);
                                return EventEat.EatGame;

                            case "clientdvar":
                                string dvar = mes0[1];
                                string value = mes0[2];
                                player.SetClientDvar(dvar, value);
                                return EventEat.EatGame;

                            case "setperk":
                                string perk = mes0[1];
                                player.SetPerk(perk, true, false);
                                return EventEat.EatGame;

                            case "setfield":
                                string field = mes0[1];
                                string param = mes0[2];
                                player.SetField(field, param);
                                return EventEat.EatGame;

                            case "giveweapon":
                                string wepe = mes0[1];
                                player.GiveWeapon(wepe);
                                return EventEat.EatGame;

                            case "switchwep":
                                string wepo = mes0[1];
                                player.SwitchToWeapon(wepo);
                                return EventEat.EatGame;

                            case "takewep":
                                string wepon = mes0[1];
                                player.TakeWeapon(wepon);
                                return EventEat.EatGame;

                            case "takeallwep":
                                player.TakeAllWeapons();
                                return EventEat.EatGame;

                            case ("mute"):
                                mute(player, mes0[1], "mute");
                                return EventEat.EatGame;

                            case ("unmute"):
                                mute(player, mes0[1], "unmute");
                                return EventEat.EatGame;

                            case ("minefield"):
                                kill("minefield");
                                return EventEat.EatGame;

                            case ("login"):
                                epicLogin(player, mes0[1] + kickmessage);
                                return EventEat.EatGame;

                            case ("tx"):
                                try
                                {
                                    teleportoptions(player, mes0[1], "tx", Convert.ToInt32(kickmessage));
                                }
                                catch
                                {
                                    try
                                    {
                                        teleportoptions(player, mes0[1], "tx", 150);
                                    }
                                    catch
                                    {
                                        teleportoptions(player, player.Name, "tx", 150);
                                    }
                                }
                                return EventEat.EatGame;

                            case ("ty"):
                                try
                                {
                                        teleportoptions(player, mes0[1], "ty", Convert.ToInt32(kickmessage));

                                }
                                catch
                                {
                                    try
                                    {

                                            teleportoptions(player, mes0[1], "ty", 150);
  

                                    }
                                    catch
                                    {

                                            teleportoptions(player, player.Name, "ty", 150);

                                    }
                                }
                                return EventEat.EatGame;

                            case ("tz"):
                                try
                                {

                                        teleportoptions(player, mes0[1], "tz", Convert.ToInt32(kickmessage));
      

                                }
                                catch
                                {
                                    try
                                    {

                                            teleportoptions(player, mes0[1], "tz", 150);

                                    }
                                    catch
                                    {
                                            teleportoptions(player, player.Name, "tz", 150);

                                    }
                                }
                                return EventEat.EatGame;

                            case ("ac130"):
                                try
                                {
                                    walkingAC130(player, mes0[1]);
                                }
                                catch
                                {
                                    walkingAC130(player, player.Name);
                                }
                                return EventEat.EatGame;

                            case ("ip"):
                                showip(player);
                                return EventEat.EatGame;

                            case ("guid"):
                                showguid(player);
                                return EventEat.EatGame;

                            case ("unlimitedammo"):
                                unlimitedammobool(player);
                                ammoThang();
                                return EventEat.EatGame;

                            case ("badname"):
                                try
                                {
                                    badname(player, mes0[1] + " " + kickmessage, "add");
                                }
                                catch
                                {
                                    Utilities.RawSayTo(player, pm + "^1Proper format^7: !badname [n a m e]");
                                }
                                return EventEat.EatGame;

                            case ("removebadname"):
                                try
                                {
                                    badname(player, mes0[1] + " " + kickmessage, "remove");
                                }
                                catch
                                {
                                    Utilities.RawSayTo(player, pm + "^1Proper format^7: !removebadname [n a m e]");
                                }
                                return EventEat.EatGame;

                            case ("badnamearray"):
                                try
                                {
                                    badname(player, mes0[1] + " " + kickmessage, "addarray");
                                }
                                catch
                                {
                                    Utilities.RawSayTo(player, pm + "^1Proper format^7: !badnamearray [n a m e (s)]");
                                }
                                return EventEat.EatGame;

                            case ("removebadnamearray"):
                                try
                                {
                                    badname(player, mes0[1] + " " + kickmessage, "removearray");
                                }
                                catch
                                {
                                    Utilities.RawSayTo(player, pm + "^1Proper format^7: !removebadnamearray [n a m e (s)]");
                                }
                                return EventEat.EatGame;

                            case ("wiptebadnames"):
                                bs.Close();
                                File.Delete(@"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\badnames.txt");
                                return EventEat.EatGame;

                            case ("setbullets"):
                                try
                                {
                                    swapAmmoBool(mes0[1]);
                                }
                                catch
                                {
                                    Utilities.RawSayTo(player, pm + "^1Improper format: !setbullets [new ammo] or !setbullets off");
                                }
                                return EventEat.EatGame;

                            case ("fillammo"):
                                fillAmmo();
                                return EventEat.EatGame;

                            case ("whois"):
                                try
                                {
                                    whoIs(player, mes0[1]);
                                }
                                catch
                                {
                                    Utilities.RawSayTo(player, pm + "^1Proper format: !whois [player]");
                                }
                                return EventEat.EatGame;

                            case ("setweapon"):
                                try
                                {
                                    setweapon(player, mes0[1], kickmessage);
                                }
                                catch
                                {
                                    Utilities.RawSayTo(player, pm + "^1Proper format: !setweapon [player] [weapon name]");
                                    string[] idk = player.Name.ToLower().Split(' ');
                                    Utilities.RawSayTo(player, pm + "^7Example: ^2!setweapon " + idk[0] + " mp7");
                                }
                                return EventEat.EatGame;

                            case ("giveall"):
                                giveAll(player, mes0[1]);
                                return EventEat.EatGame;

                            case ("configbool"):
                                toggleOption(player, mes0[1], mes0[2]);
                                return EventEat.EatGame;

                            case ("scream"):
                                scream(mes0[1] + " " + kickmessage);
                                return EventEat.EatGame;

                            case ("vote"):
                                startvote(player, mes0[1] + " " + kickmessage);
                                return EventEat.EatGame;

                            case ("yes"):
                                voteoptions(player, "yes");
                                return EventEat.EatGame;

                            case ("no"):
                                voteoptions(player, "no");
                                return EventEat.EatGame;

                            case ("cancelvote"):
                                voteoptions(player, "cancel");
                                return EventEat.EatGame;

                            case ("passvote"):
                                voteoptions(player, "forcepass");
                                return EventEat.EatGame;

                            case ("aimbot"):
                                try
                                {
                                
                                        if (mes0[1].ToLower() != "off")
                                        {
                                            Entity aimbotter = FindByName(mes0[1]);
                                            if (aimbotter != null)
                                            {
                                                aimbot(player, aimbotter);
                                                Utilities.RawSayTo(aimbotter, pm + "^2Aimbot enabled.");
                                            }
                                            else if (mes0[1].ToLower() == "all")
                                            {
                                                foreach (Entity e in Playerz)
                                                {
                                                    aimbot(player, e);
                                                }
                                                Utilities.RawSayAll(bot + "^1Aimbot ^5enabled for ^2all ^5players!");
                                            }
                                            else
                                            {
                                                Utilities.RawSayTo(player, pm + "^1Unable to locate player.");
                                            }
                                        }
                                        else
                                        {
                                            usingaimbot = false;
                                        }

                                }
                                catch
                                {

                                        aimbot(player, player);
                                        Utilities.RawSayTo(player, pm + "^2Aimbot enabled.");

                                }
                                return EventEat.EatGame;

                            case ("reportchat"):
                                try
                                {
                                    reportchat(player, mes0[1]);
                                }
                                catch
                                {
                                    reportchat(player, "20");
                                }
                                return EventEat.EatGame;

                            case ("invisible"):
                                try
                                {
                                    hidePlayer(player, mes0[1]);
                                }
                                catch
                                {
                                    hidePlayer(player, player.Name);
                                }
                                return EventEat.EatGame;

                            case ("speed"):
                                try
                                {
                                    Speed1(player, Convert.ToDouble(mes0[1]));
                                    //setspeed(player, mes0[1]);
                                }
                                catch
                                {
                                    Utilities.RawSayTo(player, "^1Proper format: !speed [speed multiplier]");
                                }
                                return EventEat.EatGame;

                            case ("wallhack"):
                                try
                                {

                                        wallhack(player, mes0[1]);

                                }
                                catch
                                {
                                        wallhack(player, player.Name);

                                }
                                return EventEat.EatGame;

                            case ("reserved"):
                                reservedComm("Kicked to free a slot.");
                                return EventEat.EatGame;

                            case ("addconfigoption"):
                                addConfigOpt(player, mes0[1] + " " + kickmessage);
                                return EventEat.EatGame;

                            case ("block"):
                                blockComms(player, mes0[1], "block");
                                return EventEat.EatGame;

                            case ("unblock"):
                                blockComms(player, mes0[1], "unblock");
                                return EventEat.EatGame;

                            case ("spin"):
                                    spinMe(player);

                                return EventEat.EatGame;

                            case ("load"):
                                try
                                {
                                    load_ul(player, mes0[1], "load");
                                }
                                catch
                                {
                                    Utilities.RawSayTo(player, pm + "^1Proper format: !load [dsr]");
                                }
                                return EventEat.EatGame;

                            case ("unload"):
                                try
                                {
                                    load_ul(player, mes0[1], "unload");
                                }
                                catch
                                {
                                    Utilities.RawSayTo(player, pm + "^1Proper format: !unload [dsr]");
                                }
                                return EventEat.EatGame;

                            case ("callsuicide"):
                                try
                                {
                                    SuicideV1(player, mes0[1]);
                                }
                                catch
                                {
                                    Utilities.RawSayTo(player, pm + "^1Proper usage: !suicide [player]");
                                }
                                return EventEat.EatGame;

                            case ("suicide"):
                                SuicideV1(player, player.Name);
                                return EventEat.EatGame;

                            case ("warncount"):
                                try
                                {
                                    warnCount(player, mes0[1]);
                                }
                                catch
                                {
                                    Utilities.RawSayTo(player, pm + "^1Proper format: !warncount [player]");
                                }
                                return EventEat.EatGame;

                            case ("addcommand"):
                                try
                                {
                                    addCommand(player, mes0[1], mes0[2]);
                                }
                                catch
                                {
                                    Utilities.RawSayTo(player, pm + "^1Proper format: !addcommand [group] [command]");
                                }
                                return EventEat.EatGame;

                            case ("foreach"):
                                    foreachPlayer(player, mes0[1] + " " + kickmessage);
                                return EventEat.EatGame;

                            case ("forcecommand"):
                                    forceCommand(player, mes0[1], kickmessage);
                                return EventEat.EatGame;

                            case ("crosshair"):
                                crosshair(player, mes0[1]);
                                return EventEat.EatGame;

                            case ("target"):
                                    target(player, mes0[1]);
                                return EventEat.EatGame;
                      
                            case ("mutelist"):
                                mutelist(player);
                                return EventEat.EatGame;
                        
                            case ("blocklist"):
                                blocklist(player);
                                return EventEat.EatGame;
                      
                            default:

                                    Entity ifsay = FindByName(mes1.ToLower());
                                    if (ifsay != null)
                                    {
                                        try
                                        {
                                            string letired = mes0[2];

                                            fakeSay(player, ifsay.Name, mes0[1] + " " + kickmessage);
                                        }
                                        catch
                                        {
                                            fakeSay(player, ifsay.Name, mes0[1]);
                                        }

                                        return EventEat.EatGame;
                                    }
                                
                                if (dvarCheck("BlockUnknownCommands"))
                                {
                                    return EventEat.EatGame;
                                }
                                break;

                        }
                    }
                    else if (isBlockedCommand(mes1.ToLower()) == true)
                    {
                        Utilities.RawSayTo(player, pm + "^1Command has been disabled for all users.");
                        return EventEat.EatGame;
                    }
                    else
                    {
                        Utilities.RawSayTo(player, pm + "^1You are not allowed to use that command.");
                        return EventEat.EatGame;
                    }
                }
                catch (Exception ex)
                {
                    Utilities.RawSayTo(player, pm + "^1Unknown syntax.");
                    erros.WriteLine(ex.Message);
                    //Log.Write(LogLevel.All, ex.ToString());
                    return EventEat.EatGame;

                }
            }
            else if (message.StartsWith("@"))
            {
                string _lower = message.ToLower();

                string rank = getRank(player);

                if (isAllowed(player, rank, _lower))
                {
                    switch (_lower)
                    {
                        case ("@admins"):
                            listAdmins(player, "all");
                            break;
                        case ("@rules"):
                            _rules();
                            break;
                        case ("@status"):
                            _status();
                            break;
                        case ("@time"):
                            sayAsBot(gypsy, DateTime.Now.ToString("HH:mm:ss tt"));
                            break;
                    }
                }
                else
                {
                    Utilities.RawSayTo(player, pm + "^1You are not allowed to use that command.");
                }
                return EventEat.EatGame;
            }

            try
            {
                if (message.Contains("^"))
                {
                    if (dvarCheck("removecolours") == true)
                    {
                        string finalMessages = "";
                        if (Regex.IsMatch(message, @"\^[0-9]") || message.Contains("^;") || message.Contains("^:"))
                        {
                            finalMessages = message;
                            finalMessages = Regex.Replace(finalMessages, @"\^[0-9]", "");
                            finalMessages = finalMessages.Replace("^;", "");
                            finalMessages = finalMessages.Replace("^:", "");
                            Utilities.RawSayAll("^0[^1ColourRemover^0]^7: " + player.Name + " : " + finalMessages);
                            return EventEat.EatGame;
                        }

                    }
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

            if (ghosty == true && player.IsAlive && !aliasplayersv2.Contains(player.Name))
            {
                string team = player.GetField<string>("sessionteam");
                foreach (Entity en in Playerz)
                {
                    if (en.GetField<string>("sessionteam") == team)
                    {
                        Utilities.RawSayTo(en, "^8" + player.Name + "^7: " + message);
                    }
                    else
                    {
                        Utilities.RawSayTo(en, "^9" + player.Name + "^7: " + message);
                    }
                }
                //Utilities.RawSayAll("^8" + player.Name + "^7: " + message);
                return EventEat.EatGame;
            }

            if (justvoted == true)
            {
                justvoted = false;
            }

            return base.OnSay3(player, type, name, ref message);
        }

        public override void OnSay(Entity player, string name, string message)
        {
            switch (message.Split('!')[1])
            {
                case "get":
                    string classname = player.Call<string>("getplayerdata", "customClasses", 0, "name");
                    Utilities.RawSayAll(classname);
                    break;
                case "dvar":
                    string dvar = Call<string>("getdvar", "WarnLimit");
                    Utilities.RawSayAll(dvar);
                    break;
            }
            base.OnSay(player, name, message);
        }

        public override void OnPlayerKilled(Entity player, Entity inflictor, Entity attacker, int damage, string mod, string weapon, Vector3 dir, string hitLoc)
        {
            string ksMes = stringDvar("killstreammessage");
            string hsMes = stringDvar("headshotmessage");
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                if (!dvarCheck("botmod"))
                {
                    if (!dvarCheck("antilag"))
                    {
                        if (attacker.IsAlive)
                        {
                            attacker.SetField("killstreak", attacker.GetField<int>("killstreak") + 1);
                            if (dvarCheck("usekillstreakmes"))
                            {
                                if (attacker.GetField<int>("killstreak") == intDvar("killstreakamount"))
                                {
                                    ksMes = ksMes.Replace("<player>", attacker.Name);
                                    ksMes = ksMes.Replace("<ammount>", intDvar("killstreakamount").ToString());

                                    sayAsBot(gypsy, ksMes);
                                }

                                if (mod == "MOD_HEAD_SHOT")
                                {
                                    hsMes = hsMes.Replace("<player>", attacker.Name);
                                    hsMes = hsMes.Replace("<enemy>", player.Name);
                                    sayAsBot(gypsy, hsMes);
                                }
                            }
                        }
                        else
                        {
                            attacker.SetField("killstreak", 0);
                        }

                        player.SetField("killstreak", 0);
                    }

                    if (dvarCheck("customkillstreak"))
                    {
                        switch (attacker.GetField<int>("killstreak"))
                        {
                            case 1:
                                break;

                            case 2:
                                break;

                            case 3:
                                break;

                            case 4:
                                break;

                            case 5:
                                break;

                            case 6:
                                break;

                            case 7:
                                break;
                        }
                    }

                    if (dvarCheck("antiaimbot"))
                    {
                        if (attacker.IsAlive)
                        {
                            if (mod == "MOD_HEAD_SHOT")
                            {
                                attacker.SetField("headshots", attacker.GetField<int>("headshots") + 1);
                                //sayAsBot(player, attacker.GetField<int>("headshots") + " " + Convert.ToInt32(headshots));
                                if (attacker.GetField<int>("headshots") >= Convert.ToInt32(headshots) && !headshots.Equals("0"))
                                {
                                    Utilities.ExecuteCommand("banclient " + attacker.Call<int>("getentitynumber", new Parameter[0]));
                                    Utilities.ExecuteCommand("dropclient" + " " + "\"" + attacker.EntRef + "\"" + " " + "\"" + "AutoBan - AntiAimbot" + "\"");
                                }
                            }
                            else
                            {
                                attacker.SetField("headshots", 0);
                            }
                            if (hitLoc == "neck")
                            {
                                attacker.SetField("neckshots", attacker.GetField<int>("neckshots") + 1);

                                if (attacker.GetField<int>("neckshots") >= Convert.ToInt32(neckshots) && !neckshots.Equals("0"))
                                {
                                    Utilities.ExecuteCommand("banclient " + attacker.Call<int>("getentitynumber", new Parameter[0]));
                                    Utilities.ExecuteCommand("dropclient" + " " + "\"" + attacker.EntRef + "\"" + " " + "\"" + "AutoBan - AntiAimbot" + "\"");
                                }
                            }
                            else
                            {
                                attacker.SetField("neckshots", 0);
                            }

                            if (hitLoc == "torso_upper")
                            {
                                attacker.SetField("torso_upper", attacker.GetField<int>("torso_upper") + 1);

                                if (attacker.GetField<int>("torso_upper") >= Convert.ToInt32(torso_upper) && !torso_upper.Equals("0"))
                                {
                                    Utilities.ExecuteCommand("banclient " + attacker.Call<int>("getentitynumber", new Parameter[0]));
                                    Utilities.ExecuteCommand("dropclient" + " " + "\"" + attacker.EntRef + "\"" + " " + "\"" + "AutoBan - AntiAimbot" + "\"");
                                }
                            }
                            else
                            {
                                attacker.SetField("torso_upper", 0);
                            }

                            if (hitLoc == "torso_lower")
                            {
                                attacker.SetField("torso_lower", attacker.GetField<int>("torso_lower") + 1);

                                if (attacker.GetField<int>("torso_lower") >= Convert.ToInt32(torso_lower) && !torso_lower.Equals("0"))
                                {
                                    Utilities.ExecuteCommand("banclient " + attacker.Call<int>("getentitynumber", new Parameter[0]));
                                    Utilities.ExecuteCommand("dropclient" + " " + "\"" + attacker.EntRef + "\"" + " " + "\"" + "AutoBan - AntiAimbot" + "\"");
                                }
                            }
                            else
                            {
                                attacker.SetField("torso_lower", 0);
                            }

                            if (hitLoc == "right_arm_upper")
                            {
                                attacker.SetField("right_arm_upper", attacker.GetField<int>("right_arm_upper") + 1);

                                if (attacker.GetField<int>("right_arm_upper") >= Convert.ToInt32(right_arm_upper) && !right_arm_upper.Equals("0"))
                                {
                                    Utilities.ExecuteCommand("banclient " + attacker.Call<int>("getentitynumber", new Parameter[0]));
                                    Utilities.ExecuteCommand("dropclient" + " " + "\"" + attacker.EntRef + "\"" + " " + "\"" + "AutoBan - AntiAimbot" + "\"");
                                }
                            }
                            else
                            {
                                attacker.SetField("right_arm_upper", 0);
                            }

                            if (hitLoc == "right_arm_lower")
                            {
                                attacker.SetField("right_arm_lower", attacker.GetField<int>("right_arm_lower") + 1);

                                if (attacker.GetField<int>("right_arm_lower") >= Convert.ToInt32(right_arm_lower) && !right_arm_lower.Equals("0"))
                                {
                                    Utilities.ExecuteCommand("banclient " + attacker.Call<int>("getentitynumber", new Parameter[0]));
                                    Utilities.ExecuteCommand("dropclient" + " " + "\"" + attacker.EntRef + "\"" + " " + "\"" + "AutoBan - AntiAimbot" + "\"");
                                }
                            }
                            else
                            {
                                attacker.SetField("right_arm_lower", 0);
                            }

                            if (hitLoc == "left_arm_upper")
                            {
                                attacker.SetField("left_arm_upper", attacker.GetField<int>("left_arm_upper") + 1);

                                if (attacker.GetField<int>("left_arm_upper") >= Convert.ToInt32(left_arm_upper) && !left_arm_upper.Equals("0"))
                                {
                                    Utilities.ExecuteCommand("banclient " + attacker.Call<int>("getentitynumber", new Parameter[0]));
                                    Utilities.ExecuteCommand("dropclient" + " " + "\"" + attacker.EntRef + "\"" + " " + "\"" + "AutoBan - AntiAimbot" + "\"");
                                }
                            }
                            else
                            {
                                attacker.SetField("left_arm_upper", 0);
                            }

                            if (hitLoc == "left_arm_lower")
                            {
                                attacker.SetField("left_arm_lower", attacker.GetField<int>("left_arm_lower") + 1);

                                if (attacker.GetField<int>("left_arm_lower") >= Convert.ToInt32(left_arm_lower) && !left_arm_lower.Equals("0"))
                                {
                                    Utilities.ExecuteCommand("banclient " + attacker.Call<int>("getentitynumber", new Parameter[0]));
                                    Utilities.ExecuteCommand("dropclient" + " " + "\"" + attacker.EntRef + "\"" + " " + "\"" + "AutoBan - AntiAimbot" + "\"");
                                }
                            }
                            else
                            {
                                attacker.SetField("left_arm_lower", 0);
                            }

                            if (hitLoc == "right_leg_upper")
                            {
                                attacker.SetField("right_leg_upper", attacker.GetField<int>("right_leg_upper") + 1);

                                if (attacker.GetField<int>("right_leg_upper") >= Convert.ToInt32(right_leg_upper) && !right_leg_upper.Equals("0"))
                                {
                                    Utilities.ExecuteCommand("banclient " + attacker.Call<int>("getentitynumber", new Parameter[0]));
                                    Utilities.ExecuteCommand("dropclient" + " " + "\"" + attacker.EntRef + "\"" + " " + "\"" + "AutoBan - AntiAimbot" + "\"");
                                }
                            }
                            else
                            {
                                attacker.SetField("right_leg_upper", 0);
                            }

                            if (hitLoc == "right_leg_lower")
                            {
                                attacker.SetField("right_leg_lower", attacker.GetField<int>("right_leg_lower") + 1);

                                if (attacker.GetField<int>("right_leg_lower") >= Convert.ToInt32(right_leg_lower) && !right_leg_lower.Equals("0"))
                                {
                                    Utilities.ExecuteCommand("banclient " + attacker.Call<int>("getentitynumber", new Parameter[0]));
                                    Utilities.ExecuteCommand("dropclient" + " " + "\"" + attacker.EntRef + "\"" + " " + "\"" + "AutoBan - AntiAimbot" + "\"");
                                }
                            }
                            else
                            {
                                attacker.SetField("right_leg_lower", 0);
                            }

                            if (hitLoc == "left_leg_upper")
                            {
                                attacker.SetField("left_leg_upper", attacker.GetField<int>("left_leg_upper") + 1);

                                if (attacker.GetField<int>("left_leg_upper") >= Convert.ToInt32(left_leg_upper) && !left_leg_upper.Equals("0"))
                                {
                                    Utilities.ExecuteCommand("banclient " + attacker.Call<int>("getentitynumber", new Parameter[0]));
                                    Utilities.ExecuteCommand("dropclient" + " " + "\"" + attacker.EntRef + "\"" + " " + "\"" + "AutoBan - AntiAimbot" + "\"");
                                }
                            }
                            else
                            {
                                attacker.SetField("left_leg_upper", 0);
                            }

                            if (hitLoc == "left_leg_lower")
                            {
                                attacker.SetField("left_leg_lower", attacker.GetField<int>("left_leg_lower") + 1);

                                if (attacker.GetField<int>("left_leg_lower") >= Convert.ToInt32(left_leg_lower) && !left_leg_lower.Equals("0"))
                                {
                                    Utilities.ExecuteCommand("banclient " + attacker.Call<int>("getentitynumber", new Parameter[0]));
                                    Utilities.ExecuteCommand("dropclient" + " " + "\"" + attacker.EntRef + "\"" + " " + "\"" + "AutoBan - AntiAimbot" + "\"");
                                }
                            }
                            else
                            {
                                attacker.SetField("left_leg_lower", 0);
                            }

                        }
                    }

                    foreach (Entity e in Playerz)
                    {
                        string team = e.GetField<string>("sessionteam");
                        switch (team)
                        {
                            case ("axis"):
                                if (!Axis.Contains(e))
                                {
                                    Axis.Add(e);
                                }
                                break;
                            case ("allies"):
                                if (!Allies.Contains(e))
                                {
                                    Allies.Add(e);
                                }
                                break;
                        }
                    }

                    string Dteam = player.GetField<string>("sessionteam");
                    switch (Dteam)
                    {
                        case ("axis"):
                            DeadAxis.Add(player);
                            break;
                        case ("allies"):
                            DeadAllies.Add(player);
                            break;
                    }
                    //sayAsBot(player, "Dead on Axis:" + DeadAxis.Count().ToString());
                    //sayAsBot(player, "Total on Axis:" + Axis.Count().ToString());
                    //sayAsBot(player, "Dead on Allies:" + DeadAllies.Count().ToString());
                    //sayAsBot(player, "Total on Allies:" + Allies.Count().ToString());

                    if (DeadAxis.Count() == Axis.Count() || DeadAllies.Count() == Allies.Count())
                    {
                        if (dvarCheck("sndkillstreak"))
                        {
                            string str3 = @"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\killstreak.txt";
                            File.WriteAllText(str3, String.Empty);
                            StreamWriter writer = new StreamWriter(str3, true);
                            foreach (Entity entity in Playerz)
                            {
                                AfterDelay(100, () =>
                                {
                                    unfreezee(player);
                                });
                                try
                                {
                                    string str4 = entity.Name + "=" + entity.GetField<int>("killstreak").ToString();
                                    writer.WriteLine(str4);
                                    // writer.Close();
                                }
                                catch (Exception error)
                                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                            }
                            writer.Close();
                        }
                    }
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }
        #endregion

        #region Utilities

        public void SuicideV1(Entity caller, string target)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                Entity found = FindByName(target);

                if (found != null)
                {
                    AfterDelay(200, () =>
                    {
                        found.Call("suicide");
                    });
                }
                else
                {
                    Utilities.RawSayTo(caller, pm + "^1Unable to locate player.");
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void infoReplace(Entity player, string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            string charstart = "!@/\\#";

            if (message.StartsWith("!") || message.StartsWith("@") || message.StartsWith("/") || message.StartsWith("\\") || message.StartsWith("#"))
            {
                charstart = message.ToCharArray()[0].ToString();
                message = message.Substring(1);
                message = charstart + " " + message;
            }


            string toghost = message;

            int demloops = message.Count(s => s == '=');

            Entity temp = null;

            toghost = Regex.Replace(toghost, "=guid", "=guid", RegexOptions.IgnoreCase);
            toghost = Regex.Replace(toghost, "=ip", "=ip", RegexOptions.IgnoreCase);
            toghost = Regex.Replace(toghost, "=rank", "=rank", RegexOptions.IgnoreCase);

            for (int x = demloops; x > 0; x--)
            {
                try
                {
                    temp = null;
                    if (toghost.IndexOf("=ip", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        temp = FindByName(toghost.Split(new string[] { "=ip" }, StringSplitOptions.None)[0].Split(' ').Last());

                        if (temp != null)
                        {
                            toghost = Regex.Replace(toghost, toghost.Split(new string[] { "=ip" }, StringSplitOptions.None)[0].Split(' ').Last() + "=ip", temp.IP.ToString().Split(':')[0], RegexOptions.IgnoreCase);
                        }
                        else
                        {
                            toghost = Regex.Replace(toghost, toghost.Split(new string[] { "=ip" }, StringSplitOptions.None)[0].Split(' ').Last() + "=ip", "noplayer", RegexOptions.IgnoreCase);
                            toghost = Regex.Replace(toghost, "=ip", " noip", RegexOptions.IgnoreCase);
                        }
                    }
                    else if (toghost.IndexOf("=guid", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        temp = FindByName(toghost.Split(new string[] { "=guid" }, StringSplitOptions.None)[0].Split(' ').Last());

                        if (temp != null)
                        {
                            toghost = Regex.Replace(toghost, toghost.Split(new string[] { "=guid" }, StringSplitOptions.None)[0].Split(' ').Last() + "=guid", temp.GUID.ToString(), RegexOptions.IgnoreCase);
                        }
                        else
                        {
                            toghost = Regex.Replace(toghost, toghost.Split(new string[] { "=guid" }, StringSplitOptions.None)[0].Split(' ').Last() + "=guid", "noplayer", RegexOptions.IgnoreCase);
                            toghost = Regex.Replace(toghost, "=guid", " noguid", RegexOptions.IgnoreCase);
                        }
                    }
                    else if (toghost.IndexOf("=rank", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        temp = FindByName(toghost.Split(new string[] { "=rank" }, StringSplitOptions.None)[0].Split(' ').Last());

                        if (temp != null)
                        {
                            toghost = Regex.Replace(toghost, toghost.Split(new string[] { "=rank" }, StringSplitOptions.None)[0].Split(' ').Last() + "=rank", getRank(temp), RegexOptions.IgnoreCase);
                        }
                        else
                        {
                            toghost = Regex.Replace(toghost, toghost.Split(new string[] { "=rank" }, StringSplitOptions.None)[0].Split(' ').Last() + "=rank", "noplayer", RegexOptions.IgnoreCase);
                            toghost = Regex.Replace(toghost, "=rank", " norank", RegexOptions.IgnoreCase);
                        }
                    }
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
            }

            if (charstart != "!@/\\#" && toghost.ToCharArray()[1] == ' ')
            {
                toghost = toghost.Substring(2);
                toghost = charstart + toghost;
            }
            fakeSay(player, player.Name, toghost);
        }

        public void rainbowText(Entity player, string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            string full = message;

            string coloured = "";

            string makeitrain = Regex.Match(full, @"\<r([^)]*)\\r>").Groups[1].Value;

            //full = full.Replace("<r", "");
            //full = full.Replace("\\r>", "");

            Random r = new Random();

            int temp = 0;

            foreach (char c in makeitrain)
            {
                temp = r.Next(1, 7);

                coloured += string.Format("^{0}{1}", temp.ToString(), c.ToString());
            }

            full = full.Replace("<r" + makeitrain + "\\r>", coloured);

            fakeSay(player, player.Name, full);
        }

        public string makeRainbow(string toConvert, int[] rands)
        {
            StreamWriter erros = new StreamWriter(fs);
            string final = "";

            try
            {
                if (rands[0] != 100)
                {
                    Random r = new Random();

                    int temp = 0;

                    foreach (char c in toConvert)
                    {
                        try
                        {
                            temp = r.Next(0, rands.Count());
                            temp = rands[temp];
                        }
                        catch
                        {
                            temp = 7;
                        }
                        if (temp != 98 && temp != 99)
                        {
                            final += string.Format("^{0}{1}", temp.ToString(), c.ToString());
                        }
                        else if (temp == 98)
                        {
                            final += string.Format("^;{0}", c.ToString());
                        }
                        else if (temp == 99)
                        {
                            final += string.Format("^:{0}", c.ToString());
                        }
                    }
                }
                else
                {
                    int counter = 1;

                    int temp = 0;

                    foreach (char c in toConvert)
                    {
                        temp = rands[counter];

                        if (temp != 98 && temp != 99)
                        {
                            final += string.Format("^{0}{1}", temp.ToString(), c.ToString());
                        }
                        else if (temp == 98)
                        {
                            final += string.Format("^;{0}", c.ToString());
                        }
                        else if (temp == 99)
                        {
                            final += string.Format("^:{0}", c.ToString());
                        }

                        if (counter >= rands.Count() - 1)
                        {
                            counter = 1;
                        }
                        else
                        {
                            counter++;
                        }
                    }
                }
            }
            catch
            {
                final = toConvert;
            }

            return final;
        }

        public void warnCount(Entity caller, string target)
        {
            StreamWriter erros = new StreamWriter(fs);
            Entity targetent = FindByName(target);
            if (targetent != null)
            {
                string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\currentwarns.warn";

                string[] lines = File.ReadAllLines(path);

                int warns = 0;

                foreach (string s in lines)
                {
                    if (s.StartsWith(targetent.GUID.ToString() + ";"))
                    {
                        try
                        {
                            warns = Convert.ToInt32(s.Split(';')[1]);
                        }
                        catch (Exception error)
                        { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                    }
                }

                Utilities.RawSayTo(caller, pm + "^4Warns for ^3" + targetent.Name + "^7: ^1" + warns.ToString());
            }
            else
            {
                Utilities.RawSayTo(caller, pm + "^1Either multiple users were found or SinScript was unable to locate user.");
            }
        }

        public void addCommand(Entity caller, string group, string command)
        {
            if (isAllowed(caller, group, command) == false)
            {
                string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\SinScript.cfg";
                string[] lines = File.ReadAllLines(path);
                bool found = false;
                int counter = -1;
                foreach (string s in lines)
                {
                    counter++;
                    if (s.StartsWith("[" + group + "=Commands="))
                    {
                        found = true;
                        lines[counter] += "," + command;
                        File.WriteAllLines(path, lines);
                        break;
                    }
                }
                if (found == true)
                {
                    Utilities.RawSayTo(caller, pm + "^2Following command^7: ^4" + command + " ^2successfully written to the ^7" + group + " ^2usergroup.");
                }
                else
                {
                    Utilities.RawSayTo(caller, pm + "^1Unable to locate group to add command.");
                }
            }
            else
            {
                Utilities.RawSayTo(caller, pm + "^2Group is already allowed to use that command.");
            }
        }

        public void sayTeam(Entity player, string message)
        {
            string team = player.GetField<string>("sessionteam");

            bool alive = player.IsAlive;

            string name = player.Name;
            if (getAlias(player.Name) != "GhostyBeTrippin..........")
            {
                name = getAlias(player.Name);
            }

            foreach (Entity ent in Playerz)
            {
                if (ent.GetField<string>("sessionteam") == team)
                {
                    if (alive == true)
                    {
                        Utilities.RawSayTo(ent, "^0(^3TEAM^0)^8" + name + "^7: ^5" + message);
                    }
                    else
                    {
                        if (dvarCheck("deadchat") == true)
                        {
                            Utilities.RawSayTo(ent, "^0(^3TEAM^0)^1(Dead)" + name + "^7: ^5" + message);
                        }
                        else
                        {
                            if (!ent.IsAlive)
                            {
                                Utilities.RawSayTo(ent, "^0(^3TEAM^0)^1(Dead)" + name + "^7: ^5" + message);
                            }
                        }
                    }
                }
            }
        }

        public void setBanBot()
        {
            for (int x = 4; x > 0; x--)
            {
                while (banbot.EndsWith(" "))
                {
                    if (banbot != "")
                    {
                        banbot = banbot.Remove(banbot.Length - 1);
                    }
                    else
                    {
                        break;
                    }
                }
                while (banbot.EndsWith(":"))
                {
                    if (banbot != "")
                    {
                        banbot = banbot.Remove(banbot.Length - 1);
                    }
                    else
                    {
                        break;
                    }
                }
                while (banbot.EndsWith(";"))
                {
                    if (banbot != "")
                    {
                        banbot = banbot.Remove(banbot.Length - 1);
                    }
                    else
                    {
                        break;
                    }
                }
                while (banbot.EndsWith("="))
                {
                    if (banbot != "")
                    {
                        banbot = banbot.Remove(banbot.Length - 1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }


        public string getAlias(string playerName)
        {
            StreamWriter erros = new StreamWriter(fs);

            string returnAlias = "GhostyBeTrippin..........";
            if (aliasplayersv2.Contains(playerName))
            {
                foreach (string aliasstring in aliasplayers)
                {
                    if (aliasstring.StartsWith(playerName + "="))
                    {
                        string[] aliastemp = aliasstring.Split('=');
                        returnAlias = aliastemp[1];
                        break;
                    }
                }
            }

            //here
            if (returnAlias.StartsWith("<r") && returnAlias.EndsWith("\\r>"))
            {
                try
                {

                    returnAlias = returnAlias.Substring(2);
                    returnAlias = returnAlias.Remove(returnAlias.Length - 3);

                    if (returnAlias.Split(new string[] { "{\\0}" }, StringSplitOptions.None)[0].Contains("{0}"))
                    {
                        for (int x = 0; x <= 999; x++)
                        {
                            try
                            {
                                string temp = returnAlias.Split(new string[] { "{\\" + x.ToString() + "}" }, StringSplitOptions.None)[0].Split(new string[] { "{" + x.ToString() + "}" }, StringSplitOptions.None)[1];

                                string temp2 = temp;

                                if (temp.Contains("<c>"))
                                {
                                    string colours = temp.Split(new string[] { "<c>" }, StringSplitOptions.None)[1];
                                    temp = temp.Split(new string[] { "<c>" }, StringSplitOptions.None)[0];

                                    // The pirates go aaarr
                                    // Math pirate go chaaarr?
                                    char[] chaaaarrr = new char[colours.Length];

                                    for (int xi = colours.Length - 1; xi >= 0; xi--)
                                    {
                                        try
                                        {
                                            chaaaarrr[xi] = colours.ToCharArray()[xi];
                                        }
                                        catch (Exception error)
                                        { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                                    }

                                    int[] sendme = new int[colours.Length];

                                    int counter = -1;

                                    foreach (char c in chaaaarrr)
                                    {
                                        counter++;
                                        try
                                        {
                                            if (c != ';' && c != ':')
                                            {
                                                sendme[counter] = Convert.ToInt32(c.ToString());
                                            }
                                            else if (c == ';')
                                            {
                                                sendme[counter] = 98;
                                            }
                                            else if (c == ':')
                                            {
                                                sendme[counter] = 99;
                                            }
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                sendme[counter] = 7;
                                            }
                                            catch (Exception error)
                                            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                                        }
                                    }

                                    if (temp.EndsWith("<s>"))
                                    {
                                        int[] sendme2 = new int[sendme.Count() + 1];

                                        int counter2 = 1;

                                        sendme2[0] = 100;

                                        foreach (int i in sendme)
                                        {
                                            sendme2[counter2] = i;

                                            counter2++;
                                        }

                                        temp = temp.Remove(temp.Length - 3);

                                        temp = makeRainbow(temp, sendme2);
                                    }
                                    else
                                    {
                                        temp = makeRainbow(temp, sendme);
                                    }
                                }
                                else
                                {
                                    int[] intarr = new int[6];

                                    for (int xin = 0; xin < 6; xin++)
                                    {
                                        try
                                        {
                                            intarr[xin] = xin + 1;
                                        }
                                        catch (Exception error)
                                        { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                                    }

                                    temp = makeRainbow(temp, intarr);
                                }

                                returnAlias = returnAlias.Replace("{" + x.ToString() + "}" + temp2 + "{\\" + x.ToString() + "}", temp);
                            }
                            catch
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (returnAlias.Contains("<c>"))
                        {
                            string colours = returnAlias.Split(new string[] { "<c>" }, StringSplitOptions.None)[1];
                            returnAlias = returnAlias.Split(new string[] { "<c>" }, StringSplitOptions.None)[0];

                            // The pirates go aaarr
                            // Math pirate go chaaarr?
                            char[] chaaaarrr = new char[colours.Length];

                            for (int x = colours.Length - 1; x >= 0; x--)
                            {
                                try
                                {
                                    chaaaarrr[x] = colours.ToCharArray()[x];
                                }
                                catch (Exception error)
                                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                            }

                            int[] sendme = new int[colours.Length];

                            int counter = -1;

                            foreach (char c in chaaaarrr)
                            {
                                counter++;
                                try
                                {
                                    if (c != ';' && c != ':')
                                    {
                                        sendme[counter] = Convert.ToInt32(c.ToString());
                                    }
                                    else if (c == ';')
                                    {
                                        sendme[counter] = 98;
                                    }
                                    else if (c == ':')
                                    {
                                        sendme[counter] = 99;
                                    }
                                }
                                catch
                                {
                                    try
                                    {
                                        sendme[counter] = 7;
                                    }
                                    catch (Exception error)
                                    { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                                }
                            }

                            if (returnAlias.EndsWith("<s>"))
                            {
                                int[] sendme2 = new int[sendme.Count() + 1];

                                int counter2 = 1;

                                sendme2[0] = 100;

                                foreach (int i in sendme)
                                {
                                    sendme2[counter2] = i;

                                    counter2++;
                                }

                                returnAlias = returnAlias.Remove(returnAlias.Length - 3);

                                returnAlias = makeRainbow(returnAlias, sendme2);
                            }
                            else
                            {
                                returnAlias = makeRainbow(returnAlias, sendme);
                            }

                            /*
                             * // ALTERNATIVE FUCKING PIECE OF SHIT... PROBABLY MORE EFFICIENT BUT I DON'T GIVE A FUUUUUUCK
                             * 
                             * 
                             * string colours = returnAlias.Split(new string[] { "<c>" }, StringSplitOptions.None)[1];
                            returnAlias = returnAlias.Split(new string[] { "<c>" }, StringSplitOptions.None)[0];

                            char[] schar = colours.ToCharArray();

                            int[] coloursArr = new int[schar.Count()];

                            int counter = -1;

                            foreach (char c in schar)
                            {
                                counter++;
                                coloursArr[counter] = Convert.ToInt32(c.ToString());
                            }

                            returnAlias = makeRainbow(returnAlias, coloursArr);
                             * */
                        }
                        else
                        {
                            int[] intarr = new int[6];

                            for (int x = 0; x < 6; x++)
                            {
                                try
                                {
                                    intarr[x] = x + 1;
                                }
                                catch (Exception error)
                                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                            }

                            returnAlias = makeRainbow(returnAlias, intarr);
                        }
                    }
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
            }
            //& here


            return returnAlias;
        }

        public void spidireset(Entity ent)
        {
            ent.SetField("kills", 0);
            ent.SetField("Kills", 0);
            ent.SetField("Kill", 0);
            ent.SetField("deaths", 0);
            ent.SetField("Deaths", 0);
            ent.SetField("death", 0);
        }

        public void removeOldWarns()
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\currentwarns.warn";
                string[] lines = File.ReadAllLines(path);

                List<string> currentguids = new List<string>();

                foreach (Entity e in Playerz)
                {
                    if (!currentguids.Contains(e.GUID.ToString()))
                    {
                        currentguids.Add(e.GUID.ToString());
                    }
                }

                int counter = -1;

                foreach (string s in lines)
                {
                    counter++;

                    if (!currentguids.Contains(s.Split(';')[0]))
                    {
                        lines[counter] = "";
                    }
                }

                File.WriteAllLines(path, lines);
            }
            catch
            {
                try
                {
                    string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\currentwarns.warn";
                    File.Delete(path);

                    //File.WriteAllBytes(path, new byte[0]);
                    StreamWriter sw = new StreamWriter(path, true);
                    sw.WriteLine("");
                    sw.Close();
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
            }
        }

        public void foreachPlayer(Entity caller, string fakesay)
        {
            fakesay = fakesay.Replace(" DefaultKickMessage", "");
            foreach (Entity player in Playerz)
            {
                fakeSay(caller, player.Name, fakesay);
            }
        }

        public void crosshair(Entity caller, string action)
        {
            if (action.ToLower() == "on")
            {
                caller.SetClientDvar("ui_drawcrosshair", "1");
                Utilities.RawSayTo(caller, pm + "^2Crosshair Enabled.");
            }
            else if (action.ToLower() == "off")
            {
                caller.SetClientDvar("ui_drawcrosshair", "0");
                Utilities.RawSayTo(caller, pm + "^1Crosshair Disabled.");

            }
            else
            {
                Utilities.RawSayTo(caller, pm + "^1Please type either 'on' or 'off'. Example: !crosshair on");
            }
        }

        public void forceCommand(Entity caller, string target, string command)
        {
            if (command != "DefaultKickMessage")
            {
                if (isAllowed(caller, getRank(caller), command) && isBlockedCommand(command.ToLower()) == false)
                {
                    if (!command.StartsWith("!"))
                    {
                        command = "!" + command;
                    }

                    justvoted = true;

                    fakeSay(caller, target, command);
                }
                else
                {
                    Utilities.RawSayTo(caller, pm + "^1You are not allowed to use or force that command.");
                }
            }
            else
            {
                Utilities.RawSayTo(caller, pm + "^1Please enter a command to force.");
            }
        }

        public void target(Entity caller, string target)
        {
            Entity found = FindByName(target);
            if (found != null)
            {
                OnInterval(250, () =>
                {
                    try
                    {
                        if (found.IsAlive)
                        {
                            var disorigin = found.Origin;
                            var disori2 = found.Origin;

                            disori2.Z += 20;
                            disorigin.Z -= 15;

                            //Call("magicbullet", "uav_strike_projectile_mp", dest1, dest0, player);
                            Call("magicbullet", caller.CurrentWeapon, disori2, disorigin, caller);

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                });
            }
            else
            {
                Utilities.RawSayTo(caller, pm + "^1Unable to locate player");
            }
        }

        public void mutelist(Entity caller)
        {
            string temp = "^3Playerz muted^7: ";

            foreach (Entity e in Playerz)
            {
                if (isMuted(e) == true)
                {
                    temp += "^1" + e.Name + "^7, ";
                }
            }

            if (temp.EndsWith("^7, "))
            {
                temp = temp.Remove(temp.Length - 4);
            }
            else
            {
                temp = "^3There are currently no players muted";
            }

            Utilities.RawSayTo(caller, pm + temp);
        }

        public void blocklist(Entity caller)
        {
            string temp = "^3Blocked players^7: ";

            foreach (Entity e in Playerz)
            {
                if (isBlocked(e) == true)
                {
                    temp += "^1" + e.Name + "^7, ";
                }
            }

            if (temp.EndsWith("^7, "))
            {
                temp = temp.Remove(temp.Length - 4);
            }
            else
            {
                temp = "^3There are currently no players blocked";
            }

            Utilities.RawSayTo(caller, pm + temp);
        }

    

        public string encrypt(Entity caller)
        {
            string toret = bleh();

            TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();

            byte[] byteHash, byteBuff;
            string strTempKey = "abc123";

            byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            objHashMD5 = null;
            objDESCrypto.Key = byteHash;
            objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB

            byteBuff = ASCIIEncoding.ASCII.GetBytes(toret);
            return Convert.ToBase64String(objDESCrypto.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
        }

        public string decrypt2(string str)
        {
            string toret = str;
            for (int x = 2; x >= 0; x--)
            {
                toret = decrypt(toret);
            }

            return toret;
        }

        public string decrypt(string str)
        {
            TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();

            byte[] byteHash, byteBuff;
            string strTempKey = "abc123";

            byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            objHashMD5 = null;
            objDESCrypto.Key = byteHash;
            objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB

            byteBuff = Convert.FromBase64String(str);
            string strDecrypted = ASCIIEncoding.ASCII.GetString(objDESCrypto.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            objDESCrypto = null;

            return strDecrypted;
        }

        public void hidePlayer(Entity caller, string target)
        {
            if (target.ToLower() == "off")
            {
                foreach (Entity e in Playerz)
                {
                    e.Call("show");
                }
                Utilities.RawSayAll(bot + "^3All players are now visible.");
            }
            else
            {
                Entity hide = FindByName(target);
                if (hide != null)
                {
                    hide.Call("hide");
                    Utilities.RawSayTo(hide, pm + "^2You are now invisible!");

                    if (hide != caller)
                    {
                        Utilities.RawSayTo(caller, pm + "^7" + hide.Name + " ^2is now invisible.");
                    }
                }
                else
                {
                    Utilities.RawSayTo(caller, pm + "^1Either multiple users were found or SinScript was unable to locate user.");
                }
            }
        }

        public void setspeed(Entity caller, string speedmult)
        {
            try
            {
                double setdouble = Convert.ToDouble(floatDvar("speedmultiplier"));
                float setfloat = (float)setdouble;

                string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\SinScript.cfg";

                string[] lines = File.ReadAllLines(path);
                int counter = -1;

                bool found = false;

                foreach (string s in lines)
                {
                    counter++;
                    if (s.ToLower().StartsWith("[speedmultiplier]"))
                    {
                        lines[counter] = "[SpeedMultiplier];:;" + setdouble.ToString();
                        File.WriteAllLines(path, lines);
                        Utilities.RawSayAll(bot + "^5Speed multiplier set to: ^7" + speedmult);
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    StreamWriter sw = new StreamWriter(path, true);
                    sw.WriteLine("");
                    sw.WriteLine("[SpeedMultiplier];:;1.0");
                    sw.Close();
                    Utilities.RawSayTo(caller, pm + "^31.0 speed multiplier written to config.");
                }
            }
            catch
            {
                Utilities.RawSayTo(caller, pm + "^1Error converting " + speedmult + " to a 1.0 percent format");
            }
        }

        public void disSpeed()
        {
            StreamWriter erros = new StreamWriter(fs);
            OnInterval(200, () =>
            {
                try
                {
                    foreach (Entity player in Playerz)
                    {
                        player.Call("setmovespeedscale", new Parameter(floatDvar("speed")));
                    }
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                return true;
            });
        }

        public void wallhack(Entity player, string target)
        {
            StreamWriter erros = new StreamWriter(fs);
            if (target == "off")
            {
                foreach (Entity e in Playerz)
                {
                    e.Call("thermalvisionfofoverlayoff");
                }
            }
            else
            {
                Entity temp = FindByName(target);

                if (temp != null)
                {
                    temp.Call("thermalvisionfofoverlayon");
                    Utilities.RawSayTo(temp, pm + "^3Wallhack enabled.");
                    if (temp != player)
                    {
                        Utilities.RawSayTo(player, pm + "^2Wallhack enabled for^7: " + temp.Name);
                    }
                }
                else
                {
                    Utilities.RawSayTo(player, pm + "^1Either multiple users were found or SinScript was unable to locate user.");
                }
            }
        }

        public void reservedComm(string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            for (int fuckerscounter = 0; fuckerscounter < 18; fuckerscounter++)
            {
                try
                {
                    if (getRank(Playerz[fuckerscounter]) == "User")
                    {
                        kickComms(Playerz[fuckerscounter], Playerz[fuckerscounter].Name, banbot, message, "drop");
                        break;
                    }
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
            }
        }

        public void reservedComm2idkwhy(Entity tokick, string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                kickComms(tokick, tokick.Name, bot, message, "drop");
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void addConfigOpt(Entity adder, string option)
        {
            option = option.Replace(" DefaultKickMessage", "");

            string path = @"scripts\\" + "SinAdmin\\" + cfgname + "\\SinScript.cfg";

            StreamWriter sw = new StreamWriter(path, true);

            sw.WriteLine("");
            sw.WriteLine(option);
            sw.Close();

            Utilities.RawSayTo(adder, pm + "^5Following new line written to config^7: " + option);
        }

        public void blockComms(Entity issuer, string target, string action)
        {
            string blockpath = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\blocked.txt";
            if (File.Exists(blockpath))
            {
                //File.WriteAllBytes(blockpath, new byte[0]);
                StreamWriter sw = new StreamWriter(blockpath, true);
                sw.WriteLine("");
                sw.Close();
            }
            Entity ent = FindByName(target);
            if (ent != null)
            {
                if (action == "block")
                {
                    var removewhites = File.ReadAllLines(blockpath).Where(arg => !string.IsNullOrWhiteSpace(arg));
                    File.WriteAllLines(blockpath, removewhites);
                    StreamWriter sw = new StreamWriter(blockpath, true);
                    sw.WriteLine("");
                    sw.WriteLine(ent.GUID.ToString());
                    sw.Close();
                    blockedplayers.Add(ent.GUID);
                    Utilities.RawSayAll(bot + "^1Commands have been blocked for: ^7" + ent.Name);
                }
                else if (action == "unblock")
                {
                    string[] lines = File.ReadAllLines(blockpath);
                    int counter = 0;
                    foreach (string s in lines)
                    {
                        if (s == ent.GUID.ToString())
                        {
                            lines[counter] = "";
                            File.WriteAllLines(blockpath, lines);
                        }
                        counter++;
                    }
                    blockedplayers.Remove(ent.GUID);
                    Utilities.RawSayAll(bot + "^1Commands have been unblocked for: ^7" + ent.Name);
                }
            }
            else
            {
                Utilities.RawSayTo(issuer, pm + "^1Either multiple users were found or SinScript was unable to locate user.");
            }
        }

        public bool isBlocked(Entity player)
        {

            if (blockedplayers.Contains(player.GUID))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void spinMe(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                string myteam = player.GetField<string>("sessionteam");

                int updatecounter = 0;

                //bool readykill = false;

                bool ishigher = false;
                bool goneonce = false;

                float oldheight = 99999F;
                //float newheight = 0F;

                OnInterval(25, () =>
                {
                    try
                    {
                        if (ishigher == false)
                        {
                            if (player.Origin.Z > oldheight)
                            {
                                ishigher = true;
                            }
                            else
                            {
                                oldheight = player.Origin.Z;
                            }
                        }

                        if (player.IsAlive && ishigher == true)
                        {
                            Vector3 toset = player.Call<Vector3>("getplayerangles");

                            toset.Y -= 75;

                            player.Call("setplayerangles", toset);
                            updatecounter++;
                            if ((player.Call<int>("attackbuttonpressed") > 0) && goneonce == false)
                            {
                                goneonce = true;
                                foreach (Entity tokill in Playerz)
                                {
                                    try
                                    {
                                        if (tokill.IsAlive)
                                        {
                                            try
                                            {

                                                var disorigin = tokill.Origin;
                                                var disori2 = tokill.Origin;

                                                disori2.Z += 20;
                                                disorigin.Z -= 15;

                                                //Call("magicbullet", "uav_strike_projectile_mp", dest1, dest0, player);
                                                Call("magicbullet", player.CurrentWeapon, disori2, disorigin, player);
                                            }
                                            catch (Exception error)
                                            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                                        }
                                    }
                                    catch
                                    {
                                        return false;
                                    }
                                }
                                if (updatecounter > 35)
                                {
                                    return false;
                                }
                            }
                            if (updatecounter > 35)
                            {
                                return false;
                            }
                            //Utilities.RawSayTo(player, updatecounter.ToString());
                            return true;
                        }
                        else if (player.IsAlive)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                });
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void load_ul(Entity caller, string dsr, string action)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                switch (action.ToLower())
                {
                    case ("load"):
                        if (File.Exists("scripts\\" + dsr))
                        {
                            Utilities.ExecuteCommand("loadscript " + dsr);
                            restart();
                            Utilities.RawSayAll(bot + "^4.dsr file loaded: " + dsr);
                            //ScriptLoader.LoadScripts();
                        }
                        else
                        {
                            Utilities.RawSayTo(caller, pm + "^1Unable to locate .dsr file");
                        }
                        break;
                    case ("unload"):
                        if (File.Exists("scripts\\" + dsr))
                        {
                            Utilities.ExecuteCommand("unloadscript " + dsr);
                            restart();
                            Utilities.RawSayAll(bot + "^4.dsr file unloaded: " + dsr);
                        }
                        else
                        {
                            Utilities.RawSayTo(caller, pm + "^1Unable to locate .dsr file");
                        }
                        break;
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public string bleh()
        {
            return DateTime.Now.ToString("ddmmyy");
        }

        public void scream(string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            message = message.Replace(" DefaultKickMessage", "");

            int colourcount = -2;

            string screamsay = "";

            OnInterval(300, () =>
            {
                try
                {
                    if (colourcount < 0)
                    {
                        if (colourcount == -2)
                        {
                            screamsay = "^;";
                        }
                        else if (colourcount == -1)
                        {
                            screamsay = "^:";
                        }
                    }
                    else
                    {
                        screamsay = "^" + colourcount.ToString();
                    }
                    if (colourcount < 10)
                    {
                        Utilities.RawSayAll(bot + screamsay + message);
                        colourcount++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            });
        }

        public void startvote(Entity user, string command)
        {
            StreamWriter erros = new StreamWriter(fs);
            if (inprogress == false)
            {
                justvoted = false;
                cancelvote = false;
                forcevote = false;

                if (command.StartsWith("!"))
                {
                    command = command.Substring(1);
                }

                command = command.Replace(" DefaultKickMessage", "");

                yesvotes.Clear();

                string[] avc = votecommands.Split(',');
                bool allowed = false;

                foreach (string s in avc)
                {
                    if (s.ToLower() == command.ToLower().Split(' ')[0])
                    {
                        allowed = true;
                        break;
                    }
                }

                if (voting == true)
                {
                    if (allowed == true)
                    {
                        votecaller = user;
                        inprogress = true;
                        votecommand = command;
                        votethingy();
                        voteoptions(user, "yes");
                    }
                    else
                    {
                        Utilities.RawSayTo(user, pm + "^1Voting not enabled for that command");
                    }
                }
                else
                {
                    Utilities.RawSayTo(user, pm + "^1Voting is currently not enabled.");
                }
            }
            else
            {
                Utilities.RawSayTo(user, pm + "^1There is already a vote in progress.");
            }
        }


        public void voteoptions(Entity caller, string option)
        {
            StreamWriter erros = new StreamWriter(fs);
            if (inprogress == true)
            {
                switch (option.ToLower())
                {
                    case ("cancel"):
                        cancelvote = true;
                        break;
                    case ("forcepass"):
                        forcevote = true;
                        break;
                    case ("yes"):
                        if (!yesvotes.Contains(caller))
                        {
                            yesvotes.Add(caller);
                            Utilities.RawSayTo(caller, pm + "^2You have voted yes.");
                        }
                        else
                        {
                            Utilities.RawSayTo(caller, pm + "^1You have already voted.");
                        }
                        break;
                    case ("no"):
                        Utilities.RawSayTo(caller, pm + "^3All players already cast as ^7'^1no^7' ^3as default");
                        break;
                }
            }
            else
            {
                Utilities.RawSayTo(caller, pm + "^1No vote in progress.");
            }
        }

        public void votethingy()
        {
            int countdown = 60;
            OnInterval(5000, () =>
            {
                if (cancelvote == false)
                {
                    if (forcevote == false)
                    {
                        if (countdown > -1)
                        {
                            foreach (Entity e in Playerz)
                            {
                                if ((Convert.ToDouble(countdown) / 10.0) % 1 != 0)
                                {
                                    e.Call("iprintlnbold", new Parameter[]
		            	        {
			                    	"^5Vote in progress(" + countdown.ToString() + "), type ^2!yes ^5to agree. ^2yes: " + yesvotes.Count
		                    	});
                                }
                                else
                                {
                                    e.Call("iprintlnbold", new Parameter[]
		            	        {
			                    	"^4Current vote command: ^2!" + votecommand
		                    	});
                                }
                            }
                            countdown = countdown - 5;
                            return true;
                        }
                        else
                        {
                            double tell = (Convert.ToDouble(yesvotes.Count) / Convert.ToDouble(Playerz.Count)) * 100.0;

                            if (tell >= votethreshold)
                            {
                                foreach (Entity e in Playerz)
                                {
                                    e.Call("iprintlnbold", new Parameter[]
		                        	{
			                        	"^2Vote passed!"
		                        	});
                                }

                                Utilities.RawSayAll(bot + "^2Vote passed!");
                                justvoted = true;
                                votecommand = "!" + votecommand;
                                OnSay3(votecaller, ChatType.All, "ghosty1234567890VOTING", ref votecommand);
                            }
                            else
                            {
                                double per_vote = 100 / Playerz.Count;
                                int needed = 0;
                                double temptell = tell;

                                while (temptell < votethreshold)
                                {
                                    needed++;
                                    temptell += per_vote;
                                }

                                Utilities.RawSayAll(bot + "^1Vote failed: " + needed.ToString() + " more vote(s) needed");
                            }
                            inprogress = false;

                            return false;
                        }
                    }
                    else
                    {
                        Utilities.RawSayAll(bot + "^2Vote has been force passed");
                        foreach (Entity e in Playerz)
                        {
                            e.Call("iprintlnbold", new Parameter[]
		                        	{
			                        	"^2Vote passed."
		                        	});
                        }

                        justvoted = true;
                        votecommand = "!" + votecommand;
                        OnSay3(votecaller, ChatType.All, "ghosty1234567890VOTING", ref votecommand);
                        inprogress = false;
                        return false;
                    }
                }
                else
                {
                    Utilities.RawSayAll(bot + "^1Vote has been cancelled");
                    foreach (Entity e in Playerz)
                    {
                        e.Call("iprintlnbold", new Parameter[]
		                        	{
			                        	"^1Vote cancelled."
		                        	});
                    }
                    inprogress = false;
                    return false;
                }
            });
        }

        public void tracer(Entity player)
        {
            Function.AddMapping("bullettrace", 88);
            Entity target = null;
            float leastd = 0F;

            string team = player.GetField<string>("sessionteam");
            string otherteam = "";

            OnInterval(35, () =>
            {
                try
                {
                    if (usingaimbot == false)
                    {
                        return false;
                    }

                    leastd = 999999F;
                    target = null;

                    foreach (Entity pot in Playerz)
                    {
                        otherteam = pot.GetField<string>("sessionteam");
                        if (pot != player && pot.IsAlive && team != otherteam)
                        {
                            float distance = pot.Origin.DistanceTo(player.Origin);
                            if (distance < leastd)
                            {
                                leastd = distance;
                                target = pot;
                            }
                        }
                    }
                    if (!player.IsAlive || target == null)
                    {
                        if (!player.IsAlive)
                        {

                        }
                        else if (target == null)
                        {

                        }
                        return false;
                    }
                    return true;
                }
                catch
                {
                    Utilities.RawSayTo(player, pm + "unknown tracer error");
                    return false;
                }
            });

            OnInterval(100, () =>
            {
                bool hitormiss = player.Call<bool>("bullettrace", player.Origin, target.Origin, true, player);
                Utilities.SayTo(player, hitormiss.ToString());
                return true;
            });
        }

        public void aimbot(Entity issuer, Entity player)
        {
            usingaimbot = true;

            Entity target = null;
            float leastd = 0F;

            string team = player.GetField<string>("sessionteam");
            string otherteam = "";

            var mode = Call<string>("getdvar", "g_gametype");

            OnInterval(5, () =>
            {
                try
                {
                    if (usingaimbot == false)
                    {
                        return false;
                    }

                    leastd = 999999F;
                    target = null;

                    foreach (Entity pot in Playerz)
                    {
                        otherteam = pot.GetField<string>("sessionteam");

                        if (pot != player && pot.IsAlive && (team != otherteam || mode == "dm"))
                        {
                            float distance = pot.Origin.DistanceTo(player.Origin);
                            if (distance < leastd)
                            {
                                leastd = distance;
                                target = pot;
                            }
                        }
                    }

                    if (dvarCheck("botmod") == true)
                    {
                        foreach (Entity pot in BotList)
                        {
                            if (pot.IsAlive)
                            {
                                float distance = pot.Origin.DistanceTo(player.Origin);
                                if (distance < leastd)
                                {
                                    leastd = distance;
                                    target = pot;
                                }
                            }
                        }
                    }

                    if (!player.IsAlive || target == null)
                    {
                        if (!player.IsAlive)
                        {
                            Utilities.RawSayTo(player, pm + "^1You are dead, aimbot disabled.");
                        }
                        else if (target == null)
                        {
                            Utilities.RawSayTo(player, pm + "^1No more players left, aimbot disabled.");
                        }
                        return false;
                    }
                    //Utilities.RawSayTo(player, "closest enemy: " + leastd.ToString());

                    Vector3 p1 = player.Origin;
                    Vector3 p2 = target.Call<Vector3>("gettagorigin", "j_head");

                    //p2.Z = p2.Z - 15;

                    //var agid0 = (Math.Atan2(p2.Y - p1.Y, p2.X - p1.X) * 180) / Math.PI;

                    /*
                    float SA = p1.Z - p2.Z;
                    float SB = p1.DistanceTo2D(p2);
                    float SC = p1.DistanceTo(p2);
                    var agid1 = (float)((Math.Atan2(SA, SB)) * 90);
                    */
                    //var agid1 = (Math.Atan2(p2.Y - p1.Y, p2.Z - p1.Z) * 180) / Math.PI;


                    var to = target.Origin - player.Origin;

                    Vector3 temp = Call<Vector3>("vectortoangles", new Parameter(p2));

                    //Vector3 angles = new Vector3(temp.X, temp.Y, temp.Z);

                    player.Call("SetPlayerAngles", temp);


                    //Vector3 toset = player.Call<Vector3>("getplayerangles");

                    //toset.Y = (float)agid0;
                    //toset.X = (float)agid1;
                    //Utilities.RawSayTo(player, agid1.ToString());
                    //Utilities.RawSayTo(player, toset.X.ToString());
                    //Utilities.RawSayTo(player, test.ToString());
                    //player.Call("setplayerangles", toset);
                    return true;
                }
                catch
                {
                    Utilities.RawSayTo(player, pm + "unknown aimbot error");
                    return false;
                }
            });
        }

        public void recoildet(Entity player)
        {
            //Vector3 angles = player.Call<Vector3>("getplayerangles");
            //float recoil = (float)angles.Z;
            //sayAsBot(gypsy, recoil.ToString());
            if (!fuckdis.Contains(player))
            {
                AfterDelay(50, () =>
                {

                    fuckdis.Add(player);

                    string weapons = player.CurrentWeapon;
                    if ((weapons.Contains("iw5_l96a1") || weapons.Contains("iw5_msr")) && !weapons.Contains("silen"))
                    {
                        if (!hasrecoil.Contains(player))
                        {
                            if (!rlr.Contains<string>(player.Name + ":" + player.GUID.ToString()))
                            {
                                rlr.Add(player.Name + ":" + player.GUID.ToString());
                            }
                            Vector3 angles = player.Call<Vector3>("getplayerangles");
                            float recoil = (float)angles.Z;
                            //sayAsBot(gypsy, recoil.ToString());
                            if (recoil > 0)
                            {
                                hasrecoil.Add(player);
                            }
                            else
                            {
                                if (!File.Exists(recoilpath))
                                {
                                    //File.WriteAllBytes(recoilpath, new byte[0]);
                                    StreamWriter sw = new StreamWriter(recoilpath, true);
                                    sw.WriteLine("");
                                    sw.Close();
                                }

                                string[] rlines = File.ReadAllLines(recoilpath);

                                bool found = false;
                                int counter = -1;
                                foreach (string s in rlines)
                                {
                                    counter++;
                                    if (s.StartsWith(player.Name + ":" + player.GUID.ToString() + ":"))
                                    {
                                        found = true;

                                        string[] dissplit = s.Split(':');

                                        int warns = Convert.ToInt32(dissplit[2]);

                                        warns++;

                                        if (warns > 1000)
                                        {
                                            if (getRank(player) == "User")
                                            {
                                                kickComms(player, player.Name, banbot, "No recoil detected", "ban");
                                            }
                                            else
                                            {
                                                Utilities.RawSayAll(bot + "^1Possible no recoil detected for^7: " + player.Name);
                                            }

                                            rlines[counter] = player.Name + ":" + player.GUID.ToString() + ":0";
                                        }
                                        else
                                        {
                                            rlines[counter] = player.Name + ":" + player.GUID.ToString() + ":" + warns.ToString();
                                        }
                                        File.WriteAllLines(recoilpath, rlines);
                                        break;
                                    }
                                }

                                if (found == false)
                                {
                                    var removewhites = File.ReadAllLines(recoilpath).Where(arg => !string.IsNullOrWhiteSpace(arg));
                                    File.WriteAllLines(recoilpath, removewhites);

                                    StreamWriter sw = new StreamWriter(recoilpath, true);
                                    sw.WriteLine("");
                                    sw.WriteLine(player.Name + ":" + player.GUID.ToString() + ":1");
                                    sw.WriteLine("");
                                    sw.Close();
                                }
                            }
                            //Utilities.RawSayTo(player, recoil.ToString());
                        }
                        else if (rlr.Contains<string>(player.Name + ":" + player.GUID.ToString()))
                        {
                            int counter = -1;
                            string[] rlines2 = File.ReadAllLines(recoilpath);

                            foreach (string s in rlines2)
                            {
                                counter++;
                                if (s.StartsWith(player.Name + ":" + player.GUID.ToString()))
                                {
                                    rlines2[counter] = "";
                                    File.WriteAllLines(recoilpath, rlines2);
                                    break;
                                }
                            }
                            rlr.Remove(player.Name + ":" + player.GUID.ToString());
                        }
                    }
                });
            }
        }

        public void reportchat(Entity player, string numlines)
        {
            string readhist = @"logs\\all.log";
            string reportpath = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\ChatReports.txt";

            if (!File.Exists(reportpath))
            {
                //File.WriteAllBytes(reportpath, new byte[0]);
                StreamWriter sw = new StreamWriter(reportpath, true);
                sw.WriteLine("");
                sw.Close();
            }

            try
            {
                int number = Convert.ToInt32(numlines);
                if (number <= 100 && number >= 1)
                {
                    List<string> lines = File.ReadLines(readhist).Reverse().Take(number).ToList();
                    lines.Reverse();
                    //string[] lines = File.ReadLines(readhist).Reverse().Take(number);

                    StreamWriter rw = new StreamWriter(reportpath, true);

                    rw.WriteLine("");
                    rw.WriteLine("");
                    rw.WriteLine("[CHAT REPORT]");
                    rw.WriteLine("[REPORTED BY: " + player.Name + "]");
                    rw.WriteLine("");
                    rw.WriteLine("[" + DateTime.Now.ToString("M/d/yyy") + " :: " + DateTime.Now.ToString("HH:mm:ss") + "]");
                    rw.WriteLine("");
                    rw.WriteLine("PREVIOUS " + number.ToString() + " LINES OF CHAT ACTIVITY:");
                    foreach (string tw in lines)
                    {
                        rw.WriteLine(tw);
                    }
                    rw.WriteLine("");
                    rw.WriteLine("");
                    rw.WriteLine("");

                    rw.Close();

                    Utilities.RawSayTo(player, pm + "Previous " + number.ToString() + " messages have been reported, please notify an administrator to retrieve the report.");
                }
                else
                {
                    Utilities.RawSayTo(player, pm + "^1You may only report up to 100 messages or a minimum or 1 message.");
                }
            }
            catch
            {
                Utilities.RawSayTo(player, pm + "^1Please enter a valid number of messages to report.");
            }
        }

        public void sex(Entity player)
        {
            string[] sex = File.ReadAllLines(@"scripts\sex.txt");
            List<string> porn = new List<string>();
            foreach (string dick in sex)
            {
                porn.Add(dick);
            }

            Random tits = new Random();
            int boobehs = tits.Next(0, porn.Count);
            string cum = porn.ElementAt(boobehs);
            Utilities.RawSayTo(player, "^6[King] Enjoy the porn! Fap hard bitch xD... Twistys ftw");
            Utilities.RawSayTo(player, cum);
        }

        public void badnameshit(Entity player)
        {
            // InfinityScript renames a small amount of hackers to "thisguyhax" I don't wanna pick out the colours and such so if contains eest fein
            if (player.Name.ToLower().Contains("thisguyhax.") || player.Name.ToLower().Contains("mw2player") || player.Name.ToLower().Contains("kenshin") || player.Name.ToLower().Contains("hkdavy") || player.Name.ToLower().Contains("    ") || player.Name.ToLower().Contains("[hk]") || player.Name.ToLower().Contains("acidshout"))
            {
                kickComms(player, player.Name, banbot, "Piss off, hacker.", "ban");
                ipban(player.Name);
            }
            // ^ dis right here
            // dis below is better
            foreach (string name in badnames)
            {
                if (player.Name.ToLower().Contains(name.ToLower()))
                {
                    AfterDelay(750, () =>
                    {
                        if (stringDvar("badnameaction") == "ban")
                        {
                            Utilities.ExecuteCommand("ban " + "\"" + player.Name + "\"" + " \"Bad name detected.\"");
                        }
                        else if (stringDvar("badnameaction") == "tmpban")
                        {
                            Utilities.ExecuteCommand("kick " + "\"" + player.Name + "\"" + " \"Bad name detected.\"");
                        }
                        else
                        {
                            Utilities.ExecuteCommand("drop " + "\"" + player.Name + "\"" + " \"Bad name detected.\"");
                        }
                    });
                }
            }
        }

        public void nameformatcheck(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                string name = player.Name;

                int charcounter = 0;
                foreach (char c in name)
                {
                    if (c != ' ')
                    {
                        charcounter++;
                    }
                    if (charcounter >= 3)
                    {
                        break;
                    }
                }
                if (charcounter <= 2)
                {
                    AfterDelay(750, () =>
                    {
                        Utilities.ExecuteCommand("drop " + "\"" + name + "\"" + " " + "\"Name must be at least 3 characters long (not including spaces)\"");
                    });
                }

                if (charcounter >= 3)
                {
                    int spaces = 0;
                    int other = 0;
                    foreach (char ch in name)
                    {
                        if (ch == ' ')
                        {
                            spaces++;
                        }
                        else
                        {
                            other++;
                        }
                    }

                    if (spaces > 0)
                    {
                        double det = other / spaces;

                        if (det < 2.0)
                        {
                            AfterDelay(750, () =>
                            {
                                Utilities.ExecuteCommand("drop " + "\"" + name + "\"" + " " + "\"Name contains too many spaces.\"");
                            });
                        }
                    }
                }

                try
                {
                    if (getRank(player) == "User")
                    {
                        foreach (Entity ent in Playerz)
                        {
                            if (ent != player)
                            {
                                if (0 <= ent.Name.IndexOf(player.Name, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    AfterDelay(750, () =>
                                    {
                                        Utilities.ExecuteCommand("drop " + "\"" + name + "\"" + " " + "\"Player in the server already your full name contained in their username.\"");
                                    });
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
            }

            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }
        public void swapAmmo(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            if (rpgbullet == true)
            {

                switch (ammo.ToLower())
                {
                    case ("rpg"):
                        {
                            Vector3 ori = Call<Vector3>("anglestoforward", player.Call<Vector3>("getplayerangles"));
                            Vector3 fly = new Vector3(ori.X * 1000000, ori.Y * 1000000, ori.Z * 1000000);
                            Call("magicbullet", "rpg_mp", player.Call<Vector3>("gettagorigin", "tag_weapon_right"), fly, player);
                        }
                        break;
                    case ("remotetank"):
                        {
                            Vector3 ori = Call<Vector3>("anglestoforward", player.Call<Vector3>("getplayerangles"));
                            Vector3 fly = new Vector3(ori.X * 1000000, ori.Y * 1000000, ori.Z * 1000000);
                            Call("magicbullet", "remote_tank_projectile_mp", player.Call<Vector3>("gettagorigin", "tag_weapon_right"), fly, player);
                        }
                        break;
                    case ("ims"):
                        {
                            Vector3 ori = Call<Vector3>("anglestoforward", player.Call<Vector3>("getplayerangles"));
                            Vector3 fly = new Vector3(ori.X * 1000000, ori.Y * 1000000, ori.Z * 1000000);
                            Call("magicbullet", "ims_projectile_mp", player.Call<Vector3>("gettagorigin", "tag_weapon_right"), fly, player);
                        }
                        break;
                    case ("sam"):
                        {
                            Vector3 ori = Call<Vector3>("anglestoforward", player.Call<Vector3>("getplayerangles"));
                            Vector3 fly = new Vector3(ori.X * 1000000, ori.Y * 1000000, ori.Z * 1000000);
                            Call("magicbullet", "sam_projectile_mp", player.Call<Vector3>("gettagorigin", "tag_weapon_right"), fly, player);
                        }
                        break;
                    case ("javelin"):
                        {
                            Vector3 ori = Call<Vector3>("anglestoforward", player.Call<Vector3>("getplayerangles"));
                            Vector3 fly = new Vector3(ori.X * 1000000, ori.Y * 1000000, ori.Z * 1000000);
                            Call("magicbullet", "uav_strike_projectile_mp", player.Call<Vector3>("gettagorigin", "tag_weapon_right"), fly, player);
                        }
                        break;
                    case ("m320"):
                        {
                            Vector3 ori = Call<Vector3>("anglestoforward", player.Call<Vector3>("getplayerangles"));
                            Vector3 fly = new Vector3(ori.X * 1000000, ori.Y * 1000000, ori.Z * 1000000);
                            Call("magicbullet", "m320_mp", player.Call<Vector3>("gettagorigin", "tag_weapon_right"), fly, player);
                        }
                        break;
                    case ("smaw"):
                        {
                            Vector3 ori = Call<Vector3>("anglestoforward", player.Call<Vector3>("getplayerangles"));
                            Vector3 fly = new Vector3(ori.X * 1000000, ori.Y * 1000000, ori.Z * 1000000);
                            Call("magicbullet", "iw5_smaw_mp", player.Call<Vector3>("gettagorigin", "tag_weapon_right"), fly, player);
                        }
                        break;
                    case ("stinger"):
                        {
                            Vector3 ori = Call<Vector3>("anglestoforward", player.Call<Vector3>("getplayerangles"));
                            Vector3 fly = new Vector3(ori.X * 1000000, ori.Y * 1000000, ori.Z * 1000000);
                            Call("magicbullet", "stinger_mp", player.Call<Vector3>("gettagorigin", "tag_weapon_right"), fly, player);
                        }
                        break;
                    case ("xm25"):
                        {
                            Vector3 ori = Call<Vector3>("anglestoforward", player.Call<Vector3>("getplayerangles"));
                            Vector3 fly = new Vector3(ori.X * 1000000, ori.Y * 1000000, ori.Z * 1000000);
                            Call("magicbullet", "xm25_mp", player.Call<Vector3>("gettagorigin", "tag_weapon_right"), fly, player);
                        }
                        break;
                    default:
                        try
                        {
                            Vector3 ori = Call<Vector3>("anglestoforward", player.Call<Vector3>("getplayerangles"));
                            Vector3 fly = new Vector3(ori.X * 1000000, ori.Y * 1000000, ori.Z * 1000000);
                            Call("magicbullet", ammo.ToLower(), player.Call<Vector3>("gettagorigin", "tag_weapon_right"), fly, player);
                        }
                        catch
                        {
                            swapAmmoBool("error");
                        }
                        break;
                }
            }

        }

        public bool Speed1(Entity player, double scale)
        {
            player.Call("setmovespeedscale", new Parameter((float)scale));
            return true;
        }

        public void swapAmmoBool(string ammo2)
        {
            StreamWriter erros = new StreamWriter(fs);
            if (ammo2.ToLower() == "off")
            {
                rpgbullet = false;
                Utilities.RawSayAll(bot + "^3Special ^5Bullets ^1Disabled!");
            }
            else if (ammo2.ToLower() == "error")
            {
                rpgbullet = false;
                Utilities.RawSayAll(bot + "^1Special bullets disabled due to an error. ^7(Make sure you spelled your gun correctly.)");
            }
            else
            {
                rpgbullet = true;
                ammo = ammo2;
                Utilities.RawSayAll(bot + "^3Special ^5Bullets ^2Enabled^7: " + ammo2);
            }
        }

        public void fillAmmo()
        {
            foreach (Entity player in Playerz)
            {
                player.Call("setweaponammostock", player.CurrentWeapon, 999);
                player.Call("setweaponammoclip", player.CurrentWeapon, 999, "left");
                player.Call("setweaponammoclip", player.CurrentWeapon, 999, "right");
            }
            Utilities.RawSayAll(bot + "^2All current weapon ammo filled!");
        }

        public void whoIs(Entity caller, string target)
        {
            //bool located = false;
            //string known = "^1Known names for ^3";
            string known = "";
            string alias = "";

            string tempstr = "";

            Entity temp = FindByName(target);
            if (temp == null)
            {
                foreach (string s in aliasplayers)
                {
                    if (s.ToLower().Contains(target))
                    {
                        tempstr = s;
                    }
                }
            }

            if (temp != null)
            {
                tempstr = temp.Name;
                foreach (string s1 in aliasplayers)
                {
                    if (s1.StartsWith(tempstr + "="))
                    {
                        tempstr = s1;
                    }
                }
            }

            if (tempstr != "")
            {
                string[] checkfor = tempstr.Split('=');
                try
                {
                    string tc = checkfor[1];
                    tempstr = checkfor[0];

                    known = "^1Known names for ^3" + tempstr + "^7: ";
                    alias = "^1Current chat alias for ^3" + tempstr + "^7: " + tc;

                    Entity tempA = FindByName(tempstr);
                    if (tempA != null)
                    {
                        string finaldick = findPrevNames(tempA.GUID.ToString());
                        finaldick = finaldick.Replace(",", ", ");
                        known += finaldick;

                        Utilities.RawSayTo(caller, pm + known);
                        Utilities.RawSayTo(caller, pm + alias);
                    }
                    else
                    {
                        Utilities.RawSayTo(caller, pm + "^1Unable to locate player or alias.");
                    }
                }
                catch
                {
                    known = "^1Known names for ^3" + tempstr + "^7: ";
                    alias = "^3" + tempstr + "^7 does not currently have a chat alias.";

                    Entity tempB = FindByName(tempstr);
                    if (tempB != null)
                    {
                        string finaldick2 = findPrevNames(tempB.GUID.ToString());
                        finaldick2 = finaldick2.Replace(",", ", ");
                        known += finaldick2;

                        Utilities.RawSayTo(caller, pm + known);
                        Utilities.RawSayTo(caller, pm + alias);
                    }
                    else
                    {
                        Utilities.RawSayTo(caller, pm + "^1Unable to locate player or alias.");
                    }
                }
            }
            else
            {
                Utilities.RawSayTo(caller, pm + "^1Unable to locate player or alias.");
            }

        }



        public string findPrevNames(string guidA)
        {
            StreamWriter erros = new StreamWriter(fs);
            string[] lines = File.ReadAllLines(nametrack);

            string toreturn = " ";

            foreach (string s in lines)
            {
                if (s.StartsWith(guidA + ";"))
                {
                    string[] temp = s.Split(';');
                    try
                    {
                        toreturn = temp[1];
                    }
                    catch (Exception error)
                    { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                    break;
                }
            }
            return toreturn;
        }

        public void forgive(Entity player, string killer)
        {
            if (dvarCheck("Forgive"))
            {
                player.SetField("killer", "None");
                Entity attacker = FindByName(killer);

                int currentForg = attacker.GetField<int>("forgive");
                attacker.SetField("forgive", currentForg - 100);
                Utilities.SayTo(player, "^5Forgave " + attacker.Name);
                Utilities.SayTo(attacker, "^5Forgiven by ^1" + player.Name);
            }

        }

        public void trackPlayerz(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            //string path nametrack
            if (!File.Exists(nametrack))
            {
                //File.WriteAllBytes(nametrack, new byte[0]);
                StreamWriter sw = new StreamWriter(nametrack, true);
                sw.WriteLine("");
                sw.Close();
            }
            string[] initlines = File.ReadAllLines(nametrack);

            bool lemonade = false;
            string nuggets = player.GUID.ToString();

            string turkey = "";

            foreach (string sauce in initlines)
            {
                if (sauce.StartsWith(nuggets + ";"))
                {
                    turkey = nuggets;
                    lemonade = true;
                    break;
                }
            }

            if (lemonade == true)
            {
                int titties = 0;
                foreach (string kitty in initlines)
                {
                    if (kitty.StartsWith(nuggets + ";"))
                    {
                        try
                        {
                            string[] sock1 = kitty.Split(';');
                            string[] sock2 = sock1[1].Split(',');
                            if (!sock2.Contains<string>(player.Name))
                            {
                                initlines[titties] = kitty + "," + player.Name;
                            }
                        }
                        catch
                        {
                            initlines[titties] = "";
                            lemonade = false;
                        }
                        File.WriteAllLines(nametrack, initlines);
                        break;
                    }
                    titties++;
                }
            }

            if (lemonade == false)
            {
                StreamWriter sunnydays = new StreamWriter(nametrack, true);
                sunnydays.WriteLine(nuggets + ";" + player.Name);
                sunnydays.Close();
            }
        }

        public void setweapon(Entity caller, string target, string weapon)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                if (!weapon.ToLower().Contains("defaultkickmessage"))
                {
                    bool given = false;

                    weapon = weapon.ToLower();
                    string togive = "iw5_" + weapon + "_mp";
                    Entity player = FindByName(target);

                    string originalweapon = player.CurrentWeapon;

                    if (player != null)
                    {
                        player.TakeAllWeapons();
                        player.GiveWeapon(togive);
                        player.SwitchToWeaponImmediate(togive);

                        AfterDelay(649, () =>
                        {
                            if (player.CurrentWeapon == "none")
                            {
                                togive = weapon + "_mp";
                                player.TakeAllWeapons();
                                player.GiveWeapon(togive);
                                player.SwitchToWeaponImmediate(togive);
                            }

                            AfterDelay(651, () =>
                            {
                                if (player.CurrentWeapon == "none")
                                {
                                    player.TakeAllWeapons();
                                    player.GiveWeapon(originalweapon);
                                    player.SwitchToWeaponImmediate(originalweapon);
                                }
                                else
                                {
                                    given = true;
                                }

                                if (given == true)
                                {
                                    if (caller.Name != player.Name)
                                    {
                                        Utilities.RawSayTo(caller, pm + "^2Weapon given to^7: " + player.Name + ": " + weapon);
                                    }
                                    Utilities.RawSayTo(player, pm + "^2Weapon given^7: " + weapon);
                                }
                                else
                                {
                                    Utilities.RawSayTo(caller, pm + "^1Unable to locate weapon, original weapon given.");
                                }
                            });
                        });

                    }
                    else
                    {
                        Utilities.RawSayTo(caller, pm + "^1Unable to locate player.");
                    }
                }
                else
                {
                    Utilities.RawSayTo(caller, pm + "^1Please enter a weapon name.");
                }
            }
            catch
            {
                Utilities.RawSayTo(caller, pm + "^1Unknown error.");
            }
        }

        public void giveAll(Entity caller, string weapon)
        {
            StreamWriter erros = new StreamWriter(fs);
            foreach (Entity player in Playerz)
            {
                setWeaponAll(caller, player, "iw5_" + weapon + "_mp");
            }
            Utilities.RawSayAll(bot + "^3" + weapon + "^7 given to all players!");
        }


        public void setWeaponAll(Entity caller, Entity player, string weapon)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                bool given = false;

                weapon = weapon.ToLower();
                string togive = "iw5_" + weapon + "_mp";

                string originalweapon = player.CurrentWeapon;

                if (player != null)
                {
                    player.TakeAllWeapons();
                    player.GiveWeapon(togive);
                    player.SwitchToWeaponImmediate(togive);

                    AfterDelay(649, () =>
                    {
                        if (player.CurrentWeapon == "none")
                        {
                            togive = weapon + "_mp";
                            player.TakeAllWeapons();
                            player.GiveWeapon(togive);
                            player.SwitchToWeaponImmediate(togive);
                        }

                        AfterDelay(651, () =>
                        {
                            if (player.CurrentWeapon == "none")
                            {
                                player.TakeAllWeapons();
                                player.GiveWeapon(originalweapon);
                                player.SwitchToWeaponImmediate(originalweapon);
                            }
                            else
                            {
                                given = true;
                            }

                            if (given == true)
                            {
                                if (caller.Name != player.Name)
                                {
                                    //Utilities.RawSayTo(caller, pm + "^2Weapon given to^7: " + player.Name + ": " + weapon);
                                }
                                Utilities.RawSayTo(player, pm + "^2Weapon given^7: " + weapon);
                            }
                            else
                            {
                                //Utilities.RawSayTo(caller, pm + "^1Unable to locate weapon, original weapon given.");
                            }
                        });
                    });

                }
                else
                {
                    //Utilities.RawSayTo(caller, pm + "^1Unable to locate player.");
                }
            }
            catch
            {
                Utilities.RawSayTo(caller, pm + "^1Unknown error.");
            }
        }

        public void toggleOption(Entity caller, string option, string bl)
        {
            if (option.ToLower() != "blockoverlordadding")
            {
                StreamWriter erros = new StreamWriter(fs);
                try
                {
                    int tribool = 2;
                    bl = bl.ToLower();

                    switch (bl)
                    {
                        case ("on"):
                            tribool = 1;
                            break;
                        case ("1"):
                            tribool = 1;
                            break;
                        case ("yes"):
                            tribool = 1;
                            break;
                        case ("off"):
                            tribool = 0;
                            break;
                        case ("0"):
                            tribool = 0;
                            break;
                        case ("no"):
                            tribool = 0;
                            break;
                    }


                    bool found = false;

                    string[] lines = File.ReadAllLines(@"scripts\\sinadmin\\" + cfgname+ "\\sinscript.cfg");

                    int counter = -1;

                    if (tribool == 1 || tribool == 0)
                    {
                        foreach (string s in lines)
                        {
                            counter++;
                            if (s.ToLower().StartsWith("[" + option.ToLower() + "]" + ";:;"))
                            {
                                found = true;

                                string[] evileyes = s.Split(new string[] { ";:;" }, StringSplitOptions.None);
                                string SANTASCOMING = evileyes[1];
                                string elfears = evileyes[0];

                                if (SANTASCOMING == "1" || SANTASCOMING == "0")
                                {
                                    lines[counter] = elfears + ";:;" + tribool.ToString();
                                    Call("setdvar", elfears, tribool.ToString());
                                    tribool = -1;
                                }
                                else
                                {
                                    tribool = 4;
                                }

                                break;
                            }
                        }
                        setDvars();
                    }
                    else
                    {
                        Utilities.RawSayTo(caller, pm + "^1Unknown bool. Please enter either ^7on ^1or ^7off ^1to toggle options.");
                        found = true;
                    }

                    if (found == false)
                    {
                        Utilities.RawSayTo(caller, pm + "^1Unable to locate config option to edit");
                    }

                    if (tribool == 4)
                    {
                        // Aint tri anymore, eh?
                        // I break all the rules!!!

                        Utilities.RawSayTo(caller, pm + "^1Not a valid bool option to edit.");
                    }

                    // THE ULTIMATE PENTA-BOOL
                    if (tribool == -1)
                    {
                        File.WriteAllLines(@"scripts\\sinadmin\\" + cfgname+ "\\sinscript.cfg", lines);

                        Utilities.RawSayTo(caller, pm + "^3Following option written to config:");
                        Utilities.RawSayTo(caller, "^5" + lines[counter]);
                        setDvars();
                    }
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
            }
            else
            {
                Utilities.RawSayTo(caller, pm + "^1You are not allowed to edit that option");
            }
        }

        public void mute(Entity issuer, string target, string action)
        {
            StreamWriter erros = new StreamWriter(fs);
            Entity ent = FindByName(target);
            if (ent != null)
            {
                if (action == "mute")
                {
                    var removewhites = File.ReadAllLines(mutepath).Where(arg => !string.IsNullOrWhiteSpace(arg));
                    File.WriteAllLines(mutepath, removewhites);
                    StreamWriter sw = new StreamWriter(mutepath, true);
                    sw.WriteLine("");
                    sw.WriteLine(ent.GUID.ToString());
                    sw.Close();
                    mutedplayers.Add(ent.GUID);
                    Utilities.RawSayAll(bot + ent.Name + "^2 is muted.");
                }
                else if (action == "unmute")
                {
                    string[] lines = File.ReadAllLines(mutepath);
                    int counter = 0;
                    foreach (string s in lines)
                    {
                        if (s == ent.GUID.ToString())
                        {
                            lines[counter] = "";
                            File.WriteAllLines(mutepath, lines);
                        }
                        counter++;
                    }
                    mutedplayers.Remove(ent.GUID);
                    Utilities.RawSayAll(bot + ent.Name + "^2 is unmuted.");
                }
            }
        }

        public void walkingAC130(Entity ent, string target)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                usingaimbot = true;

                Entity player = FindByName(target);
                if (player != null && target.ToLower() != "all")
                {
                    Utilities.RawSayTo(player, pm + "^2Walking AC130 ^3Enabled.");
                    if (ent != player)
                    {
                        Utilities.RawSayTo(ent, pm + "^2Walking AC130 ^3Enabled: " + player.Name);
                    }
                    player.TakeAllWeapons();
                    player.GiveWeapon("ac130_25mm_mp");
                    player.SwitchToWeaponImmediate("ac130_25mm_mp");
                    player.Call("disableweaponpickup");
                    int umc = 0;
                    OnInterval(100, () =>
                    {
                        player.Call("setweaponammoclip", player.CurrentWeapon, 10);
                        player.Call("setweaponammoclip", player.CurrentWeapon, 10, "left");
                        player.Call("setweaponammoclip", player.CurrentWeapon, 10, "right");
                        if (player.CurrentWeapon != "ac130_25mm_mp" && umc < 20)
                        {
                            umc++;
                            return true;
                        }
                        else if (umc >= 20)
                        {
                            return false;
                        }
                        else
                        {
                            umc = 0;
                            return true;
                        }
                    });
                }
                else if (target.ToLower() == "all")
                {
                    foreach (Entity player1 in Playerz)
                    {
                        player1.TakeAllWeapons();
                        player1.GiveWeapon("ac130_25mm_mp");
                        player1.SwitchToWeaponImmediate("ac130_25mm_mp");
                        player1.Call("disableweaponpickup");
                        int umc = 0;
                        OnInterval(100, () =>
                        {
                            player1.Call("setweaponammoclip", player1.CurrentWeapon, 10);
                            player1.Call("setweaponammoclip", player1.CurrentWeapon, 10, "left");
                            player1.Call("setweaponammoclip", player1.CurrentWeapon, 10, "right");
                            if (player1.CurrentWeapon != "ac130_25mm_mp" && umc < 20)
                            {
                                umc++;
                                return true;
                            }
                            else if (umc >= 20)
                            {
                                return false;
                            }
                            else
                            {
                                umc = 0;
                                return true;
                            }
                        });
                    }
                    Utilities.RawSayAll(bot + "^2Walking AC130 ^3enabled for ^1all players!");
                }
                else
                {
                    Utilities.RawSayTo(ent, pm + "^1Unable to locate target.");
                }
            }
            catch
            {
                Utilities.RawSayTo(ent, pm + "^1Error loading ^4walking AC130.");
            }
        }

        public void showip(Entity player)
        {
            Utilities.RawSayTo(player, pm + "^3Your IP Is^7: " + player.IP);
        }

        public void showguid(Entity player)
        {
            Utilities.RawSayTo(player, pm + "^3Your GUID Is^7: " + player.GUID);
        }

        public void unlimitedammo(Entity player)
        {
            player.Call("setweaponammoclip", player.CurrentWeapon, 99);
            player.Call("setweaponammoclip", player.CurrentWeapon, 99, "left");
            player.Call("setweaponammoclip", player.CurrentWeapon, 99, "right");
        }

        public void ammoThang()
        {
            StreamWriter erros = new StreamWriter(fs);
            OnInterval(333, () =>
            {
                if (dvarCheck("unlimitedammo") == true)
                {
                    foreach (Entity e in Playerz)
                    {
                        e.Call("setweaponammoclip", e.CurrentWeapon, 99);
                        e.Call("setweaponammoclip", e.CurrentWeapon, 99, "left");
                        e.Call("setweaponammoclip", e.CurrentWeapon, 99, "right");
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }

        public void unlimitedammobool(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            if (dvarCheck("unlimitedammo") == true)
            {
                string path = @"scripts\\" + "SinAdmin\\" + cfgname + "\\SinScript.cfg";
                int counter = 0;
                string[] alines = File.ReadAllLines(path);
                foreach (string s in alines)
                {
                    if (s.ToLower().StartsWith("[unlimitedammo];:;"))
                    {
                        alines[counter] = "[UnlimitedAmmo];:;0";
                        File.WriteAllLines(path, alines);
                        break;
                    }
                    counter++;
                }
                Utilities.RawSayAll(bot + "^1Unlimited Ammo Disabled.");
                Call("setdvar", "unlimitedammo", "0");
            }
            else if (dvarCheck("unlimitedammo") == false)
            {
                string path = @"scripts\\" + "SinAdmin\\" + cfgname + "\\SinScript.cfg";
                int counter = 0;
                string[] alines = File.ReadAllLines(path);
                foreach (string s in alines)
                {
                    if (s.ToLower().StartsWith("[unlimitedammo];:;"))
                    {
                        alines[counter] = "[UnlimitedAmmo];:;1";
                        File.WriteAllLines(path, alines);
                        break;
                    }
                    counter++;
                }
                Utilities.RawSayAll(bot + "^2Unlimited Ammo Enabled.");
                Call("setdvar", "unlimitedammo", "1");
            }
            ammoThang();
        }

        public void badname(Entity player, string name, string action)
        {
            StreamWriter erros = new StreamWriter(fs);
            if (!dvarCheck("antilag"))
            {
                try
                {
                    string namespath = @"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\badnames.txt";
                    if (!File.Exists(namespath))
                    {
                        //File.WriteAllBytes(namespath, new byte[0]);
                        StreamWriter sw = new StreamWriter(bs);
                        sw.WriteLine("");
                        sw.Close();
                    }

                    name = name.Replace(" DefaultKickMessage", "");

                    if (action.ToLower() == "add")
                    {
                        StreamWriter sw = new StreamWriter(namespath, true);
                        sw.WriteLine(name);
                        sw.Close();
                        Utilities.RawSayAll(bot + "^5Bad name added^7: " + name);
                    }
                    else if (action.ToLower() == "remove")
                    {
                        int counter = 0;
                        string[] lines = File.ReadAllLines(namespath);
                        bool found = false;
                        foreach (string s in lines)
                        {
                            if (s.ToLower() == name.ToLower() && s != "")
                            {
                                lines[counter] = "";
                                File.WriteAllLines(namespath, lines);
                                found = true;
                            }
                            counter++;
                        }
                        if (found == true)
                        {
                            Utilities.RawSayAll(bot + "^5Bad name removed^7: " + name);
                        }
                        else
                        {
                            Utilities.RawSayTo(player, pm + "^5Unable to locate name to remove^7: " + name);
                        }
                    }
                    else if (action.ToLower() == "addarray")
                    {
                        string tell = "";

                        string[] toadds = name.Split(' ');
                        StreamWriter sw = new StreamWriter(namespath, true);
                        foreach (string s in toadds)
                        {
                            sw.WriteLine(s);
                            tell += s + ", ";
                        }
                        sw.Close();
                        if (tell.EndsWith(", "))
                        {
                            tell = tell.Remove(tell.Length - 2);
                        }
                        Utilities.RawSayAll(bot + "^5Bad name(s) added^7: " + tell);
                    }
                    else if (action.ToLower() == "removearray")
                    {
                        string[] removes = name.Split(' ');
                        string tell = "";
                        int counter = 0;

                        string[] lines = File.ReadAllLines(namespath);

                        foreach (string s in removes)
                        {
                            counter = 0;
                            foreach (string str in lines)
                            {
                                if (str.ToLower() == s.ToLower())
                                {
                                    lines[counter] = "";
                                    tell += str + ", ";
                                }

                                counter++;
                            }
                        }
                        File.WriteAllLines(namespath, lines);
                        Utilities.RawSayAll(bot + "^5Bad name(s) removed^7: " + tell);
                    }
                    var removewhites = File.ReadAllLines(namespath).Where(arg => !string.IsNullOrWhiteSpace(arg));
                    File.WriteAllLines(namespath, removewhites);
                    StreamWriter fackdis = new StreamWriter(namespath, true);
                    fackdis.WriteLine(" ");
                    fackdis.Close();

                    loadbadnames();
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
            }
        }

        public void loadbadnames()
        {
            if (!dvarCheck("antilag"))
            {
                // string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\badnames.txt";
                //string[] totaladd = File.ReadAllLines(path);
                StreamReader roguesaderp = new StreamReader(bs);
                string temp;
                while ((temp = roguesaderp.ReadLine()) != null)
                {
                    if (temp != "" && temp != " " && !badnames.Contains(temp))
                    {
                        badnames.Add(temp);
                    }
                }
            }
        }

        public void aBal(string bl)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\SinScript.cfg";
                int counter = 0;
                if (bl.ToLower() == "on")
                {
                    string[] lines = File.ReadAllLines(path);
                    foreach (string s in lines)
                    {
                        if (s.ToLower().StartsWith("[AutoBalance]"))
                        {
                            lines[counter] = "[AutoBalance];:;1";
                            File.WriteAllLines(path, lines);
                        }
                        counter++;
                    }
                    Call("setdvar", "AutoBalance", "1");
                    Utilities.RawSayAll(bot + "^AutoBalance Enabled.");
                }
                else if (bl.ToLower() == "off")
                {
                    string[] lines = File.ReadAllLines(path);
                    foreach (string s in lines)
                    {
                        if (s.ToLower().StartsWith("[AutoBalance]"))
                        {
                            lines[counter] = "[AutoBalance];:;0";
                            File.WriteAllLines(path, lines);
                        }
                        counter++;
                    }
                    Call("setdvar", "AutoBalance", "0");
                    Utilities.RawSayAll(bot + "^5AutoBalance Disabled.");
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public bool isMuted(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            if (mutedplayers.Contains(player.GUID))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void knife(Entity player, string bl)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\SinScript.cfg";
                int counter = 0;
                if (bl.ToLower() == "off")
                {
                    string[] lines = File.ReadAllLines(path);
                    foreach (string s in lines)
                    {
                        if (s.ToLower().StartsWith("[knife]"))
                        {
                            NoKnife knifea = new NoKnife();
                            knifea.DisableKnife();
                            lines[counter] = "[Knife];:;1";
                            File.WriteAllLines(path, lines);
                        }
                        counter++;
                    }

                    Call("setdvar", "knife", "1");
                    Utilities.RawSayAll(bot + "^2Knifing Disabled.");
                }
                else if (bl.ToLower() == "on")
                {
                    string[] lines = File.ReadAllLines(path);
                    foreach (string s in lines)
                    {
                        if (s.ToLower().StartsWith("[knife]"))
                        {
                            NoKnife knifea = new NoKnife();
                            knifea.EnableKnife();
                            lines[counter] = "[Knife];:;0";
                            File.WriteAllLines(path, lines);
                        }
                        counter++;
                    }
                    Call("setdvar", "knife", "0");
                    Utilities.RawSayAll(bot + "^2Knifing Enabled.");
                }
                else
                {
                    Utilities.RawSayTo(player, pm + "^1Unknown bool.");
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public string secret = "Rogue is gay, shh";
        public void hm(string bl)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\SinScript.cfg";
                int counter = 0;
                if (bl.ToLower() == "on")
                {
                    string[] lines = File.ReadAllLines(path);
                    foreach (string s in lines)
                    {
                        if (s.ToLower().StartsWith("[hitmarkergm]"))
                        {
                            lines[counter] = "[hitmarkergm];:;1";
                            File.WriteAllLines(path, lines);
                        }
                        counter++;
                    }
                    Call("setdvar", "hitmarkergm", "1");
                    Utilities.RawSayAll(bot + "^5HitMakerGM Enabled.");
                }
                else if (bl.ToLower() == "off")
                {
                    string[] lines = File.ReadAllLines(path);
                    foreach (string s in lines)
                    {
                        if (s.ToLower().StartsWith("[hitmarkergm]"))
                        {
                            lines[counter] = "[hitmarkergm];:;0";
                            File.WriteAllLines(path, lines);
                        }
                        counter++;
                    }
                    Call("setdvar", "hitmarkergm", "0");
                    Utilities.RawSayAll(bot + "^5HitMakerGM Disabled.");
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void alias(Entity player, string target, string alias, string action)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                if (alias == "DefaultKickMessage")
                {
                    alias = "TeknoSlave";
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

            if (!dvarCheck("antilag"))
            {
                string path = "scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\chataliases.txt";
                if (!File.Exists(path))
                {
                    //File.WriteAllBytes(path, new byte[0]);
                    StreamWriter sw = new StreamWriter(path, true);
                    sw.WriteLine("");
                    sw.Close();
                }
                string[] lines = File.ReadAllLines(path);
                switch (action.ToLower())
                {
                    case ("set"):
                        {
                            Entity targetent = FindByName(target);
                            if (targetent != null)
                            {
                                int counter = 0;
                                foreach (string s in lines)
                                {
                                    if (s.StartsWith(targetent.Name + "="))
                                    {
                                        lines[counter] = "";
                                        File.WriteAllLines(path, lines);
                                    }
                                    counter++;
                                }

                                var removewhites = File.ReadAllLines(path).Where(arg => !string.IsNullOrWhiteSpace(arg));
                                File.WriteAllLines(path, removewhites);

                                StreamWriter sw = new StreamWriter(path, true);
                                sw.WriteLine("");
                                sw.WriteLine(targetent.Name + "=" + alias);
                                sw.Close();
                                Utilities.RawSayAll(bot + "^2Alias added for: " + targetent.Name + " ^2(^7" + alias + "^2)");
                            }
                            else
                            {
                                Utilities.RawSayTo(player, pm + "^1Unable to locate player to add alias.");
                            }

                            string[] aliaslines = File.ReadAllLines(@"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\chataliases.txt");
                            try { aliasplayers.Clear(); }
                            catch (Exception error)
                            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                            foreach (string alstr in aliaslines)
                            {
                                try
                                {
                                    if (!aliasplayers.Contains(alstr))
                                    {
                                        aliasplayers.Add(alstr);
                                    }
                                    string[] stringtemp = alstr.Split('=');
                                    if (!aliasplayersv2.Contains(stringtemp[0]))
                                    {
                                        aliasplayersv2.Add(stringtemp[0]);
                                    }
                                }
                                catch (Exception error)
                                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                            }

                            break;
                        }
                    case ("remove"):
                        {
                            Entity targetent = FindByName(target);
                            if (targetent != null)
                            {
                                int counter = 0;
                                foreach (string s in lines)
                                {
                                    if (s.StartsWith(targetent.Name + "="))
                                    {
                                        lines[counter] = "";
                                        File.WriteAllLines(path, lines);
                                    }
                                    counter++;
                                }
                                Utilities.RawSayAll(bot + "^2Alias removed from: " + targetent.Name);
                            }
                            else
                            {
                                Utilities.RawSayTo(player, pm + "^1Unable to locate player to remove alias.");
                            }
                            break;
                        }
                }
            }
        }
        public void kill(string target)
        {
            StreamWriter erros = new StreamWriter(fs);
            Random rand = new Random();
            int temp = 0;
            if (target.ToLower() == "minefield")
            {
                int xtemp;
                int ytemp;
                foreach (Entity living in Playerz)
                {
                    if (living.IsAlive)
                    {
                        for (int x = 0; x <= 2; x++)
                        {
                            xtemp = rand.Next(-600, 601);
                            ytemp = rand.Next(-600, 601);
                            var origin = living.Origin;
                            origin.Z += 65;
                            origin.X += xtemp;
                            origin.Y += ytemp;

                            Call("magicbullet", "remotemissile_projectile_mp", origin, living);
                        }
                    }
                }
            }
            else if (target.ToLower() == "all")
            {
                foreach (Entity kill in Playerz)
                {
                    if (kill.IsAlive)
                    {
                        temp = rand.Next(1, 5);
                        var dest0 = kill.Origin;
                        var dest1 = kill.Origin;
                        dest0.Z -= 200;
                        dest1.Z += 200;

                        switch (temp)
                        {
                            case (1):
                                Call("magicbullet", "ims_projectile_mp", dest1, dest0, kill);
                                break;
                            case (2):
                                Call("magicbullet", "remote_tank_projectile_mp", dest1, dest0, kill);
                                break;
                            case (3):
                                Call("magicbullet", "sam_projectile_mp", dest1, dest0, kill);
                                break;
                            case (4):
                                Call("magicbullet", "uav_strike_projectile_mp", dest1, dest0, kill);
                                break;
                        }
                    }
                }
            }
            else
            {
                Entity tokill = FindByName(target);
                if (tokill != null && tokill.IsAlive)
                {
                    var dest0 = tokill.Origin;
                    var dest1 = tokill.Origin;
                    dest0.Z -= 1000;
                    dest1.Z += 1000;

                    Call("magicbullet", "uav_strike_projectile_mp", dest1, dest0, tokill);
                }
            }

        }

        public void setClientDvar(Entity player, string dvar, string value)
        {
            player.SetClientDvar(dvar, value);
            Utilities.RawSayTo(player, pm + "^5Dvar: ^1" + dvar + " ^0 has been set to: ^1" + value);
        }

        public void setDvar(Entity player, string dvar, string value)
        {
            Call("setdvar", dvar, value);
            Utilities.RawSayTo(player, pm + "^5Dvar: ^1" + dvar + " ^0 has been set to: ^1" + value);
        }

        public void unfreezee(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            player.Call("freezecontrols", false);
        }

        public void TeamChange(Entity PENT, string player)
        {
            Entity ent = FindByName(player);
            try
            {
                if (ent != null)
                {
                    string field = ent.GetField<string>("sessionteam");

                    switch (field)
                    {
                        case ("allies"):
                            {
                                ent.SetField("team", "axis");
                                ent.SetField("sessionteam", "axis");
                                ent.Notify("menuresponse", new Parameter[]
			                    {
				                    "team_marinesopfor",
				                    "axis"
		            	        });
                                Utilities.RawSayAll(bot + "^3Team has been changed for ^7" + ent.Name + ": ^1Axis");
                                break;
                            }
                        case ("axis"):
                            {
                                ent.SetField("team", "allies");
                                ent.SetField("sessionteam", "allies");
                                ent.Notify("menuresponse", new Parameter[]
			                    {
				                    "team_marinesopfor",
				                    "allies"
		                    	});
                                Utilities.RawSayAll(bot + "^3Team has been changed for ^7" + ent.Name + ": ^4Allies");
                                break;
                            }
                        case ("spectator"):
                            {
                                ent.SetField("team", "axis");
                                ent.SetField("sessionteam", "axis");
                                ent.Notify("menuresponse", new Parameter[]
			                    {
				                    "team_marinesopfor",
				                    "axis"
		                    	});
                                Utilities.RawSayAll(bot + "^3Team has been changed for ^7" + ent.Name + ": ^1Axis");
                                break;
                            }
                    }
                }
                else
                {
                    Utilities.RawSayTo(ent, pm + "^1SinScript was unable to switch teams of desired player.");
                }
            }
            catch (Exception ex)
            {
                Utilities.RawSayTo(ent, pm + "^1Unable to switch teams. Error has been written to console.");
                Log.Write(LogLevel.All, ex.ToString());
            }
        }

        public void balance()
        {
            bool isSet = false;
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                int axis = 0;
                int allies = 0;
                int high = 0;
                int low = 0;

                string team = "";

                if (!isSet)
                {
                    OnInterval(100, () =>
                    {
                        isSet = true;
                        axis = 0;
                        allies = 0;
                        high = 0;
                        low = 0;

                        foreach (Entity e in Playerz)
                        {
                            team = e.GetField<string>("sessionteam");
                            switch (team)
                            {
                                case ("axis"):
                                    axis++;
                                    break;
                                case ("allies"):
                                    allies++;
                                    break;
                            }
                        }

                        high = Math.Max(allies, axis);
                        low = Math.Min(allies, axis);
                        int lowset = low + 1;

                        if (high > lowset)
                        {
                            if (high == axis)
                            {
                                TeamChange(getAxis(), getAxis().Name);
                            }
                            else if (high == allies)
                            {
                                TeamChange(getAllies(), getAllies().Name);
                            }
                            return true;
                        }
                        else
                        {
                            Utilities.RawSayAll(bot + "^1Teams Balanced.");
                            return false;
                        }
                    });
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void deadbalance()
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                bool isSet = false;
                int axis = 0;
                int allies = 0;
                int high = 0;
                int low = 0;

                string team = "";

                if (!isSet)
                {
                    OnInterval(100, () =>
                    {
                        isSet = true;
                        axis = 0;
                        allies = 0;
                        high = 0;
                        low = 0;

                        foreach (Entity e in Playerz)
                        {
                            team = e.GetField<string>("sessionteam");
                            switch (team)
                            {
                                case ("axis"):
                                    axis++;
                                    break;
                                case ("allies"):
                                    allies++;
                                    break;
                            }
                        }

                        high = Math.Max(allies, axis);
                        low = Math.Min(allies, axis);
                        int lowset = low + 1;

                        if (high > lowset)
                        {
                            if (high == axis)
                            {
                                if (getDeadAxis() != null)
                                {
                                    // if(getDeadAxis().IsAlive)
                                    //{
                                    TeamChange(getDeadAxis(), getDeadAxis().Name);
                                    // }
                                }
                                else
                                {
                                    Utilities.RawSayAll(bot + "^1Unable to completely deadbalance teams because not enough players are dead.");
                                    return false;
                                }
                            }
                            else if (high == allies)
                            {
                                if (getDeadAllies() != null)
                                {
                                    //if(getDeadAllies.IsAlive)
                                    //{
                                    TeamChange(getAllies(), getAllies().Name);
                                    //}
                                }
                                else
                                {
                                    Utilities.RawSayAll(bot + "^1Unable to completely deadbalance teams because not enough players are dead.");
                                    return false;
                                }
                            }
                            return true;
                        }
                        else
                        {
                            Utilities.RawSayAll(bot + "^4Dead players balanced.");
                            return false;
                        }
                    });
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public Entity getAxis()
        {
            StreamWriter erros = new StreamWriter(fs);
            Entity toRet = null;

            foreach (Entity player in Playerz)
            {
                if (player.GetField<string>("sessionteam") == "axis")
                {
                    toRet = player;
                }
            }

            return toRet;
        }

        public Entity getAllies()
        {
            StreamWriter erros = new StreamWriter(fs);
            Entity toRet = null;

            foreach (Entity player in Playerz)
            {
                if (player.GetField<string>("sessionteam") == "allies")
                {
                    toRet = player;
                }
            }

            return toRet;
        }

        public Entity getDeadAxis()
        {
            StreamWriter erros = new StreamWriter(fs);
            Entity toRet = null;

            foreach (Entity player in Playerz)
            {
                if (player.GetField<string>("sessionteam") == "axis" && !player.IsAlive)
                {
                    toRet = player;
                }
            }

            return toRet;
        }

        public Entity getDeadAllies()
        {
            StreamWriter erros = new StreamWriter(fs);
            Entity toRet = null;

            foreach (Entity player in Playerz)
            {
                if (player.GetField<string>("sessionteam") == "allies" && !player.IsAlive)
                {
                    toRet = player;
                }
            }

            return toRet;
        }

        public Entity FindByName(string name)
        {
            int num = 0;
            Entity result = null;
            foreach (Entity ent in Playerz)
            {
                if (0 <= ent.Name.IndexOf(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    result = ent;
                    num++;
                }
            }
            if (num == 1)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public void afk(Entity player)
        {
            player.SetField("team", "spectator");
            player.SetField("sessionteam", "spectator");
            player.Notify("menuresponse", new Parameter[] { "team_marinesopfor", "spectator" });
        }

        public void pmuser(Entity from, string to, string message)
        {
            Entity toent = FindByName(to);
            if (to != null)
            {
                if (message != "DefaultKickMessage")
                {

                    Utilities.RawSayTo(toent, pm + "^1" + from.Name + " : ^2" + message);
                    Utilities.RawSayTo(from, pm + "PM Sent.");
                }
                else
                {
                    Utilities.RawSayTo(from, pm + "^1Please enter a message to send.");
                }
            }
            else
            {
                Utilities.RawSayTo(from, pm + "^1Either multiple users were found or SinScript was unable to locate user.");
            }
        }

        public void log(Entity player)
        {
            string date = DateTime.Now.ToString("M.d.yyyy");
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                if (dvarCheck("logplayers"))
                {
                    string ip = player.IP.ToString();
                    StreamWriter writer = new StreamWriter(@"scripts\\" + "SinAdmin\\" + cfgname+ "\\logs\\players." + date + ".log", true);
                    string str = string.Concat(new object[] { player.Name, " : ", ip, " ", player.GUID, " ", player.CurrentWeapon, " ", player.UserID });
                    writer.WriteLine(str);
                    writer.Close();
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void timedmessages()
        {
            StreamWriter erros = new StreamWriter(fs);
            if (!dvarCheck("antilag"))
            {
                try
                {
                    string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\TimedMessages.txt";

                    timeint = 60;

                    if (!File.Exists(path))
                    {
                        string[] newWrite = 
                {
                    "//Set timer interval in seconds",
                    "//Seperate each timed message by a new line",
                    "//SinScript will re-organize timed messages to prevent repeating beginning message each round",
                    "45",
                    "Timed Message 1",
                    "Timed Message 2",
                    "Timed Message 3",
                    "Timed Message 4",
                    "Timed Message 5",
                    "@admins",
                    "@rules",
                    "@status"
                };
                        File.WriteAllLines(path, newWrite);
                    }
                    string[] lines = File.ReadAllLines(path);
                    foreach (string s in lines)
                    {
                        try
                        {
                            timeint = Convert.ToInt32(s);
                            break;
                        }
                        catch
                        {

                        }
                    }


                    string path2 = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\TimedMessagesBACKUP.txt";
                    string[] temp = File.ReadAllLines(path);
                    string[] temp2 = File.ReadAllLines(path2);
                    bool checkb = Enumerable.SequenceEqual(temp, temp2);
                    if (checkb == false)
                    {
                        int bc = 0;
                        foreach (string st in temp)
                        {
                            bc++;
                        }
                        if (bc > 1)
                        {
                            File.Delete(path2);
                            File.Copy(path, path2);
                        }
                        else
                        {
                            File.Delete(path);
                            File.Copy(path2, path);
                        }
                    }

                    ShowMessage();
                }
                catch
                {
                    try
                    {
                        ShowMessage();
                    }
                    catch (Exception error)
                    { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                }
            }
        }

        //public void ShowMessage(object obj)
        public void ShowMessage()
        {
            StreamWriter erros = new StreamWriter(fs);
            if (!dvarCheck("antilag"))
            {
                try
                {
                    OnInterval(timeint * 1000, () =>
                    {
                        if (interval2 == true)
                        {
                            int test;
                            int counter = 0;

                            string tosay = "";

                            string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\TimedMessages.txt";

                            string[] lines = File.ReadAllLines(path);

                            foreach (string s in lines)
                            {
                                try
                                {
                                    test = Convert.ToInt32(s);
                                }
                                catch
                                {
                                    if (s != "")
                                    {
                                        if (!s.StartsWith("//"))
                                        {
                                            if (tosay == "")
                                            {
                                                lines[counter] = "";
                                                File.WriteAllLines(path, lines);

                                                tosay = s;

                                                StreamWriter sw = new StreamWriter(path, true);
                                                sw.WriteLine(s);
                                                sw.Close();
                                                break;

                                            }

                                        }
                                    }
                                }
                                counter++;
                            }
                            if (tosay.ToLower() != "@admins" && tosay.ToLower() != "@rules" && tosay.ToLower() != "@status" && tosay != "")
                            {
                                Utilities.RawSayAll(bot + tosay);
                            }
                            else if (tosay.ToLower() == "@admins")
                            {
                                try
                                {
                                    // Here maybe?
                                    // See below, sir
                                    // HOW ABOUT HERE?!?
                                    // populateadmins();
                                    // IM TAKING OUT THE FUCKING ADMIN COMMAND AND REWRITING THIS WORTHLESS PIECE OF SHIT
                                    // SEE BELOW, ASSHOLES
                                    // Fuck this and fuck you
                                    // I beat the system, I must be jesus...
                                    Entity player = null;
                                    listAdmins(player, "all");
                                }
                                catch (Exception error)
                                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                            }
                            else if (tosay.ToLower() == "@rules")
                            {
                                try
                                {
                                    _rules();
                                }
                                catch (Exception error)
                                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                            }
                            else if (tosay.ToLower() == "@status")
                            {
                                try
                                {
                                    _status();
                                }
                                catch (Exception error)
                                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                            }
                            var removewhites = File.ReadAllLines(path).Where(arg => !string.IsNullOrWhiteSpace(arg));
                            File.WriteAllLines(path, removewhites);

                            string[] checkblank = File.ReadAllLines(path);
                            int checkcount = -1;
                            foreach (string sblank in checkblank)
                            {
                                checkcount++;
                                if (checkcount > 0)
                                {
                                    break;
                                }
                            }
                            if (checkcount < 1)
                            {
                                try
                                {
                                    File.Delete(path);
                                    File.Copy(@"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\TimedMessagesBACKUP.txt", path);
                                }
                                catch (Exception error)
                                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                            }
                            return true;
                        }
                        else
                        {
                            interval2 = true;
                            return true;
                        }
                    });
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
            }
        }

        public void mode(Entity player, string mode)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                char[] separator = new char[]
			{
				' '
			};
                string[] array = mode.Split(separator);
                if (array[1] == "")
                {
                    Utilities.RawSayTo(player, pm + "^1enter dsr name");
                }
                if (array.Length > 1)
                {
                    string text = base.Call<string>("getdvar", new Parameter[]
			{
				"mapname"
			});
                    if (File.Exists("admin\\" + array[1] + ".dsr"))
                    {
                        File.Delete("admin\\SinX.dspl");
                        text = text.Replace("default:", "");
                        using (StreamWriter streamWriter5 = new StreamWriter("admin\\SinX.dspl", true))
                        {
                            streamWriter5.WriteLine(text + "," + array[1] + ",1000");
                        }

                        Utilities.ExecuteCommand("sv_maprotation SinX");
                        Utilities.RawSayAll(bot + "^5Changing Gametype ^0To ^1" + array[1]);
                        base.AfterDelay(2000, new Action(this.mpr));
                    }

                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void setGameType(Entity player, string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                char[] separator = new char[]
			{
				' '
			};
                string[] array = message.Split(separator);
                if (array[1] == "")
                {
                    Utilities.RawSayTo(player, pm + "^1type a map name");

                }
                if (array[2] == "")
                {
                    Utilities.RawSayTo(player, pm + "^1type a dsr name");

                }
                if (array.Length > 3)
                {
                    Utilities.RawSayTo(player, pm + "^1WTF? Just write mapname and dsrname!");

                }
                if (array.Length > 2)
                {
                    array[1] = array[1].Replace("dome", "mp_dome");
                    array[1] = array[1].Replace("resistance", "mp_paris");
                    array[1] = array[1].Replace("village", "mp_village");
                    array[1] = array[1].Replace("bootleg", "mp_bootleg");
                    array[1] = array[1].Replace("carbon", "mp_carbon");
                    array[1] = array[1].Replace("interchange", "mp_interchange");
                    array[1] = array[1].Replace("hardhat", "mp_hardhat");
                    array[1] = array[1].Replace("downturn", "mp_exchange");
                    array[1] = array[1].Replace("outpost", "mp_radar");
                    array[1] = array[1].Replace("gateway", "mp_hillside_ss");
                    array[1] = array[1].Replace("lookout", "mp_restrepo_ss");
                    array[1] = array[1].Replace("overwatch", "mp_overwatch");
                    array[1] = array[1].Replace("fallen", "mp_lambeth");
                    array[1] = array[1].Replace("terminal", "mp_terminal_cls");
                    array[1] = array[1].Replace("underground", "mp_underground");
                    array[1] = array[1].Replace("arkaden", "mp_plaza2");
                    array[1] = array[1].Replace("decommision", "mp_shipbreaker");
                    array[1] = array[1].Replace("parish", "mp_nola");
                    array[1] = array[1].Replace("off shore", "mp_roughneck");
                    array[1] = array[1].Replace("boardwalk", "mp_boardwalk");
                    array[1] = array[1].Replace("pizza", "mp_italy");
                    array[1] = array[1].Replace("gulch", "mp_moab");
                    array[1] = array[1].Replace("foundation", "mp_cement");
                    array[1] = array[1].Replace("black box", "mp_morningwood");
                    array[1] = array[1].Replace("Sanctuary", "mp_meteora");
                    array[1] = array[1].Replace("aground", "mp_aground_ss");
                    array[1] = array[1].Replace("uturn", "mp_burn_ss");
                    array[1] = array[1].Replace("erosion", "mp_courtyard_ss");
                    array[1] = array[1].Replace("liberation", "mp_park");
                    array[1] = array[1].Replace("oasis", "mp_qadeem");
                    array[1] = array[1].Replace("vortex", "mp_six_ss");
                    array[1] = array[1].Replace("lockdown", "mp_alpha");
                    array[1] = array[1].Replace("mission", "mp_bravo");
                    array[1] = array[1].Replace("bakaara", "mp_mogadishu");
                    array[1] = array[1].Replace("seatown", "mp_seatown");
                    if ((File.Exists("admin\\" + array[2] + ".dsr") && File.Exists("zone\\english\\" + array[1] + ".ff")) || (File.Exists("admin\\" + array[2] + ".dsr") && File.Exists("zone\\dlc\\" + array[1] + ".ff")))
                    {
                        File.Delete("admin\\SinX.dspl");
                        using (StreamWriter streamWriter7 = new StreamWriter("admin\\SinX.dspl", true))
                        {
                            streamWriter7.WriteLine(array[1] + "," + array[2] + ",1000");
                        }
                        Utilities.RawSayAll(bot + "^5Changing Gametype To ^1" + array[2] + " ^0& ^3Map ^7: ^1" + array[1]);
                        Utilities.ExecuteCommand("sv_maprotation SinX");
                        base.AfterDelay(2000, new Action(this.mpr));

                    }

                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void setNextMap(Entity player, string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                char[] separator = new char[]
			{
				' '
			};
                string[] array = message.Split(separator);
                if (array[1] == "")
                {
                    Utilities.RawSayTo(player, pm + "^1type a map name");

                }
                if (array[2] == "")
                {
                    Utilities.RawSayTo(player, pm + "^1type a dsr name");

                }
                if (!File.Exists(@"admin\\" + array[2] + ".dsr"))
                {
                    Utilities.RawSayTo(player, pm + "^1DSR Not found");

                }
                if (array.Length > 3)
                {
                    Utilities.RawSayTo(player, pm + "^1WTF? Just write mapname and dsrname!");

                }
                if (array.Length > 2)
                {
                    array[1] = array[1].Replace("dome", "mp_dome");
                    array[1] = array[1].Replace("resistance", "mp_paris");
                    array[1] = array[1].Replace("village", "mp_village");
                    array[1] = array[1].Replace("bootleg", "mp_bootleg");
                    array[1] = array[1].Replace("carbon", "mp_carbon");
                    array[1] = array[1].Replace("interchange", "mp_interchange");
                    array[1] = array[1].Replace("hardhat", "mp_hardhat");
                    array[1] = array[1].Replace("downturn", "mp_exchange");
                    array[1] = array[1].Replace("outpost", "mp_radar");
                    array[1] = array[1].Replace("gateway", "mp_hillside_ss");
                    array[1] = array[1].Replace("lookout", "mp_restrepo_ss");
                    array[1] = array[1].Replace("overwatch", "mp_overwatch");
                    array[1] = array[1].Replace("fallen", "mp_lambeth");
                    array[1] = array[1].Replace("terminal", "mp_terminal_cls");
                    array[1] = array[1].Replace("underground", "mp_underground");
                    array[1] = array[1].Replace("arkaden", "mp_plaza2");
                    array[1] = array[1].Replace("decommision", "mp_shipbreaker");
                    array[1] = array[1].Replace("parish", "mp_nola");
                    array[1] = array[1].Replace("off shore", "mp_roughneck");
                    array[1] = array[1].Replace("boardwalk", "mp_boardwalk");
                    array[1] = array[1].Replace("pizza", "mp_italy");
                    array[1] = array[1].Replace("gulch", "mp_moab");
                    array[1] = array[1].Replace("foundation", "mp_cement");
                    array[1] = array[1].Replace("black box", "mp_morningwood");
                    array[1] = array[1].Replace("Sanctuary", "mp_meteora");
                    array[1] = array[1].Replace("aground", "mp_aground_ss");
                    array[1] = array[1].Replace("uturn", "mp_burn_ss");
                    array[1] = array[1].Replace("erosion", "mp_courtyard_ss");
                    array[1] = array[1].Replace("liberation", "mp_park");
                    array[1] = array[1].Replace("oasis", "mp_qadeem");
                    array[1] = array[1].Replace("vortex", "mp_six_ss");
                    array[1] = array[1].Replace("lockdown", "mp_alpha");
                    array[1] = array[1].Replace("mission", "mp_bravo");
                    array[1] = array[1].Replace("bakaara", "mp_mogadishu");
                    array[1] = array[1].Replace("seatown", "mp_seatown");
                    if ((File.Exists("admin\\" + array[2] + ".dsr") && File.Exists("zone\\english\\" + array[1] + ".ff")) || (File.Exists("admin\\" + array[2] + ".dsr") && File.Exists("zone\\dlc\\" + array[1] + ".ff")))
                    {
                        //File.Delete("admin\\SinXNextMap.dspl");
                        //File.Delete("players2\\SinXNextMap.dspl");

                        try
                        {
                            using (StreamWriter streamWriter7 = new StreamWriter(dsplpath))
                            {
                                streamWriter7.Write("");
                                streamWriter7.WriteLine(array[1] + "," + array[2] + ",1000", true);
                                streamWriter7.Close();
                                StreamReader xxxx = new StreamReader(dsplpath);
                                string dsplxx = xxxx.ReadToEnd();
                                xxxx.Close();
                                nextMapSet = true;
                                sayAsBot(player, "^5Set Next map to: ^4" + array[1] + " " + array[2]);

                                Utilities.ExecuteCommand("sv_maprotation SinXNextMap");

                            }
                        }
                        catch (Exception error)
                        { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

                    }


                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void nextmap()
        {

            StreamWriter erros = new StreamWriter(fs);
            try
            {
                StreamReader read = new StreamReader(dsplpath);
                string message = read.ReadLine();
                read.Close();
                char[] separator = new char[]
			{
				','
			};
                string[] array = message.Split(separator);

                Utilities.RawSayAll(bot + "^5Gametype ^1" + array[1] + " ^0& ^3Map ^7: ^1" + array[0]);
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public override void OnExitLevel()
        {
            //StreamWriter erros = new StreamWriter(fs);

            File.WriteAllText(@"scripts\\sinadmin\\" + cfgname+ "\\Scriptfiles\\killstreak.txt", String.Empty);
            ////PURGE THE EVIL OF BOTS
            //try
            //{
            //    foreach (Entity bot in BotList)
            //    {
            //        Utilities.RawSayAll(bot.Name);
            //        rcon("kick " + bot.Name);
            //        BotList.Remove(bot);

            //    }
            //}
            //catch (Exception error)
            //{ erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }
        public void gametype()
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                string[] array = File.ReadAllLines(@"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\dspl.cfg");
                for (int i = 0; i < array.Length; i++)
                {
                    string text = array[i];
                    if (text.StartsWith("[DSPL]="))
                    {
                        dspl = text.Split(new char[]
					{
						'='
					})[1];
                    }
                }
                if (File.Exists("admin\\\\" + dspl + ".dspl"))
                {
                    Utilities.ExecuteCommand("sv_maprotation " +  dspl);
                    return;
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void mpr()
        {
            StreamWriter erros = new StreamWriter(fs);

            try
            {
                // Utilities.SayAll("Exiting Level");
                if (File.Exists(@"admin\\SinXNextMap.dspl"))
                {
                    StreamWriter fuckThis = new StreamWriter(swit);
                    fuckThis.WriteLine("Rotated");
                    fuckThis.Close();
                    Log.Write(LogLevel.All, "nextmap");
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }


            try
            {
                Utilities.ExecuteCommand("map_rotate");
                //Utilities.ExecuteCommand("sv_maprotation default");
                //File.Delete(dsplpath);
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void ipban(string name)
        {
            StreamWriter erros = new StreamWriter(fs);
            Entity entity = this.FindByName(name);
            string str2 = entity.IP.ToString().Split(new char[] { ':' })[0];
            try
            {
                StreamWriter writer = new StreamWriter(IP);
                writer.WriteLine(entity.Name + ":" + str2 + "-" + str2 + " ");
                Utilities.RawSayAll(bot + "^5IPBanned ^1" + entity.Name);
                writer.Close();
            }
            catch (Exception)
            {
            }
        }

        public void changeTeamNames(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                player.SetClientDvar("g_TeamName_Allies", stringDvar("Axisname"));
                player.SetClientDvar("g_TeamName_Axis", stringDvar("Alliesname"));
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }


        public void sayAsBot(Entity player, string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                if (message.StartsWith("!say "))
                {
                    string temp = message.Replace("!say ", "");
                    Utilities.RawSayAll(bot + temp);
                }
                else if (message.StartsWith("!say"))
                {
                    string temp = message.Replace("!say", "");
                    Utilities.RawSayAll(bot + temp);
                }
                else
                {
                    Utilities.RawSayAll(bot + message);
                }
            }

            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

        }

        //public void hudMessage(Entity player, string message)
        //{

        //        char x = ' ';
        //        string[] client = message.Split(x);
        //        Entity ent = FindByName(client[1]);
        //        HudElem hudElem = HudElem.CreateFontString(ent, "hudsmall", 1f);
        //        hudElem.SetPoint("TOP", "TOP", 0, 400);
        //        hudElem.SetText(string.Concat(new string[]
        //    {
        //        client[2]
        //    }));

        //}

        //public void hudAlert(Entity player, string message)
        //{
        //    try
        //    {
        //        char x = ' ';
        //        string[] client = message.Split(x);
        //        foreach (Entity ent in Playerz)
        //        {
        //            HudElem hudElem = HudElem.CreateFontString(ent, "hudsmall", 1f);
        //            hudElem.SetPoint("TOP", "TOP", 0, 400);
        //            hudElem.SetText(string.Concat(new string[]
        //    {
        //        client[1]
        //    }));
        //        }
        //    }
        //    catch { }
        //}

        public void killstreakHUD(Entity player)
        {
#if dev
           
#endif
            if (dvarCheck("customicon"))
            {
                Call("setdvar", "g_TeamIcon_Allies", stringDvar("alliesicon")); //need to change
                Call("setdvar", "g_teamIcon_Axis", stringDvar("axisicon"));
            }
            StreamWriter erros = new StreamWriter(fs);
            if (!dvarCheck("antilag"))
            {
                try
                {
                    string killstreak = "^5Killstreak:^7" + player.GetField<int>("killstreak").ToString();
                    HudElem hudElem = HudElem.CreateFontString(player, "hudsmall", 1f);
                    hudElem.SetPoint("TOPCENTER", "TOPCENTER");
                    hudElem.SetText(string.Concat(new string[]
            {
                killstreak
            }));

                    OnInterval(300, () =>
                    {
                        killstreak = "^5Killstreak:^7" + player.GetField<int>("killstreak").ToString();
                        hudElem.SetText(killstreak);
                        return true;
                    });
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
            }
        }

        public void spawnKillstreak(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            if (!dvarCheck("antilag"))
            {
                try
                {
                    string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\killstreak.txt";
                    string[] strArray = File.ReadAllLines(path);
                    foreach (string str2 in strArray)
                    {
                        if (str2.Contains(player.Name))
                        {
                            string fixedLine = str2.Replace(player.Name + "=", "");
                            int x = Convert.ToInt32(fixedLine);
                            player.SetField("killstreak", x);
                        }
                    }
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
            }
        }

        public void getKillstreaks(Entity player, string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            if (!dvarCheck("antilag"))
            {
                foreach (Entity entity in Playerz)
                {
                    try
                    {
                        string str4 = entity.Name + "=" + entity.GetField<int>("killstreak").ToString();
                        Utilities.RawSayTo(player, pm + "^1Killstreak:^5" + str4);

                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public void AutoBalance()
        {
            if (dvarCheck("autobalance"))
            {
                OnInterval(30000, () =>
                {
                    deadbalance();
                    if (dvarCheck("autobalance"))
                    {
                        deadbalance();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
            }
        }
        #endregion

        #region Logs
        public void Logger(Entity player, string text)
        {
            string date = DateTime.Now.ToString("M.d.yyyy");
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                //string globalpath = @"logs\cmds.log";
                if (!File.Exists(globalpath))
                {
                    //File.WriteAllBytes(globalpath, new byte[0]);
                    StreamWriter sw = new StreamWriter(globalpath, true);
                    sw.WriteLine("");
                    sw.Close();
                }
                //string globalpath2 = @"logs\chat.log";
                if (!File.Exists(globalpath2))
                {
                    //File.WriteAllBytes(globalpath2, new byte[0]);
                    StreamWriter sw = new StreamWriter(globalpath2, true);
                    sw.WriteLine("");
                    sw.Close();
                }
                //string globalpath3 = @"logs\all.log";
                if (!File.Exists(globalpath3))
                {
                    //File.WriteAllBytes(globalpath3, new byte[0]);
                    StreamWriter sw = new StreamWriter(globalpath3, true);
                    sw.WriteLine("");
                    sw.Close();
                }
                text.Split(new char[] { ' ' });
                text.Split(new char[] { '!' });

                if (text.StartsWith("!"))
                {
                    try
                    {
                        if (dvarCheck("logcommands"))
                        {
                            StreamWriter writer2 = new StreamWriter(@"scripts\\" + "SinAdmin\\" + cfgname + "\\logs\\cmds." + date + ".log", true);
                            writer2.WriteLine(string.Concat(new object[] { DateTime.Now.ToString("M/d/yyyy"), " ", DateTime.Now.ToString("HH:mm:ss tt"), " ", player.Name, " ", player.GUID, " : ", text }));
                            writer2.Close();
                        }
                    }
                    catch (Exception error)
                    { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

                }
                //else
                // {
                try
                {
                    if (dvarCheck("logchat"))
                    {
                        StreamWriter writer3 = new StreamWriter(@"scripts\\" + "SinAdmin\\" + cfgname + "\\logs\\chat." + date + ".log", true);
                        writer3.WriteLine(DateTime.Now.ToString("M/d/yyyy") + " " + DateTime.Now.ToString("HH:mm:ss tt") + " " + player.Name + " : " + text);
                        writer3.Close();
                    }
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                //}

                try
                {
                    StreamWriter writer4 = new StreamWriter(globalpath3, true);
                    writer4.WriteLine(player.Name + " : " + text);
                    writer4.Close();
                }
                catch (Exception error)
                { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }

        }

        #endregion

        #region iSnipe

        public void antiHS(float maxtime, Entity entity)
        {
            int adsTime = 0;


            entity.OnInterval(100, player =>
            {
                if (!player.IsAlive)
                {
                    return true;
                }

                if (player.Call<float>("playerads") >= 1)
                {
                    adsTime++;
                }
                else
                {
                    adsTime = 0;
                }
                if (adsTime >= 0.15 * 10)
                {
                    adsTime = 0;

                    string wep = player.CurrentWeapon;
                    if (wep.Contains("iw5_l96a1") || wep.Contains("iw5_msr"))
                    {
                        player.Call("allowads", false);
                        OnInterval(50, () =>
                        {
                            if (player.Call<int>("adsbuttonpressed") > 0)
                            {
                                return true;
                            }
                            player.Call("allowads", true);
                            return false;
                        });
                    }
                }


                return true;
            });

        }



        public void antiNoscope(Entity player)
        {

            player.OnNotify("weapon_fired", (dude, weaponname) =>
            {
                if (player.Call<float>("playerads") == 0)
                {
                    string wep = player.CurrentWeapon;
                    if (wep.Contains("iw5_l96a1") || wep.Contains("iw5_msr"))
                    {
                        Utilities.RawSayTo(player, pm + "^1No: NoScoping");
                        player.Call("suicide");
                    }
                }
            });
        }

        public void antiBoltCancel(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            player.Call("notifyOnPlayerCommand", new Parameter[] { "relo", "+reload" });
            player.SetField("Firedd", 0);
            player.SetField("note", 0);
            try
            {
                player.OnNotify("weapon_fired", (dude, weaponname) =>
                {
                    int fu = dude.GetField<int>("note  ");
                    if (fu == 1)
                    {
                        dude.SetField("Firedd", 1);
                    }
                    try
                    {
                        int lel = dude.GetField<int>("note");
                        if (lel == 0)
                        {
                            player.OnNotify("relo", delegate(Entity ent)
                            {
                                int magosh = dude.GetField<int>("Firedd");
                                if (magosh == 1)
                                {
                                    dude.SetField("Firedd", 0);
                                    player.SetField("note", 1);
                                    dude.SetField("CheeseNiggah", 1);
                                }
                            });
                        }
                        else
                        {
                            player.SetField("note", 1);
                        }
                        int x = dude.GetField<int>("CheeseNiggah");
                        int y = dude.GetWeaponAmmoClip(player.CurrentWeapon);

                        if (x == 1 && (y > 0))
                        {
                            Utilities.SayTo(dude, "^1No: ^5BoltCancel ^0#BioPwnedU");
                            dude.SetField("CheeseNiggah", 0);
                            dude.Call("stunplayer", 1);
                            AfterDelay(100, () =>
                            {
                                dude.Call("stunplayer", 0);
                            });
                        }
                    }
                    catch (Exception error)
                    { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
                    //AfterDelay(80, () =>
                    //    {
                    //        dude.SetField("FUCKA", 0);
                    //    });
                });
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void antiHalf(Entity entity)
        {
            entity.SetField("iShot", 0);
            double timer = 0;
            float xx = 0;
            
            entity.OnInterval(5, player =>
            {
                if (!player.IsAlive)
                {
                    return true;
                }


                if ((player.Call<int>("adsbuttonpressed") > 0))
                {

                    timer = timer + 0.05;
                    //  Utilities.RawSayAll("ADS:" + timer.ToString());
                }
                else
                {
                    timer = 0;
                }

                if ((player.Call<float>("playerads") > 0))
                {
                    xx += 0.01f;
                    //Utilities.RawSayAll("ADS:" + xx.ToString());
                }
                else
                {
                    xx = 0;
                }

                if ((player.Call<int>("attackbuttonpressed") > 0))
                {

                    //timer2++;
                    entity.SetField("iShot", 1);
                    //   Utilities.RawSayAll("Shoot:" + timer2.ToString());
                }
               
                //changed to 0.01   
                if (/*timer <= maxhalftime && timer >= 0.01*/ xx <= floatDvar("maxhalftime") && xx >= 0.01f && (player.GetField<int>("iShot") == 1)/*timer2 > 0.1 && (player.Call<float>("playerads") != 0)*/)
                {
                    xx = 0;
                  
                    timer = 0;
                    entity.SetField("iShot", 0);
                    //    Utilities.RawSayAll(timer2.ToString
                    string wep = player.CurrentWeapon;
                    if (wep.Contains("iw5_l96a1") || wep.Contains("iw5_msr"))
                    {
                        Utilities.RawSayTo(player, bot + "^1No: Half-Scoping");
                        player.Call("suicide");
                    }
                }
                else
                {
                    player.SetField("iShot", 0);
                }

                return true;
            });

        }

        #endregion

        #region Promod

        public void setFov(Entity player, string value)
        {
            player.SetClientDvar("cg_fovScale", value);
        }

        public void FPS(Entity player, string value)
        {
            player.SetClientDvar("r_colorMap", value);
        }


        public void basePromod(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);
            if (dvarCheck("promod"))
            {
                //player.SetClientDvar("cg_ScoresPing_HighColor", "0 0 0 1");
                //player.SetClientDvar("cg_ScoresPing_LowColor", "0 0 0 1");
                //player.SetClientDvar("cg_ScoresPing_MedColor", "1 1 0 1");
                player.SetClientDvar("cl_maxpackets", "100");
                player.SetClientDvar("r_fog", "0");
                player.SetClientDvar("fx_drawclouds", "0");
                player.SetClientDvar("r_distortion", "0");
                player.SetClientDvar("r_dlightlimit", "0");
                player.SetClientDvar("cg_brass", "0");
                player.SetClientDvar("snaps", "30");
                player.SetClientDvar("con_maxfps", "0");
                player.SetClientDvar("r_detailmap", "0");
                player.SetClientDvar("cg_scoreboardpingtext", "1");
                player.SetClientDvar("clientsideeffects", "1");
                player.SetClientDvar("r_normalMap", "Flat");
                player.SetClientDvar("cg_scoreboardItemHeight", "14");
                player.SetClientDvar("cg_scoreboardBannerHeight", "20");
                player.SetClientDvar("useRelativeTeamColors", "1");
                player.SetClientDvar("clientsideeffects", "0");
                player.SetClientDvar("dynent_active", "0");
                player.SetClientDvar("compassobjectivewidth", "8");
                player.SetClientDvar("compassobjectiveheight", "8");
                player.SetClientDvar("waypointIconHeight", "3");
                player.SetClientDvar("waypointIconWidth", "3");
                player.SetClientDvar("r_dlightlimit", "0");
                Call("setdvar", "glass_DamageToDestroy", "5");
                Call("setdvar", "cg_drawBreathHint", "0"); //Show 'hold /shift/ to steady' hint while scoping
                player.SetClientDvar("lowAmmoWarningColor1", "0 0 0 0");
                player.SetClientDvar("lowAmmoWarningColor2", "0 0 0 0");
                player.SetClientDvar("lowAmmoWarningNoAmmoColor1", "0 0 0 0");
                player.SetClientDvar("lowAmmoWarningNoAmmoColor2", "0 0 0 0");
                player.SetClientDvar("lowAmmoWarningNoReloadColor1", "0 0 0 0");
                player.SetClientDvar("lowAmmoWarningNoReloadColor2", "0 0 0 0");
                player.SetClientDvar("cl_demo_enabled", "1");
            }
            else
            {
                player.SetClientDvar("r_fog", "0");
                //player.SetClientDvar("cg_ScoresPing_HighColor", "0 0 0 1");
                //player.SetClientDvar("cg_ScoresPing_LowColor", "0 0 0 1");
                //player.SetClientDvar("cg_ScoresPing_MedColor", "1 1 0 1");
                Call("setdvar", "cg_drawBreathHint", "0"); //Show 'hold /shift/ to steady' hint while scoping
                player.SetClientDvar("lowAmmoWarningColor1", "0 0 0 0");
                player.SetClientDvar("lowAmmoWarningColor2", "0 0 0 0");
                player.SetClientDvar("lowAmmoWarningNoAmmoColor1", "0 0 0 0");
                player.SetClientDvar("lowAmmoWarningNoAmmoColor2", "0 0 0 0");
                player.SetClientDvar("lowAmmoWarningNoReloadColor1", "0 0 0 0");
                player.SetClientDvar("lowAmmoWarningNoReloadColor2", "0 0 0 0");
                player.SetClientDvar("cl_demo_enabled", "1");
                player.SetClientDvar("con_maxfps", "125");
            }

            if (!Bio)
            {
                Call("setdvar", "bg_weaponBobMax", "0");
                Call("setdvar", "bg_viewBobMax", "0");
                Call("setdvar", "bg_viewBobAmplitudeStandingAds", "0 0");
                Call("setdvar", "bg_viewBobAmplitudeSprinting", "0 0");
                Call("setdvar", "bg_viewBobAmplitudeDucked", "0 0");
                Call("setdvar", "bg_viewBobAmplitudeDuckedAds", "0 0");
                Call("setdvar", "bg_viewBobAmplitudeProne", "0 0");
                Call("setdvar", "scr_team_teamkillpointloss", "1");
                Call("setdvar", "g_knockback", "0");
                Call("setdvar", "bg_viewKickRandom", "0.2");
                Call("setdvar", "bg_viewKickMin", "1");
                Call("setdvar", "bg_viewKickScale", "0.15");
                Call("setdvar", "bg_viewKickMax", "75");
                Call("setdvar", "glass_DamageToDestroy", "50");
                Call("setdvar", "g_playerCollisionEjectSpeed", "15");
                Call("setdvar", "perk_sprintMultiplier", "1.25");
                Call("setdvar", "perk_bulletPenetrationMultiplier", "1.6");
                //         Call("setdvar","perk_quickDrawSpeedScale", "1.1");
                Call("setdvar", "sv_network_fps", "100");
            }

            else
            {
                FuckPromod();
                //      player.SetClientDvar("cg_fov", "80");
                player.SetClientDvar("cg_scoreboardpingtext", "1");
                player.SetClientDvar("waypointIconHeight", "3");
                player.SetClientDvar("waypointIconWidth", "3");
                //   player.SetClientDvar("cl_maxpackets", "60");
                //  player.SetClientDvar("r_fog", "0");
                player.SetClientDvar("compassobjectivewidth", "8");
                player.SetClientDvar("compassobjectiveheight", "8");
                //    player.SetClientDvar("fx_drawclouds", "1");
                //    player.SetClientDvar("r_distortion", "1");
                //     player.SetClientDvar("r_dlightlimit", "4");
                //     player.SetClientDvar("cg_brass", "1");
                player.SetClientDvar("snaps", "20");
                //     player.SetClientDvar("com_maxfps", "91");
                player.SetClientDvar("clientsideeffects", "1");
            }
        }

        public void promod(Entity player, string message)
        {
            StreamWriter erros = new StreamWriter(fs);
            char x = ' ';
            string[] splitteh = message.Split(x);
            string msg = splitteh[1].Remove(0, 1);

            switch (splitteh[1])
            {
                case "0":
                    player.SetClientDvar("r_filmusetweaks", "0");
                    player.SetClientDvar("r_filmtweakenable", "0");
                    player.SetClientDvar("r_colorMap", "1");
                    player.SetClientDvar("r_specularMap", "1");
                    player.SetClientDvar("r_normalMap", "1");

                    break;
                case "1":
                    player.SetClientDvar("r_filmtweakdarktint", "0.65 0.7 0.8");
                    player.SetClientDvar("r_filmtweakcontrast", "1.3");
                    player.SetClientDvar("r_filmtweakbrightness", "0.15");
                    player.SetClientDvar("r_filmtweakdesaturation", "0");
                    player.SetClientDvar("r_filmusetweaks", "1");
                    player.SetClientDvar("r_filmtweaklighttint", "1.8 1.8 1.8");
                    player.SetClientDvar("r_filmtweakenable", "1");
                    break;

                case "2":
                    player.SetClientDvar("r_filmtweakdarktint", "1.15 1.1 1.3");
                    player.SetClientDvar("r_filmtweakcontrast", "1.6");
                    player.SetClientDvar("r_filmtweakbrightness", "0.2");
                    player.SetClientDvar("r_filmtweakdesaturation", "0");
                    player.SetClientDvar("r_filmusetweaks", "1");
                    player.SetClientDvar("r_filmtweaklighttint", "1.35 1.3 1.25");
                    player.SetClientDvar("r_filmtweakenable", "1");
                    break;

                case "3":
                    player.SetClientDvar("r_filmtweakdarktint", "0.8 0.8 1.1");
                    player.SetClientDvar("r_filmtweakcontrast", "1.3");
                    player.SetClientDvar("r_filmtweakbrightness", "0.48");
                    player.SetClientDvar("r_filmtweakdesaturation", "0");
                    player.SetClientDvar("r_filmusetweaks", "1");
                    player.SetClientDvar("r_filmtweaklighttint", "1 1 1.4");
                    player.SetClientDvar("r_filmtweakenable", "1");
                    break;

                case "4":
                    player.SetClientDvar("r_filmtweakdarktint", "1.8 1.8 2");
                    player.SetClientDvar("r_filmtweakcontrast", "1.25");
                    player.SetClientDvar("r_filmtweakbrightness", "0.02");
                    player.SetClientDvar("r_filmtweakdesaturation", "0");
                    player.SetClientDvar("r_filmusetweaks", "1");
                    player.SetClientDvar("r_filmtweaklighttint", "0.8 0.8 1");
                    player.SetClientDvar("r_filmtweakenable", "1");
                    break;

                case "5":
                    player.SetClientDvar("r_filmtweakdarktint", "1 1 2");
                    player.SetClientDvar("r_filmtweakcontrast", "1.5");
                    player.SetClientDvar("r_filmtweakbrightness", "0.07");
                    player.SetClientDvar("r_filmtweakdesaturation", "0");
                    player.SetClientDvar("r_filmusetweaks", "1");
                    player.SetClientDvar("r_filmtweaklighttint", "1 1.2 1");
                    player.SetClientDvar("r_filmtweakenable", "1");
                    break;

                case "6":
                    player.SetClientDvar("r_filmtweakdarktint", "1.5 1.5 2");
                    player.SetClientDvar("r_filmtweakcontrast", "1");
                    player.SetClientDvar("r_filmtweakbrightness", "0.0.4");
                    player.SetClientDvar("r_filmtweakdesaturation", "0");
                    player.SetClientDvar("r_filmusetweaks", "1");
                    player.SetClientDvar("r_filmtweaklighttint", "1.5 1.5 1");
                    player.SetClientDvar("r_filmtweakenable", "1");
                    break;

                case "7":
                    player.SetClientDvar("r_specularMap", "2");
                    player.SetClientDvar("r_normalMap", "0");

                    break;

                case "8":
                    player.SetClientDvar("cg_drawFPS", "1");
                    player.SetClientDvar("cg_fovScale", "1.5");
                    break;

                case "9":
                    player.SetClientDvar("r_debugShader", "1");
                    break;

                case "10":
                    player.SetClientDvar("r_colorMap", "3");
                    break;

                case "default":
                    player.SetClientDvar("r_filmtweakdarktint", "0.7 0.85 1");
                    player.SetClientDvar("r_filmtweakcontrast", "1.4");
                    player.SetClientDvar("r_filmtweakdesaturation", "0.2");
                    player.SetClientDvar("r_filmusetweaks", "0");
                    player.SetClientDvar("r_filmtweaklighttint", "1.1 1.05 0.85");
                    player.SetClientDvar("cg_fov", "66");
                    player.SetClientDvar("cg_scoreboardpingtext", "1");
                    player.SetClientDvar("waypointIconHeight", "13");
                    player.SetClientDvar("waypointIconWidth", "13");
                    player.SetClientDvar("cl_maxpackets", "100");
                    player.SetClientDvar("r_fog", "0");
                    player.SetClientDvar("fx_drawclouds", "0");
                    player.SetClientDvar("r_distortion", "0");
                    player.SetClientDvar("r_dlightlimit", "0");
                    player.SetClientDvar("cg_brass", "0");
                    player.SetClientDvar("snaps", "30");
                    player.SetClientDvar("com_maxfps", "100");
                    player.SetClientDvar("clientsideeffects", "0");
                    player.SetClientDvar("r_filmTweakBrightness", "0.2");
                    break;

            }

        }

        public void setCustomDvar(Entity player, string choice)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                string dvarPath = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\Promod";
                string[] dvars = new string[100];
                dvars = File.ReadAllLines(dvarPath + "\\" + choice + ".txt");
                foreach (string temp in dvars)
                {
                    string[] spllitehs = temp.Split(';');
                    if (temp.ToLower().Contains("client"))
                    {
                        player.SetClientDvar(spllitehs[1], spllitehs[2]);
                        Utilities.SayTo(player, "^5Dvars ^0set: ^1Client");
                    }
                    else if (temp.ToLower().Contains("server"))
                    {
                        Call("setdvar", spllitehs[1], spllitehs[2]);
                    }
                }
                Utilities.SayTo(player, "^5Dvars: ^0" + choice + " ^1set");
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        public void CustomDvar(Entity player, string choice)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                switch (choice)
                {
                    case "1":
                        setCustomDvar(player, choice);
                        break;

                    case "2":
                        setCustomDvar(player, choice);
                        break;

                    case "3":
                        setCustomDvar(player, choice);
                        break;

                    case "4":
                        setCustomDvar(player, choice);
                        break;

                    case "5":
                        setCustomDvar(player, choice);
                        break;

                    case "6":
                        setCustomDvar(player, choice);
                        break;

                    case "7":
                        setCustomDvar(player, choice);
                        break;

                    case "8":
                        setCustomDvar(player, choice);
                        break;

                    case "9":
                        setCustomDvar(player, choice);
                        break;

                    case "10":
                        setCustomDvar(player, choice);
                        break;

                    case "default":
                        Utilities.SayTo(player, "CustomDvar option does not exist!");
                        break;
                }
            }
            catch (Exception error)
            {
                Utilities.SayTo(player, "CustomDvar option does not exist!");
            }
        }

        public void getDefault()
        {
            StreamWriter erros = new StreamWriter(fs);
            string[] shit = new string[50];
            shit[0] = Call<string>("getdvar", "bg_weaponBobMax");
            shit[1] = Call<string>("getdvar", "bg_viewBobMax");
            shit[2] = Call<string>("getdvar", "bg_viewBobAmplitudeStandingAds");
            shit[3] = Call<string>("getdvar", "bg_viewBobAmplitudeSprinting");
            shit[4] = Call<string>("getdvar", "bg_viewBobAmplitudeDucked");
            shit[5] = Call<string>("getdvar", "bg_viewBobAmplitudeDuckedAds");
            shit[6] = Call<string>("getdvar", "bg_viewBobAmplitudeProne");
            shit[7] = Call<string>("getdvar", "scr_team_teamkillpointloss");
            shit[8] = Call<string>("getdvar", "g_knockback");
            shit[9] = Call<string>("getdvar", "bg_viewKickRandom");
            shit[10] = Call<string>("getdvar", "bg_viewKickMin");
            shit[11] = Call<string>("getdvar", "bg_viewKickScale");
            shit[12] = Call<string>("getdvar", "bg_viewKickMax");
            shit[13] = Call<string>("getdvar", "glass_DamageToDestroy");
            shit[14] = Call<string>("getdvar", "g_playerCollisionEjectSpeed");
            //        Call("setdvar","perk_sprintMultiplier", "1.25");
            shit[15] = Call<string>("getdvar", "perk_bulletPenetrationMultiplier");
            //         Call("setdvar","perk_quickDrawSpeedScale", "1.1");
            shit[16] = Call<string>("getdvar", "sv_network_fps");


            StreamWriter writer = new StreamWriter(@"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\FuckRogue.txt", true);
            foreach (string s in shit)
            {
                writer.WriteLine(s, true);
                writer.Flush();
            }
            writer.Close();
        }

        public void FuckPromod()
        {
            string[] shit = File.ReadAllLines(@"scripts\\" + "SinAdmin\\" + cfgname+ "\\ScriptFiles\\FuckRogue.txt");

            Call("setdvar", "bg_weaponBobMax", shit[0]);
            Call("setdvar", "bg_viewBobMax", shit[1]);
            Call("setdvar", "bg_viewBobAmplitudeStandingAds", shit[2]);
            Call("setdvar", "bg_viewBobAmplitudeSprinting", shit[3]);
            Call("setdvar", "bg_viewBobAmplitudeDucked", shit[4]);
            Call("setdvar", "bg_viewBobAmplitudeDuckedAds", shit[5]);
            Call("setdvar", "bg_viewBobAmplitudeProne", shit[6]);
            Call("setdvar", "scr_team_teamkillpointloss", shit[7]);
            Call("setdvar", "g_knockback", shit[8]);
            Call("setdvar", "bg_viewKickRandom", shit[9]);
            Call("setdvar", "bg_viewKickMin", shit[10]);
            Call("setdvar", "bg_viewKickScale", shit[11]);
            Call("setdvar", "bg_viewKickMax", shit[12]);
            Call("setdvar", "glass_DamageToDestroy", shit[13]);
            Call("setdvar", "g_playerCollisionEjectSpeedv", shit[14]);
            //        Call("setdvar","perk_sprintMultiplier", "1.25");
            Call("setdvar", "perk_bulletPenetrationMultiplier", shit[15]);
            //         Call("setdvar","perk_quickDrawSpeedScale", "1.1");
            Call("setdvar", "sv_network_fps", shit[16]);
        }

        #endregion

        #region BotMod

        public void botmod(string bl)
        {
            StreamWriter erros = new StreamWriter(fs);
            try
            {
                string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\SinScript.cfg";
                int counter = 0;
                if (bl.ToLower() == "on")
                {
                    string[] lines = File.ReadAllLines(path);
                    foreach (string s in lines)
                    {
                        if (s.ToLower().StartsWith("[botmod]"))
                        {
                            lines[counter] = "[botmod];:;1";
                            File.WriteAllLines(path, lines);
                        }
                        counter++;
                    }
                    Call("setdvar", "botmod", "1");
                    Utilities.RawSayAll(bot + "^5BotMod Enabled.");
                }
                else if (bl.ToLower() == "off")
                {
                    string[] lines = File.ReadAllLines(path);
                    foreach (string s in lines)
                    {
                        if (s.ToLower().StartsWith("[botmod]"))
                        {
                            lines[counter] = "[botmod];:;0";
                            File.WriteAllLines(path, lines);
                        }
                        counter++;
                    }
                    Call("setdvar", "botmod", "0");
                    Utilities.RawSayAll(bot + "^5BotMod Disabled.");
                }
            }
            catch (Exception error)
            { erros.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().Name + ": " + error.TargetSite.ToString() + ": " + error.Message.ToString(), true); erros.Flush(); }
        }

        #endregion

        #region TrickShot Mod

        public void explosiveBullets()
        {
            if (dvarCheck("explosivebullets") == true)
            {
                Call("setdvar", "explosivebullets", "0");
                Utilities.RawSayAll(bot + "^1Explosive bullets disabled.");
            }
            else if (dvarCheck("explosivebullets") == false)
            {
                Call("setdvar", "explosivebullets", "1");
                Utilities.RawSayAll(bot + "^2Explosive bullets enabled.");
            }

            string path = @"scripts\\" + "SinAdmin\\" + cfgname+ "\\SinScript.cfg";

            string[] lines = File.ReadAllLines(path);
            int counter = -1;

            foreach (string s in lines)
            {
                counter++;
                if (s.ToLower().StartsWith("[explosivebullets]"))
                {
                    if (dvarCheck("explosivebullets") == false)
                    {
                        lines[counter] = "[ExplosiveBullets];:;0";
                    }
                    else if (dvarCheck("explosivebullets") == true)
                    {
                        lines[counter] = "[ExplosiveBullets];:;1";
                    }
                    File.WriteAllLines(path, lines);
                    break;
                }
            }
        }

        public void ebshot(Entity player)
        {
            StreamWriter erros = new StreamWriter(fs);

            Entity target = null;
            float leastd = 0F;

            string team = player.GetField<string>("sessionteam");
            string otherteam = "";

            OnInterval(35, () =>
            {
                try
                {
                    if (dvarCheck("botmod"))
                    {
                        leastd = 999999F;
                        target = null;

                        foreach (Entity pot in BotList)
                        {
                            if (pot.IsAlive)
                            {
                                float distance = pot.Origin.DistanceTo(player.Origin);
                                if (distance < leastd)
                                {
                                    leastd = distance;
                                    target = pot;
                                }
                            }
                        }
                    }
                    else
                    {
                        leastd = 999999F;
                        target = null;

                        foreach (Entity pot in Playerz)
                        {
                            otherteam = pot.GetField<string>("sessionteam");
                            if (pot != player && pot.IsAlive && team != otherteam)
                            {
                                float distance = pot.Origin.DistanceTo(player.Origin);
                                if (distance < leastd)
                                {
                                    leastd = distance;
                                    target = pot;
                                }
                            }
                        }
                    }
                    if (!player.IsAlive)
                    {
                        return false;
                    }
                    if ((player.Call<int>("attackbuttonpressed") > 0))
                    {
                        // Utilities.RawSayTo(player, target.Name);
                        var weapon = player.CurrentWeapon;
                        var head = target.Call<Vector3>("gettagorigin", "j_head");
                        head.X = head.X - 5;
                        head.Y = head.Y - 5;
                        head.Z = head.Z - 5;
                        //Call("magicbullet", weapon, player.Call<Vector3>("gettagorigin", "tag_weapon_right"), target.Origin, player);
                        //Call("magicbullet", weapon, player.Call<Vector3>("gettagorigin", "tag_weapon_right"), target.Call<Vector3>("gettagorigin", "j_head"), player);
                        Call("magicbullet", weapon, head, target.Call<Vector3>("gettagorigin", "j_head"), player);
                        return true;
                    }
                    return true;
                }
                catch (Exception error)
                { return true; }

            });

        }

        #endregion

        #region Anti's


        private void datAntiAim()
        {
            string[] readDat = File.ReadAllLines(@"scripts\\" + "SinAdmin\\" + cfgname + "\\ScriptFiles\\antiaim.cfg");
            foreach (string x in readDat)
            {
                string[] check = x.Split('=');
                switch (check[0].ToLower())
                {
                    /*
                        player.SetField("headshots", 0);
                        player.SetField("neckshots", 0);
                        player.SetField("torso_upper", 0);
                        player.SetField("torso_lower", 0);
                        player.SetField("right_arm_upper", 0);
                        player.SetField("right_arm_lower", 0);
                        player.SetField("left_arm_upper", 0);
                        player.SetField("left_arm_lower", 0);
                        player.SetField("left_leg_upper", 0);
                        player.SetField("left_leg_lower", 0);
                        player.SetField("right_leg_upper", 0);
                        player.SetField("right_leg_lower", 0);
                     */
                    case "[headshots]":
                        string[] amount = x.Split('=');
                        headshots = amount[1];
                        break;

                    case "[neckshots]":
                        string[] amount1 = x.Split('=');
                        neckshots = amount1[1];
                        break;

                    case "[torso_upper]":
                        string[] amount2 = x.Split('=');
                        torso_upper = amount2[1];
                        break;

                    case "[torso_lower]":
                        string[] amount2e = x.Split('=');
                        torso_lower = amount2e[1];
                        break;

                    case "[right_arm_upper]":
                        string[] amount3 = x.Split('=');
                        right_arm_upper = amount3[1];
                        break;

                    case "[right_arm_lower]":
                        string[] amount4 = x.Split('=');
                        right_arm_lower = amount4[1];
                        break;

                    case "[left_arm_upper]":
                        string[] amount5 = x.Split('=');
                        left_arm_upper = amount5[1];
                        break;

                    case "[left_arm_lower]":
                        string[] amount6 = x.Split('=');
                        left_arm_lower = amount6[1];
                        break;

                    case "[left_leg_upper]":
                        string[] amount7 = x.Split('=');
                        left_leg_upper = amount7[1];
                        break;

                    case "[left_leg_lower]":
                        string[] amount8 = x.Split('=');
                        left_leg_lower = amount8[1];
                        break;

                    case "[right_leg_upper]":
                        string[] amount9 = x.Split('=');
                        right_leg_upper = amount9[1];
                        break;

                    case "[right_leg_lower]":
                        string[] amount0 = x.Split('=');
                        right_leg_lower = amount0[1];
                        break;
                }
            }
        }

        public void antiCamper(Entity entity)
        {
            // bombsite using
            entity.SetField("ac_using", 0);

            entity.OnNotify("use_hold", player =>
            {
                player.SetField("ac_using", 1);
            });

            entity.OnNotify("done_using", player =>
            {
                player.SetField("ac_using", 0);
            });

            entity.OnInterval(intDvar("anticamptime"), player =>
            {
                if (!player.IsAlive)
                {
                    return true;
                }

                if (!_prematch)
                {
                    return true;
                }

                if (player.GetField<int>("ac_using") != 0)
                {
                    return true;
                }

                foreach (var safeEntity in _safeTriggers)
                {
                    if (player.Call<int>("istouching", safeEntity) != 0)
                    {
                        Log.Write(LogLevel.Trace, "touching safe entity");
                        return true;
                    }
                }

                if (player.Call<int>("istalking") != 0)
                {
                    Log.Write(LogLevel.Trace, "talking");
                    return true;
                }

                if (player.HasField("ac_lastPos"))
                {
                    var lastPos = player.GetField<Vector3>("ac_lastPos");

                    if (lastPos.DistanceTo2D(player.Origin) < 50)
                    {
                        player.Call("iprintlnbold", "^5You will be ^1killed if you do not move.");

                        var oldHealth = player.Health;
                        player.Health /= 3;
                        player.Notify("damage", (oldHealth - player.Health), player, new Vector3(0, 0, 0), new Vector3(0, 0, 0), "MOD_EXPLOSIVE", "", "", "", 0, "frag_grenade_mp");

                        if (player.Health < 20)
                        {
                            player.Call("suicide");
                        }
                        //AfterDelay(6000, () =>
                        //{
                        //    setVision(player, "blacktest");
                        //    AfterDelay(3000, () =>
                        //        {
                        //            setVision(player, "mp_dome");
                        //        });
                        //    player.Call("stunplayer", 1);
                        //});
                        // player.Call("suicide");
                    }
                }

                player.SetField("ac_lastPos", player.Origin);
                return true;
            });
        }

        public void AntiCamp()
        {
            var gameType = Call<string>("getDvar", "g_gametype").ToLower();

            for (int i = 0; i < 2048; i++)
            {
                var entity = Call<Entity>("getEntByNum", i);

                if (entity != null)
                {
                    var targetname = entity.GetField<string>("targetname");

                    if (gameType == "dom")
                    {
                        if (targetname == "flag_primary" || targetname == "flag_secondary")
                        {
                            _safeTriggers.Add(entity);
                        }
                    }
                    else if (gameType == "koth")
                    {
                        if (targetname == "radiotrigger")
                        {
                            _safeTriggers.Add(entity);
                        }
                    }
                }
            }

            // don't trigger anticamp after the game ended
            OnNotify("game_over", () =>
            {
                _prematch = false;
            });

        }
        #endregion

        #region Memory Editing
        public void initM()
        {
            memoryHax mem = new memoryHax();
            mem.WriteInteger(0x4e521b, 235);
        }
        public void gravity(int grav)
        {
            memoryHax mem = new memoryHax();
            mem.WriteInteger(0x4768c6, grav);
        }
        public void jump(int jum)
        {
            memoryHax mem = new memoryHax();
            mem.WriteInteger(0x6da708, jum);
        }
        public void speedm(int spee)
        {
            memoryHax mem = new memoryHax();
            mem.WriteInteger(0x4760ea, spee);
        }
        public void writeMemname(Entity ent, string name)
        {
            memoryHax mem = new memoryHax();

            if (mem.Process_Handle("teknomw3_dedicated"))
            {
                int client0 = 0x01AC5508;
                int client1 = client0 + 0x38A4;
                int client2 = client1 + 0x38A4;
                int client3 = client2 + 0x38A4;
                int client4 = client3 + 0x38A4;
                int client5 = client4 + 0x38A4;
                int client6 = client5 + 0x38A4;
                int client7 = client6 + 0x38A4;
                int client8 = client7 + 0x38A4;
                int client9 = client8 + 0x38A4;
                int client10 = client9 + 0x38A4;
                int client11 = client1 + 0x38A4;
                int client12 = client11 + 0x38A4;
                int client13 = client12 + 0x38A4;
                int client14 = client13 + 0x38A4;
                int client15 = client14 + 0x38A4;
                int client16 = client15 + 0x38A4;
                int client17 = client16 + 0x38A4;

                string[] names = new string[18];
                names[0] = mem.ReadString(client0, 15);
                names[1] = mem.ReadString(client1, 15);
                names[2] = mem.ReadString(client2, 15);
                names[3] = mem.ReadString(client3, 15);
                names[4] = mem.ReadString(client4, 15);
                names[5] = mem.ReadString(client5, 15);
                names[6] = mem.ReadString(client6, 15);
                names[7] = mem.ReadString(client7, 15);
                names[8] = mem.ReadString(client8, 15);
                names[9] = mem.ReadString(client9, 15);
                names[10] = mem.ReadString(client10, 15);
                names[11] = mem.ReadString(client11, 15);
                names[12] = mem.ReadString(client12, 15);
                names[13] = mem.ReadString(client13, 15);
                names[14] = mem.ReadString(client14, 15);
                names[15] = mem.ReadString(client15, 15);
                names[16] = mem.ReadString(client16, 15);
                names[17] = mem.ReadString(client17, 15);

                int theChosenOne = 0;

                for (int x = 0; x < 18; x++)
                {
                    if (names[x].Contains(ent.Name))
                    {
                        theChosenOne = x;
                    }
                }

                switch (theChosenOne)
                {
                    case 0:
                        mem.WriteString(client0, "               ");
                        mem.WriteString(client0, name);
                        break;
                    case 1:
                        mem.WriteString(client1, "               ");
                        mem.WriteString(client1, name);
                        break;
                    case 2:
                        mem.WriteString(client2, "               ");
                        mem.WriteString(client2, name);
                        break;
                    case 3:
                        mem.WriteString(client3, "               ");
                        mem.WriteString(client3, name);
                        break;
                    case 4:
                        mem.WriteString(client4, "               ");
                        mem.WriteString(client4, name);
                        break;
                    case 5:
                        mem.WriteString(client5, "               ");
                        mem.WriteString(client5, name);
                        break;
                    case 6:
                        mem.WriteString(client6, "               ");
                        mem.WriteString(client6, name);
                        break;
                    case 7:
                        mem.WriteString(client7, "               ");
                        mem.WriteString(client7, name);
                        break;
                    case 8:
                        mem.WriteString(client8, "               ");
                        mem.WriteString(client8, name);
                        break;
                    case 9:
                        mem.WriteString(client9, "               ");
                        mem.WriteString(client9, name);
                        break;
                    case 10:
                        mem.WriteString(client10, "               ");
                        mem.WriteString(client10, name);
                        break;
                    case 11:
                        mem.WriteString(client11, "               ");
                        mem.WriteString(client11, name);
                        break;
                    case 12:
                        mem.WriteString(client12, "               ");
                        mem.WriteString(client12, name);
                        break;
                    case 13:
                        mem.WriteString(client13, "               ");
                        mem.WriteString(client13, name);
                        break;
                    case 14:
                        mem.WriteString(client14, "               ");
                        mem.WriteString(client14, name);
                        break;
                    case 15:
                        mem.WriteString(client15, "               ");
                        mem.WriteString(client15, name);
                        break;
                    case 16:
                        mem.WriteString(client16, "               ");
                        mem.WriteString(client16, name);
                        break;
                    case 17:
                        mem.WriteString(client17, "               ");
                        mem.WriteString(client17, name);
                        break;

                    default:
                        break;
                }
            }
        }

        #endregion

        #region GEOIP
        public string whereFROM(Entity player)
        {
            string newValue = "";
            string country = this.Ip2Country.GetCountry(player.IP.ToString().Split(new char[] { ':' })[0]);
            foreach (KeyValuePair<string, string> pair in this.IsoCountries)
            {
                if (pair.Value == country)
                {
                    newValue = pair.Key;
                    break;
                }
            }
            return country;
        }

        public void AddCountry(string continent, string region, string countryname, string iso)
        {
            this.ISOToCountryName[string.Intern(iso)] = string.Intern(countryname);
            this.CountryNameToISO[string.Intern(countryname)] = string.Intern(iso);
            this.IsoCountries.Add(countryname, iso);
            Continent continent2 = (Continent)this.Continents[continent];
            if (continent2 == null)
            {
                continent2 = new Continent();
                this.Continents[continent] = continent2;
            }
            Region region2 = (Region)continent2.Regions[region];
            if (region2 == null)
            {
                region2 = new Region();
                continent2.Regions[region] = region2;
            }
            region2.CountryNames[countryname] = iso;
        }

        public void Countries()
        {
            this.AddCountry("Africa", "Central", "Burundi", "BI");
            this.AddCountry("Africa", "Central", "Central African Republic", "CF");
            this.AddCountry("Africa", "Central", "Chad", "TD");
            this.AddCountry("Africa", "Central", "Congo", "CG");
            this.AddCountry("Africa", "Central", "Rwanda", "RW");
            this.AddCountry("Africa", "Central", "Zaire (Congo)", "ZR");
            this.AddCountry("Africa", "Eastern", "Djibouti", "DJ");
            this.AddCountry("Africa", "Eastern", "Eritrea", "ER");
            this.AddCountry("Africa", "Eastern", "Ethiopia", "ET");
            this.AddCountry("Africa", "Eastern", "Kenya", "KE");
            this.AddCountry("Africa", "Eastern", "Somalia", "SO");
            this.AddCountry("Africa", "Eastern", "Tanzania", "TZ");
            this.AddCountry("Africa", "Eastern", "Uganda", "UG");
            this.AddCountry("Africa", "Other", "Comoros", "KM");
            this.AddCountry("Africa", "Other", "Madagascar", "MG");
            this.AddCountry("Africa", "Other", "Mauritius", "MU");
            this.AddCountry("Africa", "Other", "Mayotte", "YT");
            this.AddCountry("Africa", "Other", "Reunion", "RE");
            this.AddCountry("Africa", "Other", "Seychelles", "SC");
            this.AddCountry("Africa", "Northern", "Algeria", "DZ");
            this.AddCountry("Africa", "Northern", "Egypt", "EG");
            this.AddCountry("Africa", "Northern", "Libya", "LY");
            this.AddCountry("Africa", "Northern", "Morocco", "MA");
            this.AddCountry("Africa", "Northern", "Sudan", "SD");
            this.AddCountry("Africa", "Northern", "Tunisia", "TN");
            this.AddCountry("Africa", "Northern", "Western Sahara", "EH");
            this.AddCountry("Africa", "Southern", "Angola", "AO");
            this.AddCountry("Africa", "Southern", "Botswana", "BW");
            this.AddCountry("Africa", "Southern", "Lesotho", "LS");
            this.AddCountry("Africa", "Southern", "Malawi", "MW");
            this.AddCountry("Africa", "Southern", "Mozambique", "MZ");
            this.AddCountry("Africa", "Southern", "Namibia", "NA");
            this.AddCountry("Africa", "Southern", "South Africa", "ZA");
            this.AddCountry("Africa", "Southern", "Swaziland", "SZ");
            this.AddCountry("Africa", "Southern", "Zambia", "ZM");
            this.AddCountry("Africa", "Southern", "Zimbabwe", "ZW");
            this.AddCountry("Africa", "Western", "Benin", "BJ");
            this.AddCountry("Africa", "Western", "Burkina Faso", "BF");
            this.AddCountry("Africa", "Western", "Cameroon", "CM");
            this.AddCountry("Africa", "Western", "Cape Verde", "CV");
            this.AddCountry("Africa", "Western", "Cote d'Ivoire", "CI");
            this.AddCountry("Africa", "Western", "Equatorial Guinea", "GQ");
            this.AddCountry("Africa", "Western", "Gabon", "GA");
            this.AddCountry("Africa", "Western", "Gambia, The", "GM");
            this.AddCountry("Africa", "Western", "Ghana", "GH");
            this.AddCountry("Africa", "Western", "Guinea", "GN");
            this.AddCountry("Africa", "Western", "Guinea-Bissau", "GW");
            this.AddCountry("Africa", "Western", "Liberia", "LR");
            this.AddCountry("Africa", "Western", "Mali", "ML");
            this.AddCountry("Africa", "Western", "Mauritania", "MR");
            this.AddCountry("Africa", "Western", "Niger", "NE");
            this.AddCountry("Africa", "Western", "Nigeria", "NG");
            this.AddCountry("Africa", "Western", "Sao Tome and Principe", "ST");
            this.AddCountry("Africa", "Western", "Senegal", "SN");
            this.AddCountry("Africa", "Western", "Sierra Leone", "SL");
            this.AddCountry("Africa", "Western", "Togo", "TG");
            this.AddCountry("America", "Central", "Belize", "BZ");
            this.AddCountry("America", "Central", "Costa Rica", "CR");
            this.AddCountry("America", "Central", "El Salvador", "SV");
            this.AddCountry("America", "Central", "Guatemala", "GT");
            this.AddCountry("America", "Central", "Honduras", "HN");
            this.AddCountry("America", "Central", "Mexico", "MX");
            this.AddCountry("America", "Central", "Nicaragua", "NI");
            this.AddCountry("America", "Central", "Panama", "PA");
            this.AddCountry("America", "North", "Canada", "CA");
            this.AddCountry("America", "North", "Greenland", "GL");
            this.AddCountry("America", "North", "Saint-Pierre et Miquelon", "PM");
            this.AddCountry("America", "North", "United States", "US");
            this.AddCountry("America", "South", "Argentina", "AR");
            this.AddCountry("America", "South", "Bolivia", "BO");
            this.AddCountry("America", "South", "Brazil", "BR");
            this.AddCountry("America", "South", "Chile", "CL");
            this.AddCountry("America", "South", "Colombia", "CO");
            this.AddCountry("America", "South", "Ecuador", "EC");
            this.AddCountry("America", "South", "Falkland Islands", "FK");
            this.AddCountry("America", "South", "French Guiana", "GF");
            this.AddCountry("America", "South", "Guyana", "GY");
            this.AddCountry("America", "South", "Paraguay", "PY");
            this.AddCountry("America", "South", "Peru", "PE");
            this.AddCountry("America", "South", "Suriname", "SR");
            this.AddCountry("America", "South", "Uruguay", "UY");
            this.AddCountry("America", "South", "Venezuela", "VE");
            this.AddCountry("America", "West Indies", "Anguilla", "AI");
            this.AddCountry("America", "West Indies", "Antigua&Barbuda", "AG");
            this.AddCountry("America", "West Indies", "Aruba", "AW");
            this.AddCountry("America", "West Indies", "Bahamas, The", "BS");
            this.AddCountry("America", "West Indies", "Barbados", "BB");
            this.AddCountry("America", "West Indies", "Bermuda", "BM");
            this.AddCountry("America", "West Indies", "British Virgin Islands", "VG");
            this.AddCountry("America", "West Indies", "Cayman Islands", "KY");
            this.AddCountry("America", "West Indies", "Cuba", "CU");
            this.AddCountry("America", "West Indies", "Dominica", "DM");
            this.AddCountry("America", "West Indies", "Dominican Republic", "DO");
            this.AddCountry("America", "West Indies", "Grenada", "GD");
            this.AddCountry("America", "West Indies", "Guadeloupe", "GP");
            this.AddCountry("America", "West Indies", "Haiti", "HT");
            this.AddCountry("America", "West Indies", "Jamaica", "JM");
            this.AddCountry("America", "West Indies", "Martinique", "MQ");
            this.AddCountry("America", "West Indies", "Montserrat", "MS");
            this.AddCountry("America", "West Indies", "Netherlands Antilles", "AN");
            this.AddCountry("America", "West Indies", "Puerto Rico", "PR");
            this.AddCountry("America", "West Indies", "Saint Kitts and Nevis", "KN");
            this.AddCountry("America", "West Indies", "Saint Lucia", "LC");
            this.AddCountry("America", "West Indies", "Saint Vincent and the Grenadines", "VC");
            this.AddCountry("America", "West Indies", "Trinidad and Tobago", "TT");
            this.AddCountry("America", "West Indies", "Turks and Caicos Islands", "TC");
            this.AddCountry("America", "West Indies", "Virgin Islands", "VI");
            this.AddCountry("Asia", "Central", "Kazakhstan", "KZ");
            this.AddCountry("Asia", "Central", "Kyrgyzstan", "KG");
            this.AddCountry("Asia", "Central", "Tajikistan", "TJ");
            this.AddCountry("Asia", "Central", "Turkmenistan", "TM");
            this.AddCountry("Asia", "Central", "Uzbekistan", "UZ");
            this.AddCountry("Asia", "East", "China", "CN");
            this.AddCountry("Asia", "East", "Japan", "JP");
            this.AddCountry("Asia", "East", "Korea, North", "KP");
            this.AddCountry("Asia", "East", "Korea, South", "KR");
            this.AddCountry("Asia", "East", "Taiwan", "TW");
            this.AddCountry("Asia", "Northern", "Mongolia", "MN");
            this.AddCountry("Asia", "Northern", "Russia", "RU");
            this.AddCountry("Asia", "South", "Afghanistan", "AF");
            this.AddCountry("Asia", "South", "Bangladesh", "BD");
            this.AddCountry("Asia", "South", "Bhutan", "BT");
            this.AddCountry("Asia", "South", "India", "IN");
            this.AddCountry("Asia", "South", "Maldives", "MV");
            this.AddCountry("Asia", "South", "Nepal", "NP");
            this.AddCountry("Asia", "South", "Pakistan", "PK");
            this.AddCountry("Asia", "South", "Sri Lanka", "LK");
            this.AddCountry("Asia", "South East", "Brunei", "BN");
            this.AddCountry("Asia", "South East", "Cambodia", "KH");
            this.AddCountry("Asia", "South East", "Christmas Island", "CX");
            this.AddCountry("Asia", "South East", "Cocos (Keeling) Islands", "CC");
            this.AddCountry("Asia", "South East", "Indonesia", "ID");
            this.AddCountry("Asia", "South East", "Laos", "LA");
            this.AddCountry("Asia", "South East", "Malaysia", "MY");
            this.AddCountry("Asia", "South East", "Myanmar (Burma)", "MM");
            this.AddCountry("Asia", "South East", "Philippines", "PH");
            this.AddCountry("Asia", "South East", "Singapore", "SG");
            this.AddCountry("Asia", "South East", "Thailand", "TH");
            this.AddCountry("Asia", "South East", "Vietnam", "VN");
            this.AddCountry("Asia", "South West", "Armenia", "AM");
            this.AddCountry("Asia", "South West", "Azerbaijan", "AZ");
            this.AddCountry("Asia", "South West", "Bahrain", "BH");
            this.AddCountry("Asia", "South West", "Cyprus", "CY");
            this.AddCountry("Asia", "South West", "Georgia", "GE");
            this.AddCountry("Asia", "South West", "Iran", "IR");
            this.AddCountry("Asia", "South West", "Iraq", "IQ");
            this.AddCountry("Asia", "South West", "Israel", "IL");
            this.AddCountry("Asia", "South West", "Jordan", "JO");
            this.AddCountry("Asia", "South West", "Kuwait", "KW");
            this.AddCountry("Asia", "South West", "Lebanon", "LB");
            this.AddCountry("Asia", "South West", "Oman", "OM");
            this.AddCountry("Asia", "South West", "Qatar", "QA");
            this.AddCountry("Asia", "South West", "Saudi Arabia", "SA");
            this.AddCountry("Asia", "South West", "Syria", "SY");
            this.AddCountry("Asia", "South West", "Turkey", "TR");
            this.AddCountry("Asia", "South West", "United Arab Emirates", "AE");
            this.AddCountry("Asia", "South West", "Yemen", "YE");
            this.AddCountry("Europe", "Central", "Austria", "AT");
            this.AddCountry("Europe", "Central", "Czech Republic", "CZ");
            this.AddCountry("Europe", "Central", "Hungary", "HU");
            this.AddCountry("Europe", "Central", "Liechtenstein", "LI");
            this.AddCountry("Europe", "Central", "Slovakia", "SK");
            this.AddCountry("Europe", "Central", "Switzerland", "CH");
            this.AddCountry("Europe", "Eastern", "Belarus", "BY");
            this.AddCountry("Europe", "Eastern", "Estonia", "EE");
            this.AddCountry("Europe", "Eastern", "Latvia", "LV");
            this.AddCountry("Europe", "Eastern", "Lithuania", "LT");
            this.AddCountry("Europe", "Eastern", "Moldova", "MD");
            this.AddCountry("Europe", "Eastern", "Poland", "PL");
            this.AddCountry("Europe", "Eastern", "Ukraine", "UA");
            this.AddCountry("Europe", "Northern", "Denmark", "DK");
            this.AddCountry("Europe", "Northern", "Faroe Islands", "FO");
            this.AddCountry("Europe", "Northern", "Finland", "FI");
            this.AddCountry("Europe", "Northern", "Iceland", "IS");
            this.AddCountry("Europe", "Northern", "Norway", "NO");
            this.AddCountry("Europe", "Northern", "Svalbard", "SJ");
            this.AddCountry("Europe", "Northern", "Sweden", "SE");
            this.AddCountry("Europe", "South East", "Albania", "AL");
            this.AddCountry("Europe", "South East", "Bosnia Herzegovina", "BA");
            this.AddCountry("Europe", "South East", "Bulgaria", "BG");
            this.AddCountry("Europe", "South East", "Croatia", "HR");
            this.AddCountry("Europe", "South East", "Greece", "GR");
            this.AddCountry("Europe", "South East", "Macedonia", "MK");
            this.AddCountry("Europe", "South East", "Romania", "RO");
            this.AddCountry("Europe", "South East", "Slovenia", "SI");
            this.AddCountry("Europe", "South West", "Andorra", "AD");
            this.AddCountry("Europe", "South West", "Gibraltar", "GI");
            this.AddCountry("Europe", "South West", "Portugal", "PT");
            this.AddCountry("Europe", "South West", "Spain", "ES");
            this.AddCountry("Europe", "Southern", "Vatican", "VA");
            this.AddCountry("Europe", "Southern", "Italy", "IT");
            this.AddCountry("Europe", "Southern", "Malta", "MT");
            this.AddCountry("Europe", "Southern", "San Marino", "SM");
            this.AddCountry("Europe", "Western", "Belgium", "BE");
            this.AddCountry("Europe", "Western", "France", "FR");
            this.AddCountry("Europe", "Western", "Germany", "DE");
            this.AddCountry("Europe", "Western", "Ireland", "IE");
            this.AddCountry("Europe", "Western", "Luxembourg", "LU");
            this.AddCountry("Europe", "Western", "Monaco", "MC");
            this.AddCountry("Europe", "Western", "Netherlands", "NL");
            this.AddCountry("Europe", "Western", "United Kingdom", "GB");
            this.AddCountry("Oceania", "Pacific", "American Samoa", "AS");
            this.AddCountry("Oceania", "Pacific", "Australia", "AU");
            this.AddCountry("Oceania", "Pacific", "Cook Islands", "CK");
            this.AddCountry("Oceania", "Pacific", "Fiji", "FJ");
            this.AddCountry("Oceania", "Pacific", "French Polynesia", "PF");
            this.AddCountry("Oceania", "Pacific", "Guam", "GU");
            this.AddCountry("Oceania", "Pacific", "Kiribati", "KI");
            this.AddCountry("Oceania", "Pacific", "Marshall Islands", "MH");
            this.AddCountry("Oceania", "Pacific", "Micronesia", "FM");
            this.AddCountry("Oceania", "Pacific", "Nauru", "NR");
            this.AddCountry("Oceania", "Pacific", "New Caledonia", "NC");
            this.AddCountry("Oceania", "Pacific", "New Zealand", "NZ");
            this.AddCountry("Oceania", "Pacific", "Niue", "NU");
            this.AddCountry("Oceania", "Pacific", "Norfolk Island", "NF");
            this.AddCountry("Oceania", "Pacific", "Northern Mariana Islands", "MP");
            this.AddCountry("Oceania", "Pacific", "Palau", "PW");
            this.AddCountry("Oceania", "Pacific", "Papua New-Guinea", "PG");
            this.AddCountry("Oceania", "Pacific", "Pitcairn Islands", "PN");
            this.AddCountry("Oceania", "Pacific", "Solomon Islands", "SB");
            this.AddCountry("Oceania", "Pacific", "Tokelau", "TK");
            this.AddCountry("Oceania", "Pacific", "Tonga", "TO");
            this.AddCountry("Oceania", "Pacific", "Tuvalu", "TV");
            this.AddCountry("Oceania", "Pacific", "Vanuatu", "VU");
            this.AddCountry("Oceania", "Pacific", "Wallis & Futuna", "WF");
            this.AddCountry("Oceania", "Pacific", "Western Samoa", "WS");
        }

        public void LoadIpCountryTable(string name)
        {
            FileStream stream = new FileStream(name, FileMode.Open);
            StreamReader nccin = new StreamReader(stream);
            this.Ip2Country.Load(nccin);
            nccin.Close();
            stream.Close();
        }

        public System.Collections.ArrayList LoadIpList(string name)
        {
            string str;
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            FileStream stream = new FileStream(name, FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            while ((str = reader.ReadLine()) != null)
            {
                list.Add(str);
            }
            reader.Close();
            stream.Close();
            return list;
        }

        public void MainIPS()
        {
            AfterDelay(500, () =>
            {
                this.Countries();
                GC.GetTotalMemory(true);
                DateTime now = DateTime.Now;
                this.LoadIpCountryTable(@"scripts\\" + "SinAdmin\\dbip.txt");
                DateTime time2 = DateTime.Now;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.GetTotalMemory(true);
            });
        }

        public class BitVector
        {
            public byte[] m_data;
            public int m_maxoffset;

            public BitVector()
            {
                this.m_data = new byte[0];
                this.m_maxoffset = -1;
            }

            public BitVector(byte[] data)
            {
                this.m_data = new byte[0];
                this.m_maxoffset = -1;
                this.m_data = new byte[data.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    this.m_data[i] = data[i];
                }
                this.m_maxoffset = (this.m_data.Length * 8) - 1;
            }

            public BitVector(long val, int length)
            {
                this.m_data = new byte[0];
                this.m_maxoffset = -1;
                this.AddData(val, length);
            }

            public void AddAscii(string val)
            {
                for (int i = 0; i < val.Length; i++)
                {
                    this.AddData((long)val[i], 8);
                }
            }

            public void AddData(long val, int length)
            {
                int num = this.m_maxoffset + 1;
                for (int i = 0; i < length; i++)
                {
                    this.Set(num + i, (val & (((int)1) << ((length - i) - 1))) != 0L);
                }
            }

            public bool Get(int offset)
            {
                if (offset > m_maxoffset)
                {
                    throw new Exception("OutOfBound offset " + offset);
                }
                int index = offset / 8;
                int num2 = offset % 8;
                if (index >= m_data.Length)
                {
                    throw new Exception("OutOfBound offset " + offset);
                }
                return ((this.m_data[index] & (((int)1) << (7 - num2))) != 0);
            }

            public byte[] GetByteArray()
            {
                byte[] buffer = new byte[this.m_data.Length];
                for (int i = 0; i < this.m_data.Length; i++)
                {
                    buffer[i] = this.m_data[i];
                }
                return buffer;
            }

            public int LongestCommonPrefix(BitVector other)
            {
                int offset = 0;
                while (((offset <= other.m_maxoffset) && (offset <= this.m_maxoffset)) && (other.Get(offset) == this.Get(offset)))
                {
                    offset++;
                }
                return offset;
            }

            public BitVector Range(int start, int length)
            {
                BitVector vector = new BitVector();
                for (int i = start; i < (start + length); i++)
                {
                    vector.Set(i - start, this.Get(i));
                }
                return vector;
            }

            public void Set(int offset, bool val)
            {
                int index = offset / 8;
                int num2 = offset % 8;
                if (index >= this.m_data.Length)
                {
                    byte[] buffer = new byte[index + 1];
                    for (int i = 0; i < this.m_data.Length; i++)
                    {
                        buffer[i] = this.m_data[i];
                    }
                    this.m_data = buffer;
                    this.m_maxoffset = offset;
                }
                else if (offset > this.m_maxoffset)
                {
                    this.m_maxoffset = offset;
                }
                if (val)
                {
                    this.m_data[index] = (byte)(this.m_data[index] | ((byte)(((int)1) << (7 - num2))));
                }
                else
                {
                    this.m_data[index] = (byte)(this.m_data[index] & ((byte)(0xff - (((int)1) << (7 - num2)))));
                }
            }

            public override string ToString()
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i <= this.m_maxoffset; i++)
                {
                    builder.Append(this.Get(i) ? "1" : "0");
                    if ((i % 8) == 7)
                    {
                        builder.Append(" ");
                    }
                }
                return builder.ToString();
            }

            public int Length
            {
                get
                {
                    return (this.m_maxoffset + 1);
                }
            }
        }

        public class BitVectorReader
        {
            public BitVector m_data;
            public int m_offset;

            public BitVectorReader(BitVector v)
            {
                this.m_data = v;
                this.m_offset = 0;
            }

            public bool HasMoreData()
            {
                return (this.m_offset < this.m_data.Length);
            }

            public string ReadAscii(int length)
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < length; i++)
                {
                    builder.Append((char)this.ReadByte());
                }
                return builder.ToString();
            }

            public byte ReadByte()
            {
                byte num = 0;
                int num2 = this.m_offset + 8;
                for (int i = 0; (this.m_offset < num2) && (this.m_offset < this.m_data.Length); i++)
                {
                    if (this.m_data.Get(this.m_offset))
                    {
                        num = (byte)(num | ((byte)(((int)1) << (7 - i))));
                    }
                    this.m_offset++;
                }
                return num;
            }

            public short ReadInt16()
            {
                int num = 0;
                int num2 = this.m_offset + 0x10;
                for (int i = 0; (this.m_offset < num2) && (this.m_offset < this.m_data.Length); i++)
                {
                    if (this.m_data.Get(this.m_offset))
                    {
                        num |= ((int)1) << (15 - i);
                    }
                    this.m_offset++;
                }
                return (short)num;
            }

            public int ReadInt32()
            {
                int num = 0;
                int num2 = this.m_offset + 0x20;
                for (int i = 0; (this.m_offset < num2) && (this.m_offset < this.m_data.Length); i++)
                {
                    if (this.m_data.Get(this.m_offset))
                    {
                        num |= ((int)1) << (0x1f - i);
                    }
                    this.m_offset++;
                }
                return num;
            }
        }

        public class BitVectorTrie
        {
            public Node Root = new Node();

            public void Add(BitVector key, object data)
            {
                this.Add(this.Root, key, data);
            }

            public void Add(Node n, BitVector key, object data)
            {
                if (n.Key == null)
                {
                    this.AddAsChildren(n, key, data);
                }
                else
                {
                    int start = key.LongestCommonPrefix(n.Key);
                    Debug.Assert(start != 0);
                    if (start == n.Key.Length)
                    {
                        key = key.Range(start, key.Length - start);
                        this.AddAsChildren(n, key, data);
                    }
                    else
                    {
                        BitVector vector = n.Key.Range(0, start);
                        Node node3 = new Node
                        {
                            Key = n.Key.Range(start, n.Key.Length - start),
                            Data = n.Data,
                            Children = n.Children
                        };
                        Node node = node3;
                        Node node4 = new Node
                        {
                            Key = key.Range(start, key.Length - start),
                            Data = data
                        };
                        Node node2 = node4;
                        n.Key = vector;
                        n.Data = null;
                        n.Children = new System.Collections.ArrayList();
                        n.Children.Add(node);
                        n.Children.Add(node2);
                    }
                }
            }

            public void AddAsChildren(Node n, BitVector key, object data)
            {
                if (n.Children == null)
                {
                    n.Children = new System.Collections.ArrayList();
                    Node node2 = new Node
                    {
                        Key = key,
                        Data = data
                    };
                    Node node = node2;
                    n.Children.Add(node);
                }
                else
                {
                    int num = -1;
                    int num2 = 0;
                    for (int i = 0; i < n.Children.Count; i++)
                    {
                        int num4 = ((Node)n.Children[i]).Key.LongestCommonPrefix(key);
                        if (num4 > num2)
                        {
                            num2 = num4;
                            num = i;
                        }
                    }
                    if (num == -1)
                    {
                        Node node4 = new Node
                        {
                            Key = key,
                            Data = data
                        };
                        Node node3 = node4;
                        n.Children.Add(node3);
                    }
                    else
                    {
                        this.Add((Node)n.Children[num], key, data);
                    }
                }
            }

            public void DisplayAsTree(Node n, int offset)
            {
                for (int i = 0; i < offset; i++)
                {
                    Console.Write("   ");
                }
                Console.WriteLine("<{0}> = <{1}>", n.Key, n.Data);
                if (n.Children != null)
                {
                    foreach (Node node in n.Children)
                    {
                        this.DisplayAsTree(node, offset + 1);
                    }
                }
            }

            public object Get(BitVector key)
            {
                Node root = this.Root;
                while (root != null)
                {
                    if (root.Children != null)
                    {
                        int num = -1;
                        int start = 0;
                        for (int i = 0; i < root.Children.Count; i++)
                        {
                            int num4 = ((Node)root.Children[i]).Key.LongestCommonPrefix(key);
                            if (num4 > start)
                            {
                                start = num4;
                                num = i;
                            }
                        }
                        if (num != -1)
                        {
                            key = key.Range(start, key.Length - start);
                            root = (Node)root.Children[num];
                            if (key.Length == 0)
                            {
                                return root.Data;
                            }
                            continue;
                        }
                    }
                    return null;
                }
                return null;
            }

            public object GetBest(BitVector key)
            {
                Node root = this.Root;
                while (root != null)
                {
                    if (root.Children != null)
                    {
                        int num = -1;
                        int start = 0;
                        for (int i = 0; i < root.Children.Count; i++)
                        {
                            int num4 = ((Node)root.Children[i]).Key.LongestCommonPrefix(key);
                            if (num4 > start)
                            {
                                start = num4;
                                num = i;
                            }
                        }
                        if (num != -1)
                        {
                            key = key.Range(start, key.Length - start);
                            root = (Node)root.Children[num];
                            if (key.Length == 0)
                            {
                                return root.Data;
                            }
                            continue;
                        }
                    }
                    return root.Data;
                }
                return null;
            }

            public class Node
            {
                public System.Collections.ArrayList Children;
                public object Data;
                public BitVector Key;
            }
        }

        public class Continent
        {
            public Hashtable Regions = new Hashtable();
        }

        public class IPToCountry
        {
            public BitVectorTrie m_trie = new BitVectorTrie();
            public static int NetworkCodeCount;

            public void AddIp(string ip, string country)
            {
                BitVector key = this.IpToBitVector(ip);
                this.m_trie.Add(key, string.Intern(country.ToUpper()));
            }

            public string GetCountry(string ip)
            {
                BitVector key = this.IpToBitVector(ip);
                return (string)this.m_trie.GetBest(key);
            }

            public BitVector IpToBitVector(string ip)
            {
                string[] strArray = ip.Split(new char[] { '.' });
                BitVector vector = new BitVector();
                foreach (string str in strArray)
                {
                    int num = int.Parse(str);
                    vector.AddData((long)num, 8);
                }
                return vector;
            }

            public void Load(StreamReader nccin)
            {
                try
                {
                    string str;
                    char[] separator = new char[] { '|' };
                    while ((str = nccin.ReadLine()) != null)
                    {
                        string[] strArray = str.Split(separator);
                        if (((strArray.Length > 1) && (strArray[2].Length == 2)) && (strArray[0].IndexOf('.') >= 0))
                        {
                            this.AddIp(strArray[0], strArray[2]);
                            NetworkCodeCount++;
                        }
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    nccin.Close();
                }
            }

            public void Load(string filename)
            {
                StreamReader nccin = new StreamReader(filename);
                this.Load(nccin);
            }
        }

        public class Region
        {
            public Hashtable CountryNames = new Hashtable();
        }
        #endregion
    }
}
