using BotForgeAPI.Game.Map;
using BotForgeAPI.Game.Map.Actors;
using BotForgeAPI.Protocol.Types;

namespace Core.Core.Char
{
    public static class CharComplements
    {
        public static void SetStatus(this IPlayedCharacter character, Status status)
        {
            character.Status = status;
        }

        public static void ParseCharacterInformations(this IPlayedCharacter character, CharacterBaseInformations characterInformations)
        {
        }
    }
}
