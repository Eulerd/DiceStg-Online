﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceStg_Online.Core
{
    public class Game
    {
        /// <summary>
        /// 初期ステータスを使用して初期化
        /// </summary>
        /// <param name="initState"></param>
        public Game(GameState initState)
        {
            State = initState;
        }

        /// <summary>
        /// 全Playerのアクションを元にゲームを更新
        /// </summary>
        /// <param name="actions"></param>
        public void Update(IList<ActionState> actions)
        {
            if(actions == null)
            {
                throw new ArgumentException("ゲームの進行にはアクションが必要です。");
            }

            if(actions.Count != State.Players.Count)
            {
                throw new ArgumentException("プレイヤーの数と等しいアクションを入力してください。");
            }
            
            // 行動
            actionPhase(actions);

            judgePhase();

            // 状態を更新
            State = new GameState(State.Field, State.Players, State.Turn + 1);
        }

        /// <summary>
        /// ゲームの状態
        /// </summary>
        public GameState State { get; private set; }

        private void judgePhase()
        {
            for (int i = 0; i < State.Players.Count; i++)
            {
                Player player = State.Players[i];

                foreach(Bullet bullet in player.Bullets)
                {
                    if (!isInField(bullet.Position))
                        bullet.Disabling();
                }
            }
        }

        /// <summary>
        /// アクションを元にオブジェクトを移動させる
        /// </summary>
        /// <param name="actions">プレイヤーごとのアクション</param>
        private void actionPhase(IList<ActionState> actions)
        {
            for(int i = 0;i < State.Players.Count;i++)
            {
                ActionState action = actions[i];
                Player player = State.Players[i];

                switch (action)
                {
                    case ActionState.MoveUp:
                    case ActionState.MoveDown:
                    case ActionState.MoveRight:
                    case ActionState.MoveLeft:
                        if (isInField(player.Position.Move(action)) && canPass(player.Position.Move(action)))
                            player.Move(action);
                        break;
                    case ActionState.Shot:
                        player.Shot();
                        break;
                    case ActionState.DoNothing:
                    default:
                        player.Update();
                        break;
                }

            }
        }

        /// <summary>
        /// その場所がフィールド内にあるか判定する
        /// </summary>
        /// <param name="pos">判断する場所</param>
        /// <returns>その場所がフィールド内にあるか</returns>
        private bool isInField(Point pos)
        {
            return (pos.X >= 0 && pos.Y >= 0 && pos.X < State.Field.Width && pos.Y < State.Field.Height);
        }
        
        private bool canPass(Point pos)
        {
            return (getObject(pos) == null);
        }

        private IDiceStgObject getObject(Point pos)
        {
            IDiceStgObject res = null;
            foreach(Player p in State.Players)
            {
                if(p.Position == pos)
                {
                    res = p;
                    break;
                }
            }

            return res;
        }
        
        // todo 通行可能か
        // todo その場所に何があるか
    }
}
