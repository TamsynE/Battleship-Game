# Battleship-Game #

Many computer scientists write software that is able to play games without requiring human interaction. These programs, called bots, are certainly a big part of video games but they are also a major component in scientific and engineering simulations. In these simulation "games", the bot is used to model an autonomous agent that interacts with its environment and other bots. The bots might be used by a traffic engineer to simulate a busy roadway for analyzing traffic patterns. Or, the bots might be used by biologists to simulate the interaction of microbes in a petri dish or a herd of wildabeasts. 

I created a simple agent that autonomously plays a virtual version of the classic Battleship Board Game. The bot  was created as part of a "plugin" architecture by implementing an abstract class. My bot maintains its own internal representation of the game with a state machine and uses this the state to choose an optimal move. My bot was able to defeat the majority of competitors and fared well against the instructor's agent.
