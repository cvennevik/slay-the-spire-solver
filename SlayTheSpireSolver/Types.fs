[<AutoOpen>]
module Types

type Attack =
    | Bash
    | Strike

type Skill =
    | Defend

type Card =
    | Attack
    | Skill

type Hand = Card list

type Health = Health of int
type Block = Block of int

type Enemy = {
    Health: Health;
}

type Player = {
    Health: Health;
}

type GameState = {
    Player: Player;
    Enemy: Enemy;
    Hand: Hand;
}