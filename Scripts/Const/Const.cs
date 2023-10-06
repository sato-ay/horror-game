using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Const
{
    /// <summary>
    /// 共通関数
    /// </summary>
    public static class GlobalConst
    {
        public const string START_ROOM = "701";
        public const string ROOM_NAME = "HospitalRoom";
        public const string CLOSE_TEXT = "Enterで閉じる";
        public const string HOLD_TAG = "HoldItem";
        public const string RELEASE_TEXT = "Release";
        public const string CLOSE_KEY = "return";
        public const string ENT_TEXT = "号室の患者さんは退院されたんだった。ここには用はないかな。";
        public const int START_SANITY = 100;
        public const int END_SANITY = 0;

        public static string Story(string str)
        {
            var story = new Dictionary<string, string>()
            {
                {"p1", "「恐怖症」とは――――特定のある一つのものに対して、心理学的および生理学的に異常な恐怖を感じる症状のことをいう。\r\n\r\n"},
                {"p2", "私も実は、恐怖症あるんだよね……先の尖ったものがすごく怖い。\r\nそれでも、夜勤専従看護師として働いている。\r\n\r\n"},
                {"p3", "看護師なのに、先端恐怖症なんて信じられないよね。\r\n\r\n"},
                {"p4", "……そういえば、なんで先端恐怖症になったんだっけ。\r\n最近、忘れっぽくて嫌になる…\r\n\r\n"},
                {"p5", "今日も仕事か…何事もなく終えて、早く帰ろう。"},
                {"e1", "「ＸＸ病院付近にてバラバラにされた女性の遺体が発見されてから1週間以上が経過した。付近の防犯カメラに映っていた40~50歳代とみられる男性の捜索を――――…」\r\n\r\n"},
                {"e2", "「1週間経ったんだね…。むごい事件だったね」\r\n"},
                {"e3", "「本当にね…あんなにいい子だったのに……」\r\n"},
                {"e4", "「仕事もできる子だったから、夜勤で顔合わせるとほっとしたよ…。この人とペアなら安心だって。なのになんで……」\r\n"},
                {"e5", "「………そういえば、最近……病棟で若い女性の人影見る気がする」\r\n"},
                {"e6", "「やめてよ、不謹慎だよ……」\r\n"},
                {"e7", "「…ごめん…。早く犯人、捕まるといいね…」\r\n"},
                {"e8", "「そうだね……」\r\n\r\n"},
                {"e9", "今日も仕事か…何事もなく終えて、早く帰ろう。"},
            };
            if (story.ContainsKey(str))
            {
                return story[str];
            }
            return string.Empty;
        }

        public static string CheckRoom(string roomNum)
        {
            var roomCall = new Dictionary<string, string>()
            {
                {"701", roomNum + "号室からだ…伺いまーす"},
                {"702", roomNum + "号室だ。何だろう"},
                {"703", roomNum + "号室ね"},
                {"704", roomNum + "号室か…"},
                {"705", roomNum + "………行かなきゃ…"},
                {"706", roomNum + "…………"},
            };
            if (roomCall.ContainsKey(roomNum))
            {
                return roomCall[roomNum];
            }
            return string.Empty;
        }

        public static string RoomDialogue(string roomNum)
        {
            var dialogue = new Dictionary<string, string>()
            {
                {"701","何もない。何だったんだろう。戻ろうかな"},
                {"702","………小さい頃の記憶、覚えてる"},
                {"703","看護師は…人の役に立ててる実感がある"},
                {"704","なんで忘れちゃったんだっけ"},
                {"705","刃物がたくさん…怖い……怖いよ"},
                {"706","…なに、これ……"},
            };
            if (dialogue.ContainsKey(roomNum))
            {
                return dialogue[roomNum];
            }
            return string.Empty;
        }

        public static string RoomChart(string roomNum)
        {
            var roomChart = new Dictionary<string, string>()
            {
                {"701", "生は死、死は生。"},
                {"702", "覚えていますか？"},
                {"703", "楽しい思い出"},
                {"704", "知らないことは怖い"},
                {"705", "死ぬのは怖い？"},
                {"706", "死を恐れるな。"},
            };

            if (roomChart.ContainsKey(roomNum))
            {
                return roomChart[roomNum];
            }
            return string.Empty;
        }

        public static string SetDialogue(string dialogueInfo)
        {
            var dialogue = new Dictionary<string, string>()
            {
                {"Call","ナースコールが鳴ってる。とらなきゃ"},

                {"Bear0","懐かしい。お気に入りのクマさんだ"},
                {"Bear1","小さいころ、身体が弱くてよく入院してて"},
                {"Bear2","お友達相手になってもらってたなぁ"},
                {"Bear3","そうだ…あの頃看護師さんにやさしくしてもらえて"},
                {"Bear4","それで看護師目指そうと思ったんだ"},

                {"Statue","戴帽式を思い出すなぁ…"},

                {"Face","え…わたしの…頭……？"},

                {"TV0","何か大切なこと忘れてるような…"},
                {"TV1","なんだっけ…？"},
                {"TV2","記憶が曖昧だ"},
                {"TV3","………"},

                {"Fear0","怖い…怖いよ…"},
                {"Fear1","みたくない…"},
                {"Fear2","なんでこんなに…"},
                {"Fear3","嫌だ…"},

                {"Surprised1","なに、今の声…？"},
                {"Surprised2","誰の声…？"},
                {"Surprised3","なんなの？！気持ちわるい…"},
            };
            if (dialogue.ContainsKey(dialogueInfo))
            {
                return dialogue[dialogueInfo];
            }
            return string.Empty;
        }

        public static string ActionName(string objName) 
        {
            var name = new Dictionary<string, string>()
            {
                {"Door","E:開閉"},
                {"Food","E:食べる"},
                {"Chart","E:情報収集"},
                {"Pick","E:拾う"},
                {"Release","E:はなす"},
                {"Put","E:設置"},
                {"Item","E:取得"},
                {"Check","E:調べる"},
                {"Phone","E:出る"},
            };

            if (name.ContainsKey(objName))
            {
                return name[objName];
            }

            return null;
        }

        public static string ItemAction(string selectNum) 
        {
            var name = new Dictionary<string, string>()
            {
                {"0","使用する"},
                {"1","破棄する"},
                {"2","キャンセル"},
            };

            return name[selectNum];
        }
    }
}
