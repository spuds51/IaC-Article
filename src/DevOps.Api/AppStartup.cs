using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Xerris.DotNet.Core;
using Xerris.DotNet.Core.Aws.IoC;
using Xerris.DotNet.Core.Cache;

namespace Sizing.Poker.Api
{
    public class AppStartup : IAppStartup
    {
        public IConfiguration StartUp(IServiceCollection collection)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
            
            Log.Information(Banner.Next());
            
            var builder = new ApplicationConfigurationBuilder<ApplicationConfig>();
            var appConfig = builder.Build();
            collection.AddSingleton<IApplicationConfig>(appConfig);
            collection.AddDefaultAWSOptions(appConfig.AwsOptions);
            
            collection.AddSecretProvider(appConfig.SecretConfigurations);
            collection.AddSingleton<ICache>(new WaitToFinishMemoryCache(2,10));
            collection.AutoRegister(GetType().Assembly);
            
            return builder.Configuration;
        }
    }
    
    public static class Banner
    {
        private static readonly List<string> All = new List<string>
        {
            Aardvark, Bat, Bear, Bird, Bison, Camel, Cat, Clown, Dog, Bunny, Rhino, Spider, Unicorn, Whinnie, Southpark, Hobbes, Whale
        };
        
        public static string Next()
        {
            var rnd = new Random();
            var next = rnd.Next(0, All.Count);
            return All[next];
        }
        
        #region creatures
        private const string Aardvark = @"
                   ,
             (`.  : \               __..----..__
              `.`.| |:          _,-':::''' '  `:`-._
                `.:\||       _,':::::'         `::::`-.
                  \\`|    _,':::::::'     `:.     `':::`.
                   ;` `-''  `::::::.                  `::\
                ,-'      .::'  `:::::.         `::..    `:\
              ,' /_) -.            `::.           `:.     |
            ,'.:     `    `:.        `:.     .::.          \
       __,-'   ___,..-''-.  `:.        `.   /::::.         |
      |):'_,--'           `.    `::..       |::::::.      ::\
       `-'                 |`--.:_::::|_____\::::::::.__  ::|
                           |   _/|::::|      \::::::|::/\  :|
                           /:./  |:::/        \__:::):/  \  :\
                         ,'::'  /:::|        ,'::::/_/    `. ``-.__
           jrei         ''''   (//|/\      ,';':,-'         `-.__  `'--..__
                                                                 `''---::::'";
        private const string Bat = @"
  #
   ##
   ###
    ####
     #####
     #######
      #######
      ########
      ########
      #########
      ##########
     ############
   ##############
  ################
   ################
     ##############
      ##############                                              ####
      ##############                                           #####
       ##############                                      #######
       ##############                                 ###########
       ###############                              #############
       ################                           ##############
      #################      #                  ################
      ##################     ##    #           #################
     ####################   ###   ##          #################
          ################  ########          #################
           ################  #######         ###################
             #######################       #####################
              #####################       ###################
                ############################################
                 ###########################################
                 ##########################################
                  ########################################
                  ########################################
                   ######################################
                   ######################################
                    ##########################      #####
                    ###  ###################           ##
                    ##    ###############
                    #     ##  ##########   dD
                              ##    ###
                                    ###
          The Dutch Dude            ##
          T.vanEck@fontys.nl        #";
        private const string Bear = @"
        (()__(()
        /       \ 
       ( /    \  \
        \ o o    /
        (_()_)__/ \             
       / _,==.____ \
      (   |--|      )
      /\_.|__|'-.__/\_
     / (        /     \ 
     \  \      (      /
      )  '._____)    /    
   (((____.--(((____/mrf";
        private const string Bird = @"
           _ _.-''''''--._
         .` `.  ...------.\     /'''''''''''\ 
        / |O :-`   _,.,---'  ---|  DERP !!  |
       '  \   ;--''             \,,,,,,,,,,,/
       | _.' (
       ,' _,' `-.
       : /       '.
       \ \         '
        `.|         `.
          `-._        \
              '.  ,-.  \
  .__          _`.\..`\ \
  ,  ''- . _,-'.,-'  ``: \
  '-...._ (( (('-.._    \ \
         `--..      `'-. \ \
              `..     '   \ \
                 `\ \fsr   `-
                   \/ ";
        private const string Bison = @"
                                    ___,,___
                                ,d8888888888b,_
                            _,d889'        8888b,
                        _,d8888'          8888888b,
                    _,d8889'           888888888888b,_
                _,d8889'             888888889'688888, /b
            _,d8889'               88888889'     `6888d 6,_
         ,d88886'              _d888889'           ,8d  b888b,  d\
       ,d889'888,             d8889'               8d   9888888Y  )
     ,d889'   `88,          ,d88'                 d8    `,88aa88 9
    d889'      `88,        ,88'                   `8b     )88a88'
   d88'         `88       ,88                   88 `8b,_ d888888
  d89            88,      88                  d888b  `88`_  8888
  88             88b      88                 d888888 8: (6`) 88')
  88             8888b,   88                d888aaa8888, `   'Y'
  88b          ,888888888888                 `d88aa `88888b ,d8
  `88b       ,88886 `88888888                 d88a  d8a88` `8/
   `q8b    ,88'`888  `888''`88          d8b d8888,` 88/ 9)_6
        88  ,88'   `88  88p    `88        d88888888888bd8( Z~/
        88b 8p      88 68'      `88      88888888' `688889`
        `88 8        `8 8,       `88    888 `8888,   `qp'
        8 8,        `q 8b       `88  88'    `888b
        q8 8b        888        `8888'
         888                     `q88b
         888'";
        private const string Camel = @"
                     .--.      .'  `.
                   .' . :\    /   :  L
                   F     :\  /   . : |        .-._
                  /     :  \/        J      .' ___\
                 J     :   /      : : L    /--'   ``.
                 F      : J           |  .<'.o.  `-'>
                /        J             L \_>.   .--w)
               J        /              \_/|   . `-__|
               F                        / `    -' /|)
              |   :                    J   '        |
             .'   ':                   |    .    :  \
            /                          J      :     |L
           F                              |     \   ||
          F .                             |   :      |
         F  |                             ; .   :  : F
        /   |                                     : J
       J    J             )                ;        F
       |     L           /      .:'                J
    .-'F:     L        ./       :: :       .       F
    `-'F:     .\    `:.J         :::.             J
      J       ::\    `:|        |::::\            |
      J        |:`.    J        :`:::\            F
       L   :':/ \ `-`.  \       : `:::|        .-'
       |     /   L    >--\         :::|`.    .-'
       J    J    |    |   L     .  :::: :`, /
        L   F    J    )   |        >::   : /
        |  J      L   F   \     .-.:'   . /
        ): |     J   /     `-   | |   .--'
        /  |     |: J        L  J J   )
        L  |     |: |        L   F|   /
        \: J     \:  L       \  /  L |
         L |      \  |        F|   | )
         J F       \ J       J |   |J
          L|        \ \      | |   | L
          J L        \ \     F \   F |
           L\         \ \   J   | J   L
          /__\_________)_`._)_  |_/   \_____";
        private const string Cat = @"
                      _                        
                      \`*-.                    
                       )  _`-.                 
                      .  : `. .                
                      : _   '  \               
                      ; *` _.   `*-._          
                      `-.-'          `-.       
                        ;       `       `.     
                        :.       .        \    
                        . \  .   :   .-'   .   
                        '  `+.;  ;  '      :   
                        :  '  |    ;       ;-. 
                        ; '   : :`-:     _.`* ;
               [bug] .*' /  .*' ; .*`- +'  `*' 
                     `*-*   `*-*  `*-*'        ";
        private const string Dog = @"
           ____,'`-, 
      _,--'   ,/::.; 
   ,-'       ,/::,' `---.___        ___,_ 
   |       ,:';:/        ;''`;``--./ ,-^.;--. 
   |:     ,:';,'         '         `.   ;`   `-. 
    \:.,:::/;/ -:.                   `  | `     `-. 
     \:::,'//__.;  ,;  ,  ,  :.`-.   :. |  ;       :. 
      \,',';/O)^. :'  ;  :   '__` `  :::`.       .:' ) 
      |,'  |\__,: ;      ;  '/O)`.   :::`;       ' ,' 
           |`--''            \__,' , ::::(       ,' 
           `    ,            `--' ,: :::,'\   ,-' 
            | ,;         ,    ,::'  ,:::   |,' 
            |,:        .(          ,:::|   ` 
            ::'_   _   ::         ,::/:| 
           ,',' `-' \   `.      ,:::/,:| 
          | : _ _   |   '     ,::,' ::: 
          | \ O`'O  ,',   ,    :,'   ;:: 
           \ `-'`--',:' ,' , ,,'      :: 
            ``:.:.__   ',-','        ::' 
    -hrr-      `--.__, ,::.         ::' 
                   |:  ::::.       ::' 
                   |:  ::::::    ,::' ";
        private const string Bunny = @"
               (`.         ,-,
               `\ `.    ,;' /
                \`. \ ,'/ .'
          __     `.\ Y /.'
       .-'  ''--.._` ` (
     .'            /   `
    ,           ` '   Q '
    ,         ,   `._    \
    |         '     `-.;_'
    `  ;    `  ` --,.._;
    `    ,   )   .'
     `._ ,  '   /_
        ; ,''-,;' ``-
         ``-..__\``--` ";
        private const string Rhino = @"
                                           ,-.             __
                                         ,'   `---.___.---'  `.
                                       ,'   ,-                 `-._
                                     ,'    /                       \
                                  ,\/     /                        \\
                              )`._)>)     |                         \\
                              `>,'    _   \                  /       ||
                                )      \   |   |            |        |\\
                       .   ,   /        \  |    `.          |        | ))
                       \`. \`-'          )-|      `.        |        /((
                        \ `-`   .`     _/  \ _     )`-.___.--\      /  `'
                         `._         ,'     `j`.__/           `.    \
                           / ,    ,'         \   /`             \   /
                           \__   /           _) (               _) (
                             `--'           /____\             /____\ ";
        private const string Spider = @"
           ;               ,           
         ,;                 '.         
        ;:                   :;         
       ::                     ::       
       ::                     ::       
       ':                     :         
        :.                    :         
     ;' ::                   ::  '     
    .'  ';                   ;'  '.     
   ::    :;                 ;:    ::   
   ;      :;.             ,;:     ::   
   :;      :;:           ,;""      ::   
   ::.      ':;  ..,.;  ;:'     ,.;:   
    ""'""...   '::,::::: ;:   .;.;""""'     
        '""""""....;:::::;,;.;""""""         
    .:::.....'""':::::::'"",...;::::;.   
   ;:' '""""'"""";.,;:::::;.'""""""""""""  ':;   
  ::'         ;::;:::;::..         :;   
 ::         ,;:::::::::::;:..       :: 
 ;'     ,;;:;::::::::::::::;"";..    ':. 
::     ;:""  ::::::""""""'::::::  "":     :: 
 :.    ::   ::::::;  :::::::   :     ; 
  ;    ::   :::::::  :::::::   :    ;   
   '   ::   ::::::....:::::'  ,:   '   
    '  ::    :::::::::::::""   ::       
       ::     ':::::::::""'    ::       
       ':       """"""""""""""'      ::       
        ::                   ;:         
        ':;                 ;:""         
-hrr-     ';              ,;'           
            ""'           '""             
              ' ";
        private const string Whinnie = @"
                           <\              _
                            \\          _/{
                     _       \\       _-   -_
                   /{        / `\   _-     - -_
                 _~  =      ( @  \ -        -  -_
               _- -   ~-_   \( =\ \           -  -_
             _~  -       ~_ | 1 :\ \      _-~-_ -  -_
           _-   -          ~  |V: \ \  _-~     ~-_-  -_
        _-~   -            /  | :  \ \            ~-_- -_
     _-~    -   _.._      {   | : _-``               ~- _-_
  _-~   -__..--~    ~-_  {   : \:}
=~__.--~~              ~-_\  :  /
                           \ : /__
                          //`Y'--\\      =
                         <+       \\
                          \\      WWW
                          MMM";
        private const string Hobbes = @"
           ( ).---.( )
          ./.=""'""=.\.
           |=.     .=|
           |_  0 0  _|
          .`  .---.  '.
          :   `---'   :
          `._'-----'_.'
            _:-----:._
           /={     }=_\
          /_.{     }-_=\
          |=|{     }=|-|
          |=|{     }-|=|
           \|{     }_|/
            |{     } |
            |{     }=|
            |{     } =\
            |`.   .'=|`\
      jgs   |_=`|'`=_|`\`\    .'`.
          __|_=_|=___|  `\`\_/./`'
         (((__(((_____)   `.__/
";
        private const string Southpark = @"
            .- <O> -.        .-====-.      ,-------.      .-=<>=-.
           /_-\'''/-_\      / / '' \ \     |,-----.|     /__----__\
          |/  o) (o  \|    | | ')(' | |   /,'-----'.\   |/ (')(') \|
           \   ._.   /      \ \    / /   {_/(') (')\_}   \   __   /
           ,>-_,,,_-<.       >'=jf='<     `.   _   .'    ,'--__--'.
         /      .      \    /        \     /'-___-'\    /    :|    \
        (_)     .     (_)  /          \   /         \  (_)   :|   (_)
         \_-----'____--/  (_)        (_) (_)_______(_)   |___:|____|
          \___________/     |________|     \_______/     |_________|
";
private const string Unicorn = @"
                                                    /
                                                  .7
                                       \       , //
                                       |\.--._/|//
                                      /\ ) ) ).'/
                                     /(  \  // /
                                    /(   J`((_/ \
                                   / ) | _\     /
                                  /|)  \  eJ    L
                                 |  \ L \   L   L
                                /  \  J  `. J   L
                                |  )   L   \/   \
                               /  \    J   (\   /
             _....___         |  \      \   \```
      ,.._.-'        '''--...-||\     -. \   \
    .'.=.'                    `         `.\ [ Y
   /   /                                  \]  J
  Y / Y                                    Y   L
  | | |          \                         |   L
  | | |           Y                        A  J
  |   I           |                       /I\ /
  |    \          I             \        ( |]/|
  J     \         /._           /        -tI/ |
   L     )       /   /'-------'J           `'-:.
   J   .'      ,'  ,' ,     \   `'-.__          \
    \ T      ,'  ,'   )\    /|        ';'---7   /
     \|    ,'L  Y...-' / _.' /         \   /   /
      J   Y  |  J    .'-'   /         ,--.(   /
       L  |  J   L -'     .'         /  |    /\
       |  J.  L  J     .-;.-/       |    \ .' /
       J   L`-J   L____,.-'`        |  _.-'   |
        L  J   L  J                  ``  J    |
        J   L  |   L                     J    |
         L  J  L    \                    L    \
         |   L  ) _.'\                    ) _.'\
         L    \('`    \                  ('`    \
          ) _.'\`-....'                   `-....'
         ('`    \
          `-.___/
";
        private const string Clown = @"
                                  ,;;;;;;,
                                ,;;;'""""`;;\
                              ,;;;/  .'`',;\
                            ,;;;;/   |    \|_
                           /;;;;;    \    / .\
                         ,;;;;;;|     '.  \/_/
                        /;;;;;;;|       \
             _,.---._  /;;;;;;;;|        ;   _.---.,_
           .;;/      `.;;;;;;;;;|         ;'      \;;,
         .;;;/         `;;;;;;;;;.._    .'         \;;;.
        /;;;;|          _;-""`       `""-;_          |;;;;\
       |;;;;;|.---.   .'  __.-""```""-.__  '.   .---.|;;;;;|
       |;;;;;|     `\/  .'/__\     /__\'.  \/`     |;;;;;|
       |;;;;;|       |_/ //  \\   //  \\ \_|       |;;;;;|
       |;;;;;|       |/ |/    || ||    \| \|       |;;;;;|
        \;;;;|    __ || _  .-.\| |/.-.  _ || __    |;;;;/
         \jgs|   / _\|/ = /_o_\   /_o_\ = \|/_ \   |;;;/
          \;;/   |`.-     `   `   `   `     -.`|   \;;/
         _|;'    \ |    _     _   _     _    | /    ';|_
        / .\      \\_  ( '--'(     )'--' )  _//      /. \
        \/_/       \_/|  /_   |   |   _\  |\_/       \_\/
                      | /|\\  \   /  //|\ |
                      |  | \'._'-'_.'/ |  |
                      |  ;  '-.```.-'  ;  |
                      |   \    ```    /   |
    __                ;    '.-""""""""""-.'    ;                __
   /\ \_         __..--\     `-----'     /--..__         _/ /\
   \_'/\`''---''`..;;;;.'.__,       ,__.',;;;;..`''---''`/\'_/
        '-.__'';;;;;;;;;;;,,'._   _.',,;;;;;;;;;;;''__.-'
             ``''--; ;;;;;;;;..`""`..;;;;;;;; ;--''``   _
        .-.       /,;;;;;;;';;;;;;;;;';;;;;;;,\    _.-' `\
      .'  /_     /,;;;;;;'/| ;;;;;;; |\';;;;;;,\  `\     '-'|
     /      )   /,;;;;;',' | ;;;;;;; | ',';;;;;,\   \   .'-./
     `'-..-'   /,;;;;','   | ;;;;;;; |   ',';;;;,\   `""`
              | ;;;','     | ;;;;;;; |  ,  ', ;;;'|
             _\__.-'  .-.  ; ;;;;;;; ;  |'-. '-.__/_
            / .\     (   )  \';;;;;'/   |   |    /. \
            \/_/   (`     `) \';;;'/    '-._|    \_\/
                    '-/ \-'   '._.'         `
                      """"""      /.`\
                               \|_/
         ";
        private const string Whale = @"
     .-'
'--./ /     _.---.
'-,  (__..-`       \
   \          .     |
    `,.__.   ,__.--/
      '._/_.'___.-`
";
        #endregion
    }
}