﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordRelayGame
{
    public struct Letter
    {
        public bool IsEmpty;
        public char Chosung, Jwungsung, Jongsung;
        public Letter(char cho, char jwung, char jong)
        {
            IsEmpty = false;
            Chosung = cho;
            Jwungsung = jwung;
            Jongsung = jong;
        }

        public Letter(bool empty)
        {
            IsEmpty = empty;
            Chosung = Jwungsung = Jongsung = ' ';
        }
    }
    public static class Korean
    {
        public static readonly string 초성 = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
        public static readonly string 중성 = "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ";
        public static readonly string 종성 = " ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ";

        // ㄱ ㄲ ㄴ ㄷ ㄸ ㄹ ㅁ ㅂ ㅃ ㅅ ㅆ ㅇ ㅈ ㅉ ㅊ ㅋ ㅌ ㅍ ㅎ
        private static int[] ChoSung ={ 0x3131, 0x3132, 0x3134, 0x3137, 0x3138, 0x3139, 0x3141
            , 0x3142, 0x3143, 0x3145, 0x3146, 0x3147, 0x3148, 0x3149, 0x314a
            , 0x314b, 0x314c, 0x314d, 0x314e };

        // ㅏ ㅐ ㅑ ㅒ ㅓ ㅔ ㅕ ㅖ ㅗ ㅘ ㅙ ㅚ ㅛ ㅜ ㅝ ㅞ ㅟ ㅠ ㅡ ㅢ ㅣ
        private static int[] JwungSung = {   0x314f, 0x3150, 0x3151, 0x3152, 0x3153, 0x3154, 0x3155
            , 0x3156, 0x3157, 0x3158, 0x3159, 0x315a, 0x315b, 0x315c, 0x315d, 0x315e
            , 0x315f, 0x3160, 0x3161, 0x3162, 0x3163 };

        // ㄱ ㄲ ㄳ ㄴ ㄵ ㄶ ㄷ ㄹ ㄺ ㄻ ㄼ ㄽ ㄾ ㄿ ㅀ ㅁ ㅂ ㅄ ㅅ ㅆ ㅇ ㅈ ㅊ ㅋ ㅌ ㅍ ㅎ
        private static int[] JongSung = { 0, 0x3131, 0x3132, 0x3133, 0x3134, 0x3135, 0x3136
            , 0x3137, 0x3139, 0x313a, 0x313b, 0x313c, 0x313d, 0x313e, 0x313f
            , 0x3140, 0x3141, 0x3142, 0x3144, 0x3145, 0x3146, 0x3147, 0x3148
            , 0x314a, 0x314b, 0x314c, 0x314d, 0x314e };

        public static Letter ParseChar(char data)
        {
            int a, b, c; //자소버퍼 초성중성종성순
            if (data >= 0xAC00 && data <= 0xD7A3) //한글일 경우만 분리 시행
            {
                c = data - 0xAC00;
                a = c / (21 * 28);
                c = c % (21 * 28);
                b = c / 28;
                c = c % 28;

                return new Letter((char)ChoSung[a], (char)JwungSung[b], JongSung[c] == '\0' ? ' ' : (char)JongSung[c]);
            }
            else return new Letter(true);
        }

        public static char MergeLetter(Letter letter)
        {
            int unicode;
            int firstKRCode = 0xAC00;

            int chosungIndex, jwungSungIndex, jongSungIndex;

            chosungIndex = 초성.IndexOf(letter.Chosung);
            jwungSungIndex = 중성.IndexOf(letter.Jwungsung);
            jongSungIndex = 종성.IndexOf(letter.Jongsung);

            unicode = firstKRCode + (chosungIndex * 21 + jwungSungIndex) * 28 + jongSungIndex;

            char temp = Convert.ToChar(unicode);

            return temp;
        }

        public static Letter TwoVoice(Letter letter)
        {
            if (letter.Chosung == 'ㄹ') letter.Chosung = 'ㄴ';
            if(letter.Chosung == 'ㄴ')
            {
                switch (letter.Jwungsung)
                {
                    case 'ㅣ':
                    case 'ㅑ':
                    case 'ㅕ':
                    case 'ㅛ':
                    case 'ㅠ':
                        letter.Chosung = 'ㅇ';
                        break;
                }
            }
            return letter;
        }
    }
}
