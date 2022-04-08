using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using EdGeLib.Components;
using EdGeLib.Sprites;
using EdGeLib.Systems;

namespace EdGeLib.GUI
{
    public class TextBox : Label
    {
        private readonly CursorSprite cursorSprite;

        private readonly KeyCommand A;
        private readonly KeyCommand B;
        private readonly KeyCommand C;
        private readonly KeyCommand D;
        private readonly KeyCommand E;
        private readonly KeyCommand F;
        private readonly KeyCommand G;
        private readonly KeyCommand H;
        private readonly KeyCommand I;
        private readonly KeyCommand J;
        private readonly KeyCommand K;
        private readonly KeyCommand L;
        private readonly KeyCommand M;
        private readonly KeyCommand N;
        private readonly KeyCommand O;
        private readonly KeyCommand P;
        private readonly KeyCommand Q;
        private readonly KeyCommand R;
        private readonly KeyCommand S;
        private readonly KeyCommand T;
        private readonly KeyCommand U;
        private readonly KeyCommand V;
        private readonly KeyCommand W;
        private readonly KeyCommand X;
        private readonly KeyCommand Y;
        private readonly KeyCommand Z;
        private readonly KeyCommand Back;
        private readonly KeyCommand Enter;

        public TextBox(int width)
            : base("", width, false)
        {
            cursorSprite = new CursorSprite();
            Graphics.Sprites.Add(cursorSprite);
            ContentRectangle.Visible = true;
            TextSprite.TextChanged += TextSprite_TextChanged;   
            Input.MouseEnabled = true;
            Input.FocusChanged += Input_FocusChanged;

            A = new KeyCommand(Keys.A);
            Input.KeyCommands.Add(A);
            A.Pressed += A_Pressed;
            B = new KeyCommand(Keys.B);
            Input.KeyCommands.Add(B);
            B.Pressed += B_Pressed;
            C = new KeyCommand(Keys.C);
            Input.KeyCommands.Add(C);
            C.Pressed += C_Pressed;
            D = new KeyCommand(Keys.D);
            Input.KeyCommands.Add(D);
            D.Pressed += D_Pressed;
            E = new KeyCommand(Keys.E);
            Input.KeyCommands.Add(E);
            E.Pressed += E_Pressed;
            F = new KeyCommand(Keys.F);
            Input.KeyCommands.Add(F);
            F.Pressed += F_Pressed;
            G = new KeyCommand(Keys.G);
            Input.KeyCommands.Add(G);
            G.Pressed += G_Pressed;
            H = new KeyCommand(Keys.H);
            Input.KeyCommands.Add(H);
            H.Pressed += H_Pressed;
            I = new KeyCommand(Keys.I);
            Input.KeyCommands.Add(I);
            I.Pressed += I_Pressed;
            J = new KeyCommand(Keys.J);
            Input.KeyCommands.Add(J);
            J.Pressed += J_Pressed;
            K = new KeyCommand(Keys.K);
            Input.KeyCommands.Add(K);
            K.Pressed += K_Pressed;
            L = new KeyCommand(Keys.L);
            Input.KeyCommands.Add(L);
            L.Pressed += L_Pressed;
            M = new KeyCommand(Keys.M);
            Input.KeyCommands.Add(M);
            M.Pressed += M_Pressed;
            N = new KeyCommand(Keys.N);
            Input.KeyCommands.Add(N);
            N.Pressed += N_Pressed;
            O = new KeyCommand(Keys.O);
            Input.KeyCommands.Add(O);
            O.Pressed += O_Pressed;
            P = new KeyCommand(Keys.P);
            Input.KeyCommands.Add(P);
            P.Pressed += P_Pressed;
            Q = new KeyCommand(Keys.Q);
            Input.KeyCommands.Add(Q);
            Q.Pressed += Q_Pressed;
            R = new KeyCommand(Keys.R);
            Input.KeyCommands.Add(R);
            R.Pressed += R_Pressed;
            S = new KeyCommand(Keys.S);
            Input.KeyCommands.Add(S);
            S.Pressed += S_Pressed;
            T = new KeyCommand(Keys.T);
            Input.KeyCommands.Add(T);
            T.Pressed += T_Pressed;
            U = new KeyCommand(Keys.U);
            Input.KeyCommands.Add(U);
            U.Pressed += U_Pressed;
            V = new KeyCommand(Keys.V);
            Input.KeyCommands.Add(V);
            V.Pressed += V_Pressed;
            W = new KeyCommand(Keys.W);
            Input.KeyCommands.Add(W);
            W.Pressed += W_Pressed;
            X = new KeyCommand(Keys.X);
            Input.KeyCommands.Add(X);
            X.Pressed += X_Pressed;
            Y = new KeyCommand(Keys.Y);
            Input.KeyCommands.Add(Y);
            Y.Pressed += Y_Pressed;
            Z = new KeyCommand(Keys.Z);
            Input.KeyCommands.Add(Z);
            Z.Pressed += Z_Pressed;
            Back = new KeyCommand(Keys.Back);
            Input.KeyCommands.Add(Back);
            Back.Pressed += Back_Pressed;
            Enter = new KeyCommand(Keys.Enter);
            Input.KeyCommands.Add(Enter);
            Enter.Pressed += Enter_Pressed;
        }

        private void Enter_Pressed(object sender, KeyInputEventArgs e)
        {
            Input.HasFocus = false;
        }

        private void A_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('A');
        }

        private void B_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('B');
        }

        private void C_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('C');
        }

        private void D_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('D');
        }

        private void E_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('E');
        }

        private void F_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('F');
        }

        private void G_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('G');
        }

        private void H_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('H');
        }

        private void I_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('I');
        }

        private void J_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('J');
        }

        private void K_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('K');
        }

        private void L_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('L');
        }

        private void M_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('M');
        }

        private void N_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('N');
        }

        private void O_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('O');
        }

        private void P_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('P');
        }

        private void Q_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('Q');
        }

        private void R_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('R');
        }

        private void S_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('S');
        }

        private void T_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('T');
        }

        private void U_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('U');
        }

        private void V_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('V');
        }

        private void W_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('W');
        }

        private void X_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('X');
        }

        private void Y_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('Y');
        }

        private void Z_Pressed(object sender, KeyInputEventArgs e)
        {
            AddChar('Z');
        }

        private void Back_Pressed(object sender, KeyInputEventArgs e)
        {
            if (TextSprite.Text.Length > 0)
            {
                TextSprite.Text = TextSprite.Text.Remove(TextSprite.Text.Length - 1);
            }
        }

        private void Input_FocusChanged(object sender, EventArgs e)
        {
            if (Input.HasFocus)
            {
                cursorSprite.Visible = true;
                cursorSprite.IsAnimating = true;
            }
            else
            {
                cursorSprite.Visible = false;
                cursorSprite.IsAnimating = false;
            }
        }

        private void TextSprite_TextChanged(object sender, EventArgs e)
        {
            cursorSprite.Offset = new Vector2(
                cursorSprite.DefaultOffset.X + TextSprite.Width,
                cursorSprite.DefaultOffset.Y);
        }

        private void AddChar(char newChar)
        {
            TextSprite.Text += newChar;
            if (TextSprite.Width > ContentRectangle.Width)
            {
                TextSprite.Text = TextSprite.Text.Remove(TextSprite.Text.Length - 1);
            }
        }
    }
}
