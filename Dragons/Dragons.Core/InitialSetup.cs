using System.Collections.Generic;

namespace Dragons.Core
{

    public class InitialSetup
    {
        public int BoardSize { get; set; }
        public List<Dragon> Dragons { get; set; }
        public List<Piece> AdditionalPieces { get; set; }

        public static List<InitialSetup> AllSetups()
        {
            return new List<InitialSetup>
            {
                new InitialSetup
                {
                    BoardSize = 14,
                    Dragons = new List<Dragon>
                    {
                        new Dragon
                        {
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 1,
                                    Y = 5
                                },
                                Type = PieceType.DragonHead,
                                Orientation = -90
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 2,
                                    Y = 5
                                },
                                Type = PieceType.DragonBody,
                                Orientation = -90
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 3,
                                    Y = 5
                                },
                                Type = PieceType.DragonTail,
                                Orientation = -90
                            }
                        },
                        new Dragon
                        {
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 10,
                                    Y = 11
                                },
                                Type = PieceType.DragonHead,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 10,
                                    Y = 10
                                },
                                Type = PieceType.DragonBody,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 9,
                                    Y = 11
                                },
                                Type = PieceType.DragonTail,
                                Orientation = 0
                            }
                        },
                        new Dragon
                        {
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 13,
                                    Y = 7
                                },
                                Type = PieceType.DragonHead,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 13,
                                    Y = 5
                                },
                                Type = PieceType.DragonBody,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 12,
                                    Y = 5
                                },
                                Type = PieceType.DragonBody,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 12,
                                    Y = 6
                                },
                                Type = PieceType.DragonBody,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 12,
                                    Y = 7
                                },
                                Type = PieceType.DragonTail,
                                Orientation = 0
                            }
                        },
                        new Dragon
                        {
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 5,
                                    Y = 3
                                },
                                Type = PieceType.DragonHead,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 5,
                                    Y = 2
                                },
                                Type = PieceType.DragonBody,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 6,
                                    Y = 3
                                },
                                Type = PieceType.DragonBody,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 6,
                                    Y = 2
                                },
                                Type = PieceType.DragonBody,
                                Orientation = 0
                            }
                        },
                        new Dragon
                        {
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 9,
                                    Y = 5
                                },
                                Type = PieceType.DragonHead,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 9,
                                    Y = 4
                                },
                                Type = PieceType.DragonTail,
                                Orientation = 0
                            }
                        }
                    },
                    AdditionalPieces = new List<Piece>
                    {
                        new Piece
                        {
                            Coordinate = new Coordinate
                            {
                                X = 11,
                                Y = 12
                            },
                            Type = PieceType.LargeMana,
                            Orientation = 0
                        },
                        new Piece
                        {
                            Coordinate = new Coordinate
                            {
                                X = 11,
                                Y = 8
                            },
                            Type = PieceType.SmallMana,
                            Orientation = 0
                        },
                        new Piece
                        {
                            Coordinate = new Coordinate
                            {
                                X = 2,
                                Y = 2
                            },
                            Type = PieceType.SmallMana,
                            Orientation = 0
                        },
                        new Piece
                        {
                            Coordinate = new Coordinate
                            {
                                X = 6,
                                Y = 1
                            },
                            Type = PieceType.SmallMana,
                            Orientation = 0
                        }
                    }
                },
                new InitialSetup
                {
                    BoardSize = 14,
                    Dragons = new List<Dragon>
                    {
                        new Dragon
                        {
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 6,
                                    Y = 1
                                },
                                Type = PieceType.DragonHead,
                                Orientation = -90
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 7,
                                    Y = 2
                                },
                                Type = PieceType.DragonBody,
                                Orientation = -90
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 7,
                                    Y = 1
                                },
                                Type = PieceType.DragonTail,
                                Orientation = -90
                            }
                        },
                        new Dragon
                        {
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 2,
                                    Y = 7
                                },
                                Type = PieceType.DragonHead,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 2,
                                    Y = 8
                                },
                                Type = PieceType.DragonBody,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 2,
                                    Y = 9
                                },
                                Type = PieceType.DragonTail,
                                Orientation = 0
                            }
                        },
                        new Dragon
                        {
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 12,
                                    Y = 11
                                },
                                Type = PieceType.DragonHead,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 12,
                                    Y = 12
                                },
                                Type = PieceType.DragonBody,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 11,
                                    Y = 12
                                },
                                Type = PieceType.DragonBody,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 10,
                                    Y = 12
                                },
                                Type = PieceType.DragonBody,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 10,
                                    Y = 11
                                },
                                Type = PieceType.DragonTail,
                                Orientation = 0
                            }
                        },
                        new Dragon
                        {
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 12,
                                    Y = 3
                                },
                                Type = PieceType.DragonHead,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 12,
                                    Y = 2
                                },
                                Type = PieceType.DragonBody,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 11,
                                    Y = 3
                                },
                                Type = PieceType.DragonBody,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 11,
                                    Y = 2
                                },
                                Type = PieceType.DragonBody,
                                Orientation = 0
                            }
                        },
                        new Dragon
                        {
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 7,
                                    Y = 11
                                },
                                Type = PieceType.DragonHead,
                                Orientation = 0
                            },
                            new Piece
                            {
                                Coordinate = new Coordinate
                                {
                                    X = 7,
                                    Y = 12
                                },
                                Type = PieceType.DragonTail,
                                Orientation = 0
                            }
                        }
                    },
                    AdditionalPieces = new List<Piece>
                    {
                        new Piece
                        {
                            Coordinate = new Coordinate
                            {
                                X = 0,
                                Y = 0
                            },
                            Type = PieceType.LargeMana,
                            Orientation = 0
                        },
                        new Piece
                        {
                            Coordinate = new Coordinate
                            {
                                X = 0,
                                Y = 13
                            },
                            Type = PieceType.SmallMana,
                            Orientation = 0
                        },
                        new Piece
                        {
                            Coordinate = new Coordinate
                            {
                                X = 5,
                                Y = 3
                            },
                            Type = PieceType.SmallMana,
                            Orientation = 0
                        },
                        new Piece
                        {
                            Coordinate = new Coordinate
                            {
                                X = 12,
                                Y = 4
                            },
                            Type = PieceType.SmallMana,
                            Orientation = 0
                        }
                    }
                }
            };
        }
    }
}
