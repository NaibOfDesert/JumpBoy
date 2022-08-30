using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player
    {
        public Player(int coinValue, int speedLevel,   float jumpForce, bool isJumping, bool isFacingRight,  Rigidbody2D rigidbody2D)
        {
            CoinValue = coinValue;
            SpeedLevel = speedLevel;

            JumpForce = jumpForce;
            IsJumping = isJumping;
            IsFacingRight = isFacingRight;
            Rigidbody2D = rigidbody2D;
            
        }

        private int speedLevel;

        private int jumpLevel;
        public int CoinValue { get; set; }
        public int SpeedLevel { get { return speedLevel; } set { speedLevel = value; } }

        public float MoveHorizontal { get; set; }
        public float MoveVertical { get; set; }
        public int JumpLevel { get { return jumpLevel; } set { jumpLevel = value; JumpForce *= JumpLevel; } }
        public float JumpForce { get; private set; }
        public bool IsJumping { get; set; }
        public bool IsFacingRight { get; set; }
        public Rigidbody2D Rigidbody2D { get; private set; }

    }
}
