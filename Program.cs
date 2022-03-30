using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EZInput;
using System.IO;
using System.Threading;

namespace TankCap
{
    class Program
    {
        static char[,] mazeA = new char[30, 141];
        static int[,] bullets = new int[2, 128];
        static string[] mainA = new string[24]{
                              "                                                         _..----.._                                ",
                              "                                                        ]_.--._____[                               ",
                              "                                                      ___|`--`__..|--._                            ",
                              "                                  __               ```    ;            :                           ",
                              "                                ()_ `````---...__.```!`:  /    ___       :                         ",
                              "                                   ```---...__\\]..__] | /    [ 0 ]      :                         ",
                              "                                              ```!--./ /      ```        :                         ",
                              "                                       __  ...._____;```.__________..--..:_                        ",
                              "                                      /  !```````!``````````|````/` ` ` ` \\`--..__  __..          ",
                              "                                     /  /.--.    |          |  .`          \\` ` `.``--.{`.        ",
                              "                 _...__            >=7 //.-.:    |          |.`             \\ ._.__  ` ````.      ",
                              "              .-` /    ````----..../ ``>==7-.....:______    |                \\| |  ``;.;-`> \\    ",
                              "              ````;           __..`   .--`/`````----....`````----.....H_______\\_!....`----````]   ",
                              "              _..---|._ __..--``       _!.-=_.            ```````````````                   ;```   ",
                              "           /   .-`;-.`--...___     .` .-``; `;``-``-...^..__...-v.^___,  ,__v.__..--^`--``-v.^v,   ",
                              "          ;   ;   |`.         ```-/ ./;  ;   ;\\P.        ;   ;        ````____;  ;.--````// ```<, ",
                              "          ;   ;   | 1            ;  ;  `.: .`  ;<   ___.-`._.`------``````____`..`.--```;;`  o `;  ",
                              "          `.   \\__:/__           ;  ;--``()_   ;`  /___ .-` ____---``````` __.._ __._   `>.,  ,/; ",
                              "            \\   \\    /```<--...__;  `_.-`/; ``; ;.`.`  `-..`    `-.      /`/    `__. `.   `---`; ",
                              "             `.  `v ; ;     ;;    \\  \\ .`  \\ ; ///     _.-` `-._   ;    : ;   .-`__ `. ;   .^`.`",
                              "               `.  `; `.   .`/     `. `-.__.` /;;;   .o__.---.__o. ;    : ;   ```;;``` ;v^` .^     ",
                              "                 `-. `-.___.`<__v.^,v`.  `-.-` ;|:   `    :      ` ;v^v^`.`.    .;`.__/_..-`       ",
                              "                    `-...__.___...---```-.   `-`.;\\     `WW\\     .`_____..>.`^`-````````         ",
                              "                                          `--..__ ``._..`  ``-;;```                                "};

        static char[,] player1 = new char[4,14] {{'[', '[', '[', '\xDB', '\xDB', '\xDB', '\xDB', '\xDB', '=', '=', '~', '~', '~', '>'},
                       {' ', ' ', '_', ',', '*', '\xDB', '\xDB', '*', ',', '_', ' ', ' ', ' ', ' '},
                       {' ', '\xDB', '\xDB', '\xDB', '\xDB', '\xDB', '\xDB', '\xDB', '\xDB', '\xDB', '\xDB', '\xDB', ']', ' '},
                       {' ', '\\', '_', '@', '_', '@', '_', '@', '_', '@', '_', '@', ' ', ' '}};

        static char[,] enemy = new char[4,14] {{'<', '~', '~', '~', '=', '=', '\xDB', '\xDB', '\xDB', '\xDB', '\xDB', ']', ']', ']'},
                     {' ', ' ', ' ', ' ', '_', ',', '*', '\xDB', '\xDB', '*', ',', '_', ' ', ' '},
                     {' ', '\xDB', '\xDB', '\xDB', '\xDB', '\xDB', '\xDB', '\xDB', '\xDB', '\xDB', '\xDB', '\xDB', ']', ' '},
                     {' ', '\\', '_', '@', '_', '@', '_', '@', '_', '@', '_', '@', ' ', ' '}};
        static string[] enemy1 = new string[4] {"      ______ ",
                                                "<====(``````)",
                                                "     \\(@)(@)/",
                                                "      ****** "};

        static double highScore = 0;
        static int pX = 11;
        static int pY = 1;
        static int e1X = 11;
        static int e1Y = 125;
        static int e2X = 1;
        static int e2Y = 125;
        static int e3X = 22;
        static int e3Y = 125;
        static int a = 21;
        static bool tf = false;
        static bool enemyBool = false;
        static bool enemy1Bool = false;
        static bool enemy2Bool = false;
        static bool horizontal = false;
        static bool horizontal1 = false;
        static bool horizontal2 = false;
        static bool gameBreak = true;
        static int bIndex = 0;
        static int temp = 0;
        static int enemy1Health = 20;
        static int enemy2Health = 20;
        static int enemy3Health = 20;
        static bool e1Bullet = false;
        static bool e2Bullet = false;
        static bool e3Bullet = false;
        static int e1BulletX = e1X;
        static int e1BulletY = 124;
        static int e1count = 0;
        static int e2count = 0;
        static int e3count = 0;
        static int playerHealth = 3;
        static int e2BulletX = e2X + 1;
        static int e2BulletY = e2Y - 1;
        static int e3BulletX = e3X + 1;
        static int e3BulletY = e3Y - 1;
        static int playerLife = 3;
        static int playerHealthPercentage = 0;
        static int score = 0;
        static void Main(string[] args)
        {
            string op;
            int b = 0;
            int c = 0;
            readFile();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            mainTank();
            Console.ForegroundColor= ConsoleColor.White;
            gotoxy(26, 50);
            loading();
            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Yellow;
                op = mainMenu();
                if (op == "1")
                {
                    Console.Clear();
                    Console.CursorVisible = false;
                    maze();
                    enemyBool = true;
                    enemy1Bool = true;
                    enemy2Bool = true;
                    horizontal = true;
                    horizontal1 = true;
                    horizontal2 = true;
                    highScore = 0;
                    pX = 11;
                    pY = 1;
                    e1X = 11;
                    e1Y = 125;
                    e2X = 1;
                    e2Y = 125;
                    e3X = 22;
                    e3Y = 125;
                    a = 21;
                    tf = false;
                    gameBreak = true;
                    bIndex = 0;
                    temp = 0;
                    enemy1Health = 20;
                    enemy2Health = 20;
                    enemy3Health = 20;
                    e1Bullet = false;
                    e2Bullet = false;
                    e3Bullet = false;
                    e1BulletX = e1X;
                    e1BulletY = 124;
                    e1count = 0;
                    e2count = 0;
                    e3count = 0;
                    playerHealth = 3;
                    e2BulletX = e2X + 1;
                    e2BulletY = e2Y - 1;
                    e3BulletX = e3X + 1;
                    e3BulletY = e3Y - 1;
                    playerLife = 3;
                    playerHealthPercentage = 0;
                    score = 0;
                    enemyDisplay();
                    enemy1Display();
                    enemy2Display();
                    playerDisplay();
                    while (gameBreak)
                    {
                        a++;
                        if (a >= 10)
                        {
                            moveEnemy();
                            a = 0;
                        }
                        b++;
                        if (b >= 15)
                        {
                            moveEnemy1();
                            b = 0;
                        }
                        c++;
                        if (c > 10)
                        {
                            moveEnemy2();
                            c = 0;
                        }
                        e1count++;
                        e2count++;
                        e3count++;
                        if (Keyboard.IsKeyPressed(Key.LeftArrow) && mazeA[pX, pY - 1] == ' ')
                        {
                            movePLeft();
                        }
                        if (Keyboard.IsKeyPressed(Key.RightArrow) && mazeA[pX, pY + 14] == ' ')
                        {
                            movePRight();
                        }
                        if (Keyboard.IsKeyPressed(Key.UpArrow) && mazeA[pX - 1, pY] == ' ')
                        {
                            movePUP();
                        }
                        if (Keyboard.IsKeyPressed(Key.DownArrow) && mazeA[pX + 4, pY] == ' ')
                        {
                            movePDown();
                        }
                        if (Keyboard.IsKeyPressed(Key.Space))
                        {
                            Thread.Sleep(5);
                            tf = true;
                            genBullet();
                        }
                        if (Keyboard.IsKeyPressed(Key.Escape))
                        {
                            pX = 11;
                            pY = 1;
                            e1X = 11;
                            e1Y = 125;
                            a = 21;
                            tf = false;
                            enemyBool = false;
                            horizontal = false;
                            gameBreak = true;
                            bIndex = 0;
                            temp = 0;
                            break;
                        }
                        drawBullets();
                        eraseBullets();
                        moveBullet();
                        bulletHit();
                        enemy1B();
                        enemy2B();
                        enemy3B();
                        enemy1Bupdate();
                        enemy2Bupdate();
                        enemy3Bupdate();
                        lifeEnemy();
                        life_healthDisplay();
                        scoreDisplay();
                        game_break();
                    }
                }
                else if (op == "2")
                {
                    while (true)
                    {
                        string o = " ";
                        Console.Clear();
                        string op1 = instructMenu();
                        if (op1 == "1")
                        {
                            Console.Clear();
                            header();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("keys.");
                            Console.WriteLine("________________");
                            Console.WriteLine("1. UP              Go up");
                            Console.WriteLine("2. Down            Go down");
                            Console.WriteLine("3. Left            Go Left");
                            Console.WriteLine("4. Right           Go Right");
                            Console.WriteLine("5. Space           Fire user");
                            Console.WriteLine("6. ESC             End Game.");
                            Console.Write("Press any key to continue : ");
                            Console.Read();
                            Console.Read();
                            Console.Read();
                        }
                        else if (op1 == "2")
                        {
                            Console.Clear();
                            header();
                            Console.ForegroundColor= ConsoleColor.Green;
                            Console.WriteLine("Instructions.");
                            Console.WriteLine("-----------------------");
                            Console.WriteLine("1. Enemy died after 20 bullet hits.");
                            Console.WriteLine("2. You have 3 lives (each life has 3 health)");
                            Console.WriteLine("3. if one bullet of enemy hit you then one health decrease(3 health = 1 life)");
                            Console.WriteLine("4. If you hit with enemy you loose");
                            Console.WriteLine("5. If enemy Collide with left wall you loose.");
                            Console.Write("Press any key to continue : ");
                            Console.Read();
                            Console.Read();
                            Console.Read();
                        }
                        else if (op1 == "3")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("You entered wrong option.");
                            Console.Write("Enter any key to continue : ");
                            o = Console.ReadLine();
                            
                        }
                    }
                }
                else if (op == "3")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Thank you for playing.");
                    break;
                }
                else
                {
                    Console.WriteLine("You entered wrong option.");
                }
            }
        }
        static void header()
        {
            Console.ForegroundColor=ConsoleColor.DarkYellow;
            Console.WriteLine(" _____           _    _____");
            Console.WriteLine("|_   _|         | |  /  __ \\");
            Console.WriteLine("  | | __ _ _ __ | | _| /  \\/ __ _ _ __  _   _");
            Console.WriteLine("  | |/ _` | '_ \\| |/ / |    / _` | '_ \\| | | |");
            Console.WriteLine("  | | (_| | | | |   <| \\__/\\ (_| | |_) | |_| |");
            Console.WriteLine("  \\_/\\__,_|_| |_|_|\\_\\____/ \\__,_| .__(_)__,_|");
            Console.WriteLine("                                 | |           ");
            Console.WriteLine("                                 |_|         ");
            Console.WriteLine();
            Console.WriteLine();
        }
        static string mainMenu()
        {
            string op;
            header();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Menu.");
            Console.WriteLine("__________________");
            Console.WriteLine("1. Start");
            Console.WriteLine("2. Option");
            Console.WriteLine("3. Exit");
            Console.Write("Enter one option : ");
            op = Console.ReadLine();
            return op;
        }
        static void playerDisplay()
        {
            Console.ForegroundColor =ConsoleColor.Cyan;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 14; y++)
                {
                    gotoxy(pX + x, pY + y);
                    Console.Write(player1[x, y]);
                }
                Console.WriteLine();
            }
        }
        static void enemyDisplay()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 14; y++)
                {
                    gotoxy(e1X + x, e1Y + y);
                    Console.Write(enemy[x, y]);
                }
                Console.WriteLine();
            }
        }
        static void enemy1Display()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int x = 0; x < 4; x++)
            {
                gotoxy(e2X + x, e2Y);
                Console.WriteLine(enemy1[x]);
            }
        }
        static void enemy2Display()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int x = 0; x < 4; x++)
            {
                    gotoxy(e3X + x, e3Y);
                    Console.WriteLine(enemy1[x]);
            }
        }
        static void mainTank()
        {
            for (int x = 0; x < 24; x++)
            {
                 Console.WriteLine(mainA[x]);
            }
        }
        static void gotoxy(int col, int ro)
        {
            if(ro  >= 0 && ro < 180 && col>=0 && col < 35)
            {
                Console.SetCursorPosition(ro, col);
            }
        }
        static void loading()
        {
            Console.WriteLine("        TankCap.u");
            Console.Write("\t\t\t\t\t\t   Please wait while loading\n\n");
            char a = ' ';
                char b = ' ';
            a = '\xDB';
            b = '\xDB';
            Console.Write("\t\t\t\t\t\t       ");
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i <= 15; i++)
                Console.Write(a);
            Console.Write("\r");
            Console.Write("\t\t\t\t\t\t       ");
            Console.ForegroundColor= ConsoleColor.Blue;
            for (int i = 0; i <= 15; i++)
            {
                Console.Write(b);
                Thread.Sleep(500);
            }
        }
        static void readFile()
        {
            StreamReader fp = new StreamReader("maze.txt");
            string record;
            int row = 0;
            while ((record = fp.ReadLine()) != null)
            {
                for (int x = 0; x < 140; x++)
                {
                    mazeA[row, x] = record[x];
                }
                row++;
            }
            fp.Close();
        }
        static void maze()
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int x = 0; x < 30; x++)
            {
                for (int y = 0; y < 140; y++)
                {
                    Console.Write(mazeA[x,y]);
                }
                Console.WriteLine();
            }
        }
        static void movePLeft()
        {
            mazeA[pX,pY] = ' ';
            gotoxy(pX, pY);
            Console.Write(" ");

            pY = pY - 1;
            gotoxy(pX, pY);
            playerDisplay();
            gotoxy(pX, pY + 14);
            Console.Write(" ");
        }
        static void movePRight()
        {
            mazeA[pX,pY] = ' ';
            gotoxy(pX, pY);
            Console.Write(" ");
            pY = pY + 1;
            gotoxy(pX, pY);
            playerDisplay();
        }
        static void movePUP()
        {
            mazeA[pX, pY] = ' ';
            for (int x = 0; x < 14; x++)
            {
                gotoxy(pX + 3, pY + x);
                Console.Write(" ");
            }
            pX = pX - 1;
            gotoxy(pX, pY);
            playerDisplay();
        }
        static void movePDown()
        {
            mazeA[pX, pY] = ' ';
            for (int x = 0; x < 14; x++)
            {
                gotoxy(pX, pY + x);
                Console.Write(" ");
            }

            pX = pX + 1;
            gotoxy(pX, pY);
            playerDisplay();
        }
        static void genBullet()
        {
            bullets[0, bIndex] = pY + 14;
            bullets[1, bIndex] = pX;
            bIndex++;
            if (bIndex == 128)
                bIndex = 0;
        }
        static void drawBullets()
        {
            for (int i = 0; i < 128; i++)
            {
                if (mazeA[bullets[1, i], bullets[0, i]] == '#')
                {

                    gotoxy(bullets[1, i], bullets[0, i]);
                    Console.Write("#");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    gotoxy(bullets[1, i], bullets[0, i]);
                    Console.Write(">");
                }
            }
            temp = 0;
        }
        static void moveBullet()
        {
            for (int i = 0; i < 128; i++)
            {
                if (bullets[0, i] > 2 && i < 128)
                {
                    bullets[0, i] = bullets[0, i] + 1;
                    if (bullets[0, i] > 139)
                    {
                        bullets[0, i] = 0;
                    }
                }
                else if (i > 127 && bullets[0, i] <= 2)
                {
                    bullets[0, i] = 0;
                }
            }
        }
        static void eraseBullets()
        {
            for (int i = 0; i < 128; i++)
            {
                if (bullets[0, i] >= 1 && mazeA[bullets[1, i], bullets[0, i]] == ' ' || (bullets[0, i] == e1Y && e1X + 4 - bullets[1, i] > 0 && e1X + 4 - bullets[1, i] < 8))
                {
                    gotoxy(bullets[1, i], bullets[0, i]);
                    Console.Write(" ");
                }
            }
        }
        static void moveEnemy()
        {
            if (enemyBool)
            {
                if (horizontal)
                {

                    if (mazeA[e1X, e1Y - 1] == '>')
                    {
                        horizontal = false;
                    }
                    if (mazeA[e1X, e1Y - 1] == ' ')
                    {
                        if (mazeA[e1X, e1Y] == '.')
                        {
                            gotoxy(e1X, e1Y);
                            Console.Write(".");
                        }
                        else if (mazeA[e1X, e1Y] == ' ')
                        {
                            gotoxy(e1X, e1Y);
                            Console.Write(" ");
                        }
                        else if (mazeA[e1X, e1Y] == '*')
                        {
                            gotoxy(e1X, e1Y);
                            Console.Write("*");
                        }
                        e1Y = e1Y - 1;
                        enemyDisplay();
                    }
                    gotoxy(e1X, e1Y + 14);
                    Console.Write(" ");
                }
            }
        }
        static void eraseBullet(int i)
        {
            gotoxy(bullets[0, i], bullets[1, i]);
            Console.Write(" ");
        }
        static void bulletHit()
        {
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < bIndex; j++)
                {
                    if (bullets[i, j] != 0)
                    {
                        if (bullets[i + 1, j] >= e1X && bullets[i + 1, j] <= e1X + 4)
                        {

                            if (bullets[i, j] >= e1Y && bullets[i, j] <= e1Y + 4)
                            {
                                eraseBullet(i);
                                bullets[i, j] = 0;
                                enemy1Health--;
                                score = score + 10;
                                if (enemy1Health == 0)
                                {
                                    enemyBool = false;
                                    eraseEnemy1();
                                    e1X = 10000;
                                    e1Y = 10000;
                                }
                            }
                        }
                        if (bullets[i + 1, j] >= e2X && bullets[i + 1, j] <= e2X + 4)
                        {
                            if (bullets[i, j] >= e2Y && bullets[i, j] <= e2Y + 4)
                            {
                                eraseBullet(i);
                                bullets[i, j] = 0;
                                enemy2Health--;
                                score = score + 10;
                                if (enemy2Health == 0)
                                {
                                    enemy1Bool = false;
                                    eraseEnemy2();
                                    e2X = 10000;
                                    e2Y = 10000;
                                }
                            }
                        }
                        if (bullets[i + 1 , j] >= e3X && bullets[i + 1, j] <= e3X + 4)
                        {
                            if (bullets[i, j] >= e3Y && bullets[i, j] <= e3Y + 4)
                            {
                                eraseBullet(i);
                                bullets[i, j] = 0;
                                enemy3Health--;
                                score = score + 10;
                                if (enemy3Health == 0)
                                {
                                    enemy2Bool = false;
                                    eraseEnemy3();
                                    e3X = 10000;
                                    e3Y = 10000;
                                }
                            }
                        }
                    }
                }
            }
        }
        static void eraseEnemy1()
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 14; y++)
                {
                    int pg = e1X + x;
                    int pg1 = e1Y + y;
                    gotoxy(pg, pg1);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        static void eraseEnemy2()
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 14; y++)
                {
                    gotoxy(e2X + x, e2Y + y);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        static void eraseEnemy3()
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 14; y++)
                {
                    gotoxy(e3X + x, e3Y + y);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        static void erasePlayer()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 14; y++)
                {
                    gotoxy(pX + x, pY + y);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
        static void enemy1Bupdate()
        {
            if (e1Bullet)
            {
                gotoxy(e1BulletX, e1BulletY - e1count);
                Console.Write("<");
                gotoxy(e1BulletX, e1BulletY - e1count + 1);
                Console.Write(" ");
                if (e1BulletY - e1count == 1 || (e1BulletY - e1count == pY + 14 && e1BulletX <= pX + 3 && e1BulletX >= pX) || (pX + 3 == e1BulletX && pY <= e1BulletY - e1count && pY + 14 >= e1BulletY - e1count))
                {
                    if (e1BulletY - e1count == 1)
                    {
                        gotoxy(e1BulletX, 1);
                        Console.Write(" ");
                    }
                    else
                    {
                        playerHealth--;
                        gotoxy(e1BulletX, e1BulletY - e1count);
                        Console.Write(" ");
                    }

                    e1Bullet = false;
                    e1count = 0;
                }
            }
        }
        static void enemy1B()
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (e1Bullet == false && enemyBool == true)
            {
                e1BulletX = e1X;
                e1BulletY = e1Y - 1;
                gotoxy(e1BulletX, e1BulletY - 1);
                Console.Write("<");
                e1Bullet = true;
            }
        }
        static void enemy2B()
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (e2Bullet == false && enemy1Bool == true)
            {
                e2BulletX = e2X + 1;
                if(e2Y > 0)
                {
                    e2BulletY = e2Y - 1; 
                }
                gotoxy(e2BulletX, e2BulletY);
                Console.Write("<");
                e2Bullet = true;
            }
        }
        static void enemy2Bupdate()
        {
            if (e2Bullet)
            {
                gotoxy(e2BulletX, e2BulletY - e2count);
                Console.Write("<");
                gotoxy(e2BulletX, e2BulletY - e2count + 1);
                Console.Write(" ");
                if (e2BulletY - e2count == 2 || (e2BulletY - e2count == pY + 14 && e2BulletX <= pX + 3 && e2BulletX >= pX) || (pX + 1 == e2BulletX && pY <= e2BulletY - e2count && pY + 14 >= e2BulletY - e2count))
                {
                    if (e2BulletY - e2count == 2)
                    {
                        gotoxy(e2BulletX, 2);
                        Console.Write(" ");
                    }
                    else
                    {
                        playerHealth--;
                        gotoxy(e2BulletX, e2BulletY - e2count);
                        Console.Write(" ");
                    }

                    e2Bullet = false;
                    e2count = 0;
                }
            }
        }
        static void moveEnemy1()
        {
            if (enemy1Bool)
            {
                if (horizontal1)
                {

                    if (mazeA[e2X, e2Y - 1] == '>')
                    {
                        horizontal1 = false;
                    }
                    if (mazeA[e2X, e2Y - 1] == ' ')
                    {
                        if (mazeA[e2X, e2Y] == '.')
                        {
                            gotoxy(e2X, e2Y);
                            Console.Write(".");
                        }
                        else if (mazeA[e2X, e2Y] == ' ')
                        {
                            gotoxy(e2X, e2Y);
                            Console.Write(" ");
                        }
                        else if (mazeA[e2X, e2Y] == '*')
                        {
                            gotoxy(e2X, e2Y);
                            Console.Write("*");
                        }
                        e2Y = e2Y - 1;
                        enemy1Display();
                    }
                    gotoxy(e2X + 1, e2Y + 13);
                    Console.Write(" ");
                    gotoxy(e2X + 2, e2Y + 13);
                    Console.Write(" ");
                }
            }
        }
        static void enemy3B()
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (e3Bullet == false && enemy2Bool == true)
            {
                e3BulletX = e3X + 1;
                e3BulletY = e3Y - 1;
                gotoxy(e3BulletX, e3BulletY);
                Console.Write("<");
                e3Bullet = true;
            }
        }
        static void enemy3Bupdate()
        {
            if (e3Bullet)
            {
                gotoxy(e3BulletX, e3BulletY - e3count);
                Console.Write("<");
                gotoxy(e3BulletX, e3BulletY - e3count + 1);
                Console.Write(" ");
                if (e3BulletY - e3count == 1 || (e3BulletY - e3count == pY + 14 && e3BulletX <= pX + 3 && e3BulletX >= pX) || (pX + 1 == e3BulletX && pY <= e3BulletY - e3count && pY + 14 >= e3BulletY - e3count))
                {
                    if (e3BulletY - e3count == 1)
                    {
                        gotoxy(e3BulletX, 1);
                        Console.Write(" ");
                    }
                    else
                    {
                        playerHealth--;
                        gotoxy(e3BulletX, e3BulletY - e3count);
                        Console.Write(" ");
                    }

                    e3Bullet = false;
                    e3count = 0;
                }
            }
        }
        static void moveEnemy2()
        {
            if (enemy2Bool)
            {
                if (horizontal2)
                {

                    if (mazeA[e3X, e3Y - 1] == '>')
                    {
                        horizontal1 = false;
                    }
                    if (mazeA[e3X, e3Y - 1] == ' ')
                    {
                        
                        if (mazeA[e3X, e3Y] == ' ')
                        {
                            gotoxy(e3X, e3Y);
                            Console.Write(" ");
                        }
                        e3Y--;
                        enemy2Display();
                    }
                    gotoxy(e3X + 1, e3Y + 13);
                    Console.Write(" ");
                    gotoxy(e3X + 2, e3Y + 13);
                    Console.Write(" ");
                }
            }
        }
        static void game_break()
        {
            string temp;
            if ((e1Y - (pY + 7) >= 0 && e1Y - (pY + 7) < 8 && e1X + 4 - pX > 0 && e1X + 4 - pX < 8) ||
                (e2Y - (pY + 7) >= 0 && e2Y - (pY + 7) < 8 && e2X + 4 - pX > 0 && e2X + 4 - pX < 8) ||
                (e3Y - (pY + 7) >= 0 && e3Y - (pY + 7) < 8 && e3X + 4 - pX > 0 && e3X + 4 - pX < 8) ||
                (pX == e1X + 3 && pY + 14 >= e1Y && pY <= e1Y + 11) ||
                (pX == e2X + 3 && pY + 14 >= e2Y && pY <= e2Y + 11) ||
                (pX + 3 == e1X && pY + 14 >= e1Y && pY <= e1Y + 14) ||
                (pX + 3 == e3X && pY + 14 >= e3Y && pY <= e3Y + 14) ||
                ((e1Y + 14) - (pY) >= 0 && (e1Y + 14) - (pY) < 8 && e1X + 4 - pX > 0 && e1X + 4 - pX < 8) ||
                ((e2Y + 14) - (pY) >= 0 && (e2Y + 14) - (pY) < 8 && e2X + 4 - pX > 0 && e2X + 4 - pX < 8) ||
                ((e3Y + 14) - (pY) >= 0 && (e3Y + 14) - (pY) < 8 && e3X + 4 - pX > 0 && e3X + 4 - pX < 8))
            {
                erasePlayer();
                pX = 11;
                pY = 1;
                playerDisplay();
                playerLife--;
            }
            if (playerHealth == 0)
            {
                playerHealth = 3;
                erasePlayer();
                pX = 11;
                pY = 1;
                playerDisplay();
                playerLife--;
            }
            if (playerLife == 0 || e1Y == 2 || e2Y == 3 || e3Y <= 2)
            {
                eraseEnemy1();
                eraseEnemy2();
                eraseEnemy3();
                Console.Clear();
                //Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("You Lose!");
                Console.Write("Enter any key to continue : ");
                temp = Console.ReadLine();
                gameBreak = false;
            }
            if (enemyBool == false && enemy1Bool == false && enemy2Bool == false)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("You Won!");
                Console.Write("Enter any key to continue : ");
                temp = Console.ReadLine();
                gameBreak = false;
            }
        }
        static void life_healthDisplay()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            gotoxy(0, 3);
            Console.Write("Life's : " + playerLife);
            gotoxy(0, 30);
            Console.Write("Health : " + playerHealth);
        }
        static void lifeEnemy()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            gotoxy(0, 141);
            Console.Write("Enemy 1 health : " + enemy1Health);
            if (enemy1Health < 10)
            {
                gotoxy(0, 159);
                Console.Write(" ");
            }
            gotoxy(2, 141);
            Console.Write("Enemy 2 health : " + enemy2Health);
            if (enemy2Health < 10)
            {
                gotoxy(2, 159);
                Console.Write(" ");
            }
            gotoxy(4, 141);
            Console.Write("Enemy 3 health : " + enemy3Health);
            if (enemy3Health < 10)
            {
                gotoxy(4, 159);
                Console.Write(" ");
            }
        }
        static void scoreDisplay()
        {
            gotoxy(29, 70);
            Console.Write("Score : " + score);
        }
        static string instructMenu()
        {
            string op1 = " ";
            header();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. Keys.");
            Console.WriteLine("2. Instructions.");
            Console.WriteLine("3. Exit");
            Console.Write("Enter any option : ");
            op1 = Console.ReadLine();
            return op1;
        }
    }
}
