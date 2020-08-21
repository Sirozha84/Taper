using System.Linq;

namespace Taper
{
    static class Basic
    {
        public static string Program(byte[] data, bool spaces)
        {
            if (data == null) return "";
            string[] Symbols = {"","","","","","","","","","","","","","","","","","","","","","","","","","","","","","", //0-29
                            "","","","","","","","","","","","","","","","","","","","","","","","","","","","","","", //30-59
                            "","","","","","","","","","","","","","","","","","","","","","","","","","","","","","", //60-89
                            "","","","","","","","","","","","","","","","","","","","","","","","","","","","","","", //90-119
                            "","","","","","","","","","","","","","","","","","","","","","","","","","","","","","", //120-149
                            "","","","","","","","","","","","","","",""," RND "," INKEY$ "," PI "," FN "," POINT ", //150-169
                            " SCREEN$ "," ATTR "," AT "," TAB "," VAL$ "," CODE "," VAL "," LEN "," SIN "," COS ", //170-179
                            " TAN "," ASN "," ACS "," ATN "," LN "," EXP "," INT "," SQR "," SGN "," ABS ", //180-189
                            " PEEK "," IN "," USR "," STR$ "," CHR$ "," NOT "," BIN "," OR "," AND ","<=", //190-199
                            ">=","<>"," LINE ","THEN "," TO "," STEP "," DEF FN "," CAT "," FORMAT ","", //200-209
                            ""," OPEN# "," CLOSE# "," MARGE "," VERIFY "," BEEP "," CIRCLE "," INK "," PAPER "," FLASH ", //210-219
                            " BRIGHT "," INVERSE "," OVER "," OUT "," LPRINT "," LLIST "," STOP "," READ "," DATA "," RESTORE ", //220-229
                            " NEW "," BORDER "," CONTINUE "," DIM "," REM "," FOR "," GO TO "," GO SUB "," INPUT "," LOAD ", //230-239
                            " LIST "," LET "," PAUSE "," NEXT "," POKE "," PRINT "," PLOT "," RUN "," SAVE "," RANDOMIZE ", //240-249
                            " IF "," CLS "," DRAW "," CLEAR "," RETURN "," COPY " //250-255
                           };
            
            string text = "";
            bool newstring = true;
            for (int i = 1; i < data.Count() - 1; i++)
            {

                if (newstring)
                {
                    int num = (data[i] * 256 + data[i + 1]);
                    if (num > 9999) break;
                    if (num < 1000) text += " ";
                    if (num < 100) text += " ";
                    if (num < 10) text += " ";
                    text += num.ToString();
                    i += 3;
                    newstring = false;
                }
                else
                {
                    byte b = data[i];
                    if (b == 13)
                    {
                        text += (char)13;
                        text += (char)10;
                        if (spaces)
                        {
                            text += (char)13;
                            text += (char)10;
                        }
                        newstring = true;
                    }
                    if (b == 14) i += 5;
                    if (b >= 32 & (b < 165)) text += (char)b;
                    if (b >= 165) text += Symbols[b];
                }
            }
            return text;
        }
    }
}
