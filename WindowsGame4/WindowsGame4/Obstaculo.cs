﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame4
{
    class Obstaculo : Game1 
    {
        protected List<Rectangle> frames = new List<Rectangle>();
            private int frameWidth = 0;
            private int frameHeight = 0;
            private int currentFrame;
            private float frameTime = 0.1f;
            private float timeForCurrentFrame = 0.0f;
            private Color tintColor = Color.White;
            private float rotation = 0.0f;
            public int CollisionRadius = 0;
            public int BoundingXPadding = 0;
            public int BoundingYPadding = 0;
            protected Vector2 location = Vector2.Zero;
            protected Vector2 velocity = Vector2.Zero;

            public BoundingBox obBR = new BoundingBox();

        public Obstaculo(Vector2 location,Texture2D Piedra,Rectangle piedraFrame,Vector2 velocity)
    {
        this.location = location;
        Texture texture = Piedra;
        this.velocity = velocity;
        frames.Add(piedraFrame);
        frameWidth = piedraFrame.Width;
        frameHeight = piedraFrame.Height;

            
    }

            public Rectangle BoundingBoxRect
            {
                get
                {
                    return new Rectangle(
                    (int)location.X + BoundingXPadding,
                    (int)location.Y + BoundingYPadding,
                    frameWidth - (BoundingXPadding * 2),
                    frameHeight - (BoundingYPadding * 2));
                }
            }

            public Vector2 Location
            {
                get { return location; }
                set { location = value; }
            }
            public Vector2 Velocity
            {
                get { return velocity; }
                set { velocity = value; }
            }
            public Color TintColor
            {
                get { return tintColor; }
                set { tintColor = value; }
            }
            public float Rotation
            {
                get { return rotation; }
                set
                {
                    rotation = value % MathHelper.TwoPi;
                }
            }

            public int Frame
                {
                    get { return currentFrame; }
                    set { currentFrame = (int)MathHelper.Clamp(value, 0, frames.Count - 1); }
                }
            public float FrameTime
                {
                    get { return frameTime; }
                    set { frameTime = MathHelper.Max(0, value); }
                }
            public Rectangle Source
                {
                    get { return frames[currentFrame]; }
                }
            public Rectangle Destination
                { 
                    get
                        {
                            return new Rectangle(
                            (int)location.X,
                            (int)location.Y,
                            frameWidth,
                            frameHeight);
                        }
                }

        public Vector2 Center
        {
            get
            {
                return location +
                new Vector2(frameWidth / 2, frameHeight / 2);
            }
        }

            public bool IsBoxColliding(Rectangle OtherBox, Rectangle BoundingBoxRect)
            {
                return BoundingBoxRect.Intersects(OtherBox);
            }
            public bool IsCircleColliding(Vector2 otherCenter, float
            otherRadius)
            {
                if (Vector2.Distance(Center, otherCenter) < (CollisionRadius + otherRadius))
                {
                    return true;
                }
                else
                {
                    return false;
                }
                }
    }
    }

