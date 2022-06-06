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
        public Player(int __coinValue, int __speedLevel,  float __moveSpeed, float __jumpForce, Rigidbody2D __rigidbody2D)
        {
            CoinValue = __coinValue;
            SpeedLevel = __speedLevel;
            MoveSpeed = __moveSpeed;
            JumpForce = __jumpForce;
            Rigidbody2D = __rigidbody2D;
        }

        private int speedLevel;
        private int jumpLevel;

        public int CoinValue { get; set; }
        public int SpeedLevel { get { return speedLevel; } set { speedLevel = value; MoveSpeed *= SpeedLevel; } }
        public float MoveSpeed { get; private set; }
        public float MoveHorizontal { get; set; }
        public float MoveVertical { get; set; }
        public int JumpLevel { get { return jumpLevel; } set { jumpLevel = value; JumpForce *= JumpLevel; } }
        public float JumpForce { get; private set; }
        public bool IsJumping { get; set; }
        public Rigidbody2D Rigidbody2D { get; private set; }

    }
}
