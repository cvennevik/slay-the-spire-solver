# Slay the Spire Solver
A WIP tool for finding the best possible actions in Slay the Spire fights.

This README was last updated May 21st, 2022.

## Why?
This is one of those "side projects" I've been told programmers do for fun.

A list of reasons:
1. I've played Slay the Spire a few hundred hours and it's very familiar to me.
2. "Solving" the game sounds like a fun challenge which I can break into smaller goals.
3. I want to try out programming techniques freely!

## How?
Two parts:

1. A rules engine for Slay the Spire fights.
2. An AI using the rules engine to find the best actions.

I'll start with setting up the rules engine to handle a single fight: A fresh Ironclad start vs. a Jaw Worm. I'll then write the AI to look for optimal actions, and expand the project there.

**I do not have much experience with AI and I have zero experience with machine learning.** My first idea is to explore every possible action and game state fully and pick the actions with the highest expected player health at the end of the fight. That is a very naive performance metric, and I'm 99% certain that exploring all possibilities is infeasible. Figuring it out will be part of the project!

Oh, and I will be spending an indulgent amount of time making the rules engine nice and expressive. Optimizing for performance comes later.

## Is this open source?
Nope! The source code is public, but this is a "me" project, and I'm not placing it under any sort of license as of now. I'm not even certain that there's no legal issues with reproducing the Slay the Spire rules engine. If it becomes relevant, I'll look into what options there are.
