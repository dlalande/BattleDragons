using Google.Protobuf.WellKnownTypes;
using System;
using System.Linq;

namespace Dragons.Core.Grpc
{
    public static class ConvertExtentions
    {
        public static Player ToPlayer(this Models.Player player)
        {
            return new Player
            {
                Name = player.Name,
                PlayerId = player.PlayerId,
                Type = player.Type.ToPlayerType()
            };
        }

        public static Models.Player ToPlayer(this Player player)
        {
            return new Models.Player(player.PlayerId, player.Name)
            {
                Type = player.Type.ToPlayerType()
            };
        }

        public static PlayerType ToPlayerType(this Types.PlayerType playerType)
        {
            return (PlayerType)(int)playerType;
        }

        public static Types.PlayerType ToPlayerType(this PlayerType playerType)
        {
            return (Types.PlayerType)(int)playerType;
        }

        public static PlayerTuple ToPlayerTuple(this Tuple<Models.Player, Models.Player> playerTuple)
        {
            return new PlayerTuple
            {
                Item1 = playerTuple.Item1.ToPlayer(),
                Item2 = playerTuple.Item2.ToPlayer()
            };
        }

        public static Reservation ToReservation(this Models.Reservation reservation)
        {
            return new Reservation()
            {
                Player = reservation.Player.ToPlayer(),
                Created = Timestamp.FromDateTime(reservation.Created)
            };
        }
        public static Models.Reservation ToReservation(this Reservation reservation)
        {
            try
            {
                return new Models.Reservation()
                {
                    Player = reservation.Player.ToPlayer(),
                    Created = reservation.Created?.ToDateTime() ?? DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public static Spell ToSpell(this Models.Spell spell)
        {
            if(spell == null)
                return null;

            return new Spell()
            {
                Description = spell.Description,
                ManaCost = spell.ManaCost,
                Type = spell.Type.ToSpellType()
            };
        }

        public static SpellType ToSpellType(this Types.SpellType SpellType)
        {
            return (SpellType)(int)SpellType;
        }

        public static Types.SpellType ToSpellType(this SpellType SpellType)
        {
            return (Types.SpellType)(int)SpellType;
        }

        public static Game ToGame(this Models.Game game)
        {
            var g = new Game()
            {
                Board = game.Board.ToGameBoard(),
                CanMove = game.CanMove,
                IsOver = game.IsOver,
                Mana = game.Mana,
                Name = game.Name,
                Opponent = game.Opponent,
            };
            g.Spells.AddRange(game.Spells.Select(s => s.ToSpell()));
            return g;
        }

        public static GameBoard ToGameBoard(this Models.GameBoard gameBoard)
        {
            var g = new GameBoard()
            {
                InitialSetup = gameBoard.InitialSetup.ToInitialSetup(),
            };
            foreach (var pieces in gameBoard.Pieces)
            {
                var list = new ListOfPieces();
                list.Items.AddRange(pieces.Select(p => p.ToPiece()));
                g.Pieces.Add(list);
            }
            return g;
        }

        public static InitialSetup ToInitialSetup(this Models.InitialSetup initialSetup)
        {
            var i = new InitialSetup
            {
                BoardSize = initialSetup.BoardSize
            };
            i.Dragons.AddRange(initialSetup.Dragons.Select(d => d.ToDragon()));
            i.AdditionalPieces.AddRange(initialSetup.AdditionalPieces.Select(p => p.ToPiece()));
            return i;
        }

        public static Dragon ToDragon(this Models.Dragon dragon)
        {
            var d = new Dragon();
            d.Pieces.AddRange(dragon.Select(p => p.ToPiece()));
            return d;
        }

        public static Piece ToPiece(this Models.Piece piece)
        {
            return new Piece()
            {
                Coordinate = new Coordinate() { X = piece.Coordinate.X, Y = piece.Coordinate.Y },
                Type = (PieceType)(int)piece.Type
            };
        }

        public static Models.Piece ToPiece(this Piece piece)
        {
            return new Models.Piece()
            {
                Coordinate = new Models.Coordinate() { X = piece.Coordinate.X, Y = piece.Coordinate.Y },
                Type = (Types.PieceType)(int)piece.Type
            };
        }

        public static GameStart ToGameStart(this Models.GameStart gameStart)
        {
            return new GameStart()
            {
                Player1 = gameStart.Player1.ToPlayer(),
                Player1Setup = gameStart.Player1Setup?.ToInitialSetup(),
                Player2 = gameStart.Player2.ToPlayer(),
                Player2Setup = gameStart.Player2Setup?.ToInitialSetup()
            };
        }

        public static Models.GameStart ToGameStart(this GameStart gameStart)
        {
            try
            {
                return new Models.GameStart()
                {
                    Player1 = gameStart.Player1.ToPlayer(),
                    Player1Setup = gameStart.Player1Setup.ToInitialSetup(),
                    Player2 = gameStart.Player2.ToPlayer(),
                    Player2Setup = gameStart.Player2Setup.ToInitialSetup()
                };
            }
            catch (Exception e)
            {
                throw; 
            }
        }

        public static Models.InitialSetup ToInitialSetup(this InitialSetup initialSetup)
        {
            if(initialSetup == null)
                return null;
            var i = new Models.InitialSetup
            {
                BoardSize = initialSetup.BoardSize,
                Dragons = initialSetup.Dragons.Select(d => d.ToDragon()).ToList(),
                AdditionalPieces = initialSetup.AdditionalPieces.Select(p => p.ToPiece()).ToList()

            };
            return i;
        }

        public static Models.Dragon ToDragon(this Dragon dragon)
        {
            return new Models.Dragon(dragon.Pieces.Select(p => p.ToPiece()));
        }

        public static Models.Move ToMove(this Move move)
        {
            return new Models.Move()
            {
                Coordinate = new Models.Coordinate() { X = move.Coordinate.X, Y = move.Coordinate.Y },
                Player = move.Player.ToPlayer(),
                Spell = move.Spell.ToSpell()
            };
        }

        public static Move ToMove(this Models.Move move)
        {
            return new Move()
            {
                Coordinate = new Coordinate() { X = move.Coordinate.X, Y = move.Coordinate.Y },
                Player = move.Player.ToPlayer(),
                Spell = move.Spell.ToSpell()
            };
        }

        public static Models.Spell ToSpell(this Spell spell)
        {
            return new Models.Spell()
            {
                Description = spell.Description,
                ManaCost = spell.ManaCost,
                Type = (Types.SpellType)(int)spell.Type
            };
        }

        public static Event ToEvent(this Models.Event e)
        {
            var newEvent = new Event()
            {
                Mana = e.Mana,
                Player = e.Player.ToPlayer(),
                Spell = e.Spell?.ToSpell(),
                Type = (EventType)(int)e.Type
            };
            newEvent.Pieces.AddRange(e.Pieces.Select(p => p.ToPiece()));
            return newEvent;
        }
    }
}
