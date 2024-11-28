using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using QRCoder;
using Shouldly;
using Xunit;
using ECCLevel = QRCoder.QRCodeGenerator.ECCLevel;


namespace QRCoderTests;


public class QRGeneratorTests
{
    [Fact]
    public void validate_antilogtable()
    {
        var gen = new QRCodeGenerator();

        var checkString = string.Empty;
        var gField = gen.GetType().GetField("_galoisFieldByExponentAlpha", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null).ShouldBeOfType<int[]>();
        gField.Length.ShouldBe(256);
        for (int i = 0; i < gField.Length; i++)
        {
            checkString += i + "," + gField[i] + ",:";
        }
        checkString.ShouldBe("0,1,:1,2,:2,4,:3,8,:4,16,:5,32,:6,64,:7,128,:8,29,:9,58,:10,116,:11,232,:12,205,:13,135,:14,19,:15,38,:16,76,:17,152,:18,45,:19,90,:20,180,:21,117,:22,234,:23,201,:24,143,:25,3,:26,6,:27,12,:28,24,:29,48,:30,96,:31,192,:32,157,:33,39,:34,78,:35,156,:36,37,:37,74,:38,148,:39,53,:40,106,:41,212,:42,181,:43,119,:44,238,:45,193,:46,159,:47,35,:48,70,:49,140,:50,5,:51,10,:52,20,:53,40,:54,80,:55,160,:56,93,:57,186,:58,105,:59,210,:60,185,:61,111,:62,222,:63,161,:64,95,:65,190,:66,97,:67,194,:68,153,:69,47,:70,94,:71,188,:72,101,:73,202,:74,137,:75,15,:76,30,:77,60,:78,120,:79,240,:80,253,:81,231,:82,211,:83,187,:84,107,:85,214,:86,177,:87,127,:88,254,:89,225,:90,223,:91,163,:92,91,:93,182,:94,113,:95,226,:96,217,:97,175,:98,67,:99,134,:100,17,:101,34,:102,68,:103,136,:104,13,:105,26,:106,52,:107,104,:108,208,:109,189,:110,103,:111,206,:112,129,:113,31,:114,62,:115,124,:116,248,:117,237,:118,199,:119,147,:120,59,:121,118,:122,236,:123,197,:124,151,:125,51,:126,102,:127,204,:128,133,:129,23,:130,46,:131,92,:132,184,:133,109,:134,218,:135,169,:136,79,:137,158,:138,33,:139,66,:140,132,:141,21,:142,42,:143,84,:144,168,:145,77,:146,154,:147,41,:148,82,:149,164,:150,85,:151,170,:152,73,:153,146,:154,57,:155,114,:156,228,:157,213,:158,183,:159,115,:160,230,:161,209,:162,191,:163,99,:164,198,:165,145,:166,63,:167,126,:168,252,:169,229,:170,215,:171,179,:172,123,:173,246,:174,241,:175,255,:176,227,:177,219,:178,171,:179,75,:180,150,:181,49,:182,98,:183,196,:184,149,:185,55,:186,110,:187,220,:188,165,:189,87,:190,174,:191,65,:192,130,:193,25,:194,50,:195,100,:196,200,:197,141,:198,7,:199,14,:200,28,:201,56,:202,112,:203,224,:204,221,:205,167,:206,83,:207,166,:208,81,:209,162,:210,89,:211,178,:212,121,:213,242,:214,249,:215,239,:216,195,:217,155,:218,43,:219,86,:220,172,:221,69,:222,138,:223,9,:224,18,:225,36,:226,72,:227,144,:228,61,:229,122,:230,244,:231,245,:232,247,:233,243,:234,251,:235,235,:236,203,:237,139,:238,11,:239,22,:240,44,:241,88,:242,176,:243,125,:244,250,:245,233,:246,207,:247,131,:248,27,:249,54,:250,108,:251,216,:252,173,:253,71,:254,142,:255,1,:");

        var gField2 = gen.GetType().GetField("_galoisFieldByIntegerValue", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null).ShouldBeOfType<int[]>();
        gField2.Length.ShouldBe(256);
        var checkString2 = string.Empty;
        for (int i = 0; i < gField2.Length; i++)
        {
            checkString2 += i + "," + gField2[i] + ",:";
        }
        checkString2.ShouldBe("0,0,:1,0,:2,1,:3,25,:4,2,:5,50,:6,26,:7,198,:8,3,:9,223,:10,51,:11,238,:12,27,:13,104,:14,199,:15,75,:16,4,:17,100,:18,224,:19,14,:20,52,:21,141,:22,239,:23,129,:24,28,:25,193,:26,105,:27,248,:28,200,:29,8,:30,76,:31,113,:32,5,:33,138,:34,101,:35,47,:36,225,:37,36,:38,15,:39,33,:40,53,:41,147,:42,142,:43,218,:44,240,:45,18,:46,130,:47,69,:48,29,:49,181,:50,194,:51,125,:52,106,:53,39,:54,249,:55,185,:56,201,:57,154,:58,9,:59,120,:60,77,:61,228,:62,114,:63,166,:64,6,:65,191,:66,139,:67,98,:68,102,:69,221,:70,48,:71,253,:72,226,:73,152,:74,37,:75,179,:76,16,:77,145,:78,34,:79,136,:80,54,:81,208,:82,148,:83,206,:84,143,:85,150,:86,219,:87,189,:88,241,:89,210,:90,19,:91,92,:92,131,:93,56,:94,70,:95,64,:96,30,:97,66,:98,182,:99,163,:100,195,:101,72,:102,126,:103,110,:104,107,:105,58,:106,40,:107,84,:108,250,:109,133,:110,186,:111,61,:112,202,:113,94,:114,155,:115,159,:116,10,:117,21,:118,121,:119,43,:120,78,:121,212,:122,229,:123,172,:124,115,:125,243,:126,167,:127,87,:128,7,:129,112,:130,192,:131,247,:132,140,:133,128,:134,99,:135,13,:136,103,:137,74,:138,222,:139,237,:140,49,:141,197,:142,254,:143,24,:144,227,:145,165,:146,153,:147,119,:148,38,:149,184,:150,180,:151,124,:152,17,:153,68,:154,146,:155,217,:156,35,:157,32,:158,137,:159,46,:160,55,:161,63,:162,209,:163,91,:164,149,:165,188,:166,207,:167,205,:168,144,:169,135,:170,151,:171,178,:172,220,:173,252,:174,190,:175,97,:176,242,:177,86,:178,211,:179,171,:180,20,:181,42,:182,93,:183,158,:184,132,:185,60,:186,57,:187,83,:188,71,:189,109,:190,65,:191,162,:192,31,:193,45,:194,67,:195,216,:196,183,:197,123,:198,164,:199,118,:200,196,:201,23,:202,73,:203,236,:204,127,:205,12,:206,111,:207,246,:208,108,:209,161,:210,59,:211,82,:212,41,:213,157,:214,85,:215,170,:216,251,:217,96,:218,134,:219,177,:220,187,:221,204,:222,62,:223,90,:224,203,:225,89,:226,95,:227,176,:228,156,:229,169,:230,160,:231,81,:232,11,:233,245,:234,22,:235,235,:236,122,:237,117,:238,44,:239,215,:240,79,:241,174,:242,213,:243,233,:244,230,:245,231,:246,173,:247,232,:248,116,:249,214,:250,244,:251,234,:252,168,:253,80,:254,88,:255,175,:");
    }

#if !NETFRAMEWORK // [Theory] is not supported in xunit < 2.0.0
    [Theory]
    // version 1 numeric
    [InlineData("1", "KWw84nkWZLMh5LqAJ/4s/4mW/08", 21)]
    [InlineData("12", "+MdvzzZYQNF3d+6NuZGSmqmCmXY", 21)]
    [InlineData("123", "meNWffAoC6ozzXEdDpEjixvBAME", 21)]
    [InlineData("1234", "rOI2dmjilbVXsk4m2sJjAWybMto", 21)]
    [InlineData("12345", "gVrbNyJNTkLCoXhLA1g1vGUlQvI", 21)]
    [InlineData("123456", "TsdKS6PDgtq1b2stRT1C90DiGik", 21)]
    [InlineData("1234567", "pbpVWmVQPjeRSPk/8GIlAbtPPlY", 21)]
    [InlineData("12345678", "ng29QoMxhqMsygeU7t2Ic9RB2hk", 21)]
    [InlineData("123456789", "Xb/EHaUUUU+22a4Hm/2Sr+O1zv0", 21)]
    [InlineData("1234567890", "X0kmbmnqpAFjTuS0SQAEkphAaok", 21)]
    [InlineData("1234567890123456", "afOstf4rTgaLUaHL/Vb23vzjQFM", 21)]
    [InlineData("12345678901234567", "S0BOgmRblr9Bb6Lpkf62WfYIj58", 21)]
    // version 2 numeric
    [InlineData("123456789012345678", "qs5j+bBK3fdRgoQg1N00vUF7f0g", 25)]
    [InlineData("1234567890123456789012345678901234", "mu6wUZp+uXqXGyYFduQZt38Jbu0", 25)]
    // version 3 numeric
    [InlineData("12345678901234567890123456789012345", "AiWiTB6xreLc514aHw4StDsomvk", 29)]
    [InlineData("1234567890123456789012345678901234567890123456789012345678", "WNLD0ved5WdysFG1uqNBBV7ItKI", 29)]
    // version 4 numeric
    [InlineData("12345678901234567890123456789012345678901234567890123456789", "cV6Rijj6q3f/dUlDVOZD3DafrMM", 33)]

    // version 1 alphanumeric
    [InlineData("A", "YUpoycThbE3FwkkHaO6GYqe9V+c", 21)]
    [InlineData("AB", "UnUHZDgLdnYIy0iN31sguw2qbh8", 21)]
    [InlineData("ABC", "GVB3xcSMAawwOZlq0hiF9hqVldg", 21)]
    [InlineData("ABCD", "jATOwpwGVWpou3WtKiq4DX4jWkk", 21)]
    [InlineData("ABCDE", "m/LrK4iP22OW9RmC2r2dnDFd4wE", 21)]
    [InlineData("ABCDEF", "p8acVHkm3z751oh5yK4mBBRMUuE", 21)]
    [InlineData("ABCDEFG", "md1jFcZSqDmQ2KeFTwKJVFrfZko", 21)]
    [InlineData("ABCDEFGH", "XvL+fpHNqQQ2FHUCXraQw77DGns", 21)]
    [InlineData("ABCDEFGHI", "k+DTXI3yht473k9lvYLMdHf0V/0", 21)]
    [InlineData("ABCDEFGHIJ", "f9uful+85iSlJVJAFc5zEk04eMc", 21)]
    // version 2 alphanumeric
    [InlineData("ABCDEFGHIJK", "qiXut4Jz2zX8Tl9DSXxIo+bqjZY", 25)]
    [InlineData("ABCDEFGHIJKL", "wSpjZmpo9CEjlxlYF18xEa6BMYM", 25)]
    [InlineData("ABCDEFGHIJKLM", "utCYtAtJp+GdKS6y6A7jQuES6kA", 25)]
    [InlineData("ABCDEFGHIJKLMN", "jhzNewJNcC875mlYI31BkVgx0G0", 25)]
    [InlineData("ABCDEFGHIJKLMNO", "eWCSdyn3EH3uFDig1a0NUYZFlO0", 25)]
    [InlineData("ABCDEFGHIJKLMNOP", "glV9FE+UQPDkplgOXFhk3Ll29pI", 25)]
    [InlineData("ABCDEFGHIJKLMNOPQ", "Crlq92Pqiw8X9EIG6KFCvStNQuI", 25)]
    [InlineData("ABCDEFGHIJKLMNOPQR", "lIG8/YBsD0uK+Dop6QfvD7IFdAY", 25)]
    [InlineData("ABCDEFGHIJKLMNOPQRS", "rsDPSYotVP65kLAxP3fzbGqt6wc", 25)]
    [InlineData("ABCDEFGHIJKLMNOPQRST", "yOb1jCKlj3wqdczHEPRdWxafvNU", 25)]
    // version 3 alphanumeric
    [InlineData("ABCDEFGHIJKLMNOPQRSTU", "U4YNhfjgJbprsTurHs4E7Mi2sS8", 29)]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUV", "s9WCvPzYhXrwzNoVbDocQtse1w8", 29)]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVW", "6vZ9rDMy1GUVEcnL2ErJFOxqWI0", 29)]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWX", "C9LlmjRV+TPDkR03MlgDvo/DP+U", 29)]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXY", "+EtALGm0mrDrnZVW54WdXG612P0", 29)]
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ789012345", "3nFUvZ/Aa2wUdAj1zlMmSu9x4kU", 29)]
    // version 4 alphanumeric
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ7890123456", "9K6EinxynS2KRum46iQsVoPgM7k", 33)]

    // version 1 binary
    [InlineData("a", "zd6kApf0BQSE5W8fhCijkz6wzKA", 21)]
    [InlineData("ab", "mXAUC/dqcqqj6SzC+Us6NiYzzCM", 21)]
    [InlineData("abc", "6Y3HGyOFxhZUYINks/hzE2DjulM", 21)]
    [InlineData("abcd", "ssvlMmEub85t1d0R/aZG+Qgpa+0", 21)]
    [InlineData("abcde", "At93DjDyAtIkTCpOwD3p/lSqFz4", 21)]
    [InlineData("abcdef", "Q8BU1lCJJA/UesKGvszQTptskSk", 21)]
    [InlineData("abcdefg", "2AceZvcjIpEBCh5FIc1esXsaEY4", 21)]
    // version 2 binary
    [InlineData("abcdefgh", "kl8bx15B4ApauURa0nlD71NPk+I", 25)]
    [InlineData("abcdefghi", "v9rI0/2a8nYxM9MerzxSCwT6EKs", 25)]
    [InlineData("abcdefghij", "yE59+LfDLQCt2VXCuhnz9aFIoMk", 25)]
    [InlineData("abcdefghijk", "nVne+lyjPV5XDMKqa0+oNfxZTgI", 25)]
    [InlineData("abcdefghijkl", "QUeDHmjQDyHvbe5r8tViXxBcHv0", 25)]
    [InlineData("abcdefghijklm", "WtN1tTti8hV4vvH5vX6obIPdjpM", 25)]
    [InlineData("abcdefghijklmn", "AT5SPNUPL3wG0r4XXPBzSAK2sIE", 25)]
    // version 3 binary
    [InlineData("abcdefghijklmno", "N04AFOJlXQRjeXijWoy4rsBNZGg", 29)]
    [InlineData("abcdefghijklmnop", "a4tgwBApGX0+P4yiwR/wUtLAxQA", 29)]
    [InlineData("abcdefghijklmnopq", "MFLd+exCRrUZkqfw5UTqY2QZ1n0", 29)]
    [InlineData("abcdefghijklmnopqr", "aSYOJXfFAjxrtBWnBQqHWrC8Zv0", 29)]
    [InlineData("abcdefghijklmnopqrs", "K9Uic6+NO2rPy/Hfo4fEhXkUw2Q", 29)]
    [InlineData("abcdefghijklmnopqrst", "eKVJvIH8J1waEb3UHRdXYAWLezc", 29)]
    [InlineData("abcdefghijklmnopqrstu", "ylmFLWV1grM2MoTFpdngo05fdyI", 29)]
    [InlineData("abcdefghijklmnopqrstuv", "Z0IETxnf8x+pTU2nuj1hxg2G/pQ", 29)]
    [InlineData("abcdefghijklmnopqrstuvw", "oHzGRWtkI+a30AF5JILT6HON7Zc", 29)]
    [InlineData("abcdefghijklmnopqrstuvwx", "NRIoT6rGd3HWrBq4JhBWvbwYp9g", 29)]
    // version 4 binary
    [InlineData("abcdefghijklmnopqrstuvwxy", "RMSyMOBpdBYphJPkXR/xA/ekPoo", 33)]
    [InlineData("abcdefghijklmnopqrstuvwxyz", "BAMiY251UapecfI+v2C3cX2EBH4", 33)]
    // version 5 binary
    [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghi", "yV9Cd3xiW2HRzSIMq3eLTIrdqVQ", 37)]
    [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghij", "mV/R+gMAwN+lO8ByXhU5IyZp39Y", 37)]
    [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijk", "sIb5hBRamy+MIgaFakCCGnDM9yU", 37)]
    [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijkl", "2/PZLsxe4c/R/tStrn9pcB8EUOQ", 37)]
    [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijklm", "KFReyVpr4rq5c+ELZBt/ZuhQkYM", 37)]
    [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijklmn", "IwlWmCnXp0FSr+WUp/igMuQKHQo", 37)]
    // version 7 binary
    [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefg", "bisFBjhANRxoF9JDCBSODvsSKqk", 45)]
    [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefgh", "MwRnhkqr5CM17xtcQycytd+d+Fs", 45)]
    [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghi", "PFlhVI0La4/qOweduCP2WfedoCQ", 45)]
    [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghij", "ZwMdK51id9A99IxefE01o5ZtkN4", 45)]
    [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijk", "HM6MMwWDmJ0PTLLBWzIo7Q0YvmA", 45)]
    public void can_encode_various_strings_ecc_h(string input, string expectedHash, int expectedSize)
    {
        var gen = new QRCodeGenerator();
        var qrData = gen.CreateQrCode(input, ECCLevel.H);
        (qrData.ModuleMatrix.Count - 8).ShouldBe(expectedSize); // exclude padding
        var result = string.Join("", qrData.ModuleMatrix.Select(x => x.ToBitString()).ToArray());
        var hash = System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(result));
        var hashString = Convert.ToBase64String(hash);
        hashString.TrimEnd('=').ShouldBe(expectedHash);
    }

    [Theory]
    // Version 1
    [InlineData(17, ECCLevel.L, "iOaoY7YsHAYGNRn+Tpnt74IQoVw=", 21)]
    [InlineData(14, ECCLevel.M, "JV2XYoq8nt/lWipVkwvvSbNvFVQ=", 21)]
    [InlineData(11, ECCLevel.Q, "44vd54SCPFEevWN9PKC5swEpVmU=", 21)]
    [InlineData(7, ECCLevel.H, "FvR2FAU+4sltHMS969/Y1FAHZRA=", 21)]
    // Version 2
    [InlineData(32, ECCLevel.L, "vM4eIrKbner3NxjRznd6kZLbyck=", 25)]
    [InlineData(26, ECCLevel.M, "mesaTID5N92ar2fyElorp7zcSVg=", 25)]
    [InlineData(20, ECCLevel.Q, "mg1Z+VPVuxMoGwvgRzJrW4NHehA=", 25)]
    [InlineData(14, ECCLevel.H, "6T0I6Z9AmN9yNIvan82NQqAMATc=", 25)]
    // Version 3
    [InlineData(53, ECCLevel.L, "O8Wkal/iDmCnENBubqR0HXOo/RY=", 29)]
    [InlineData(42, ECCLevel.M, "NCjzwIm3l5urwU4EcFGK5DD1y9U=", 29)]
    [InlineData(32, ECCLevel.Q, "kR+4FNybKyAiGDLPDnzIslvjypQ=", 29)]
    [InlineData(24, ECCLevel.H, "ZamyoGRJG7mGMY6nzz8r3+q0z18=", 29)]
    // Version 4
    [InlineData(78, ECCLevel.L, "P3Dx7gDjD2L94wPyL23AO/z5+Yk=", 33)]
    [InlineData(62, ECCLevel.M, "wDUcQmVTCcTx6sStDlPG4Wsn5FU=", 33)]
    [InlineData(46, ECCLevel.Q, "3mYP2cHqQysy93UC4NGUnNwfj10=", 33)]
    [InlineData(34, ECCLevel.H, "5ovh+NFiGh6soAuNNTWqxenM8cw=", 33)]
    // Version 5
    [InlineData(106, ECCLevel.L, "0vZswNhwdcCcj2GpwucfkcnlG/M=", 37)]
    [InlineData(84, ECCLevel.M, "AWlV4NsbRtkmH2/vfBxTahIiG7U=", 37)]
    [InlineData(60, ECCLevel.Q, "8w35kxFqvcMajba9IvRhjbOn0Js=", 37)]
    [InlineData(44, ECCLevel.H, "+d0gLH3v9FG+w/hhv+zDm2Y3IVw=", 37)]
    // Version 6
    [InlineData(134, ECCLevel.L, "CNyyNDIylrMi97DwuNh6JAgHlw8=", 41)]
    [InlineData(106, ECCLevel.M, "z4LUkv75O26FLaVo823TMLv9Owg=", 41)]
    [InlineData(74, ECCLevel.Q, "NgCKIxbeuSt24C9M067nDGopKgU=", 41)]
    [InlineData(58, ECCLevel.H, "Bzp923oooHYWoQfETFENmb5wup0=", 41)]
    // Version 7
    [InlineData(154, ECCLevel.L, "ftMeEbWj6D0lyOBVsAnTCq0UV0s=", 45)]
    [InlineData(122, ECCLevel.M, "zif9uHXnPgo+OeIN95xU3iqcexk=", 45)]
    [InlineData(86, ECCLevel.Q, "wApf2GzMYIQlYw4ws3k6Wi1DqMU=", 45)]
    [InlineData(64, ECCLevel.H, "i8llCv2L4dwlW5E8+mswsAa+Zo4=", 45)]
    // Version 8
    [InlineData(192, ECCLevel.L, "DSph/W1Nq2VAKFxgRq0VeqTP54g=", 49)]
    [InlineData(152, ECCLevel.M, "j06phyT/k6pXqf935BuaMxUjckk=", 49)]
    [InlineData(108, ECCLevel.Q, "RufVav4xUUuL/K5ELnH3/qUrEf8=", 49)]
    [InlineData(84, ECCLevel.H, "oG538pE6ac81I2of3LzIHQ6+Dxg=", 49)]
    // Version 9
    [InlineData(230, ECCLevel.L, "LBs30yL9Rec1qFdPwKz4nBrDraY=", 53)]
    [InlineData(180, ECCLevel.M, "c0DN8hFoX6SEkVKr/yVA79SZE4g=", 53)]
    [InlineData(130, ECCLevel.Q, "XdZ3zNyz14Sq0fv9KjonZK7ok04=", 53)]
    [InlineData(98, ECCLevel.H, "9NE1egSXCdGY8AiY4LhHM6sO/jA=", 53)]
    // Version 10
    [InlineData(271, ECCLevel.L, "gyox9Nk2DCPzVeL2E1V/P5XsuNY=", 57)]
    [InlineData(213, ECCLevel.M, "iqS7CNYuwZpw47/SnM8JAcWkhCE=", 57)]
    [InlineData(151, ECCLevel.Q, "vVdh2R+yWmSeDc7iCKonTTcs4ok=", 57)]
    [InlineData(119, ECCLevel.H, "n79CbR/JZZC30sDIDjdFAgurzR4=", 57)]
    // Version 11
    [InlineData(321, ECCLevel.L, "R77LLVhO+/YE8WKmn3CV9f/I9ZY=", 61)]
    [InlineData(251, ECCLevel.M, "l/IFWD6Pkm1TZHFc4ZuFLWDrfdc=", 61)]
    [InlineData(177, ECCLevel.Q, "SxVElF8qBWe0oXXGn57CoI6iglo=", 61)]
    [InlineData(137, ECCLevel.H, "GrHJ2EiDMJ/cXpjcITvypJZZGrY=", 61)]
    // Version 12
    [InlineData(367, ECCLevel.L, "rCv4hIrv0obcHALDSvzN/5zwCfg=", 65)]
    [InlineData(287, ECCLevel.M, "mBC3lYhpNuCa2TbD/h+F6gFH8f4=", 65)]
    [InlineData(203, ECCLevel.Q, "2Gpr+HihG8dDshcf96n2lNopsiM=", 65)]
    [InlineData(155, ECCLevel.H, "eMjatzihLLJH1KZ56GAmaXyf/os=", 65)]
    // Version 13
    [InlineData(425, ECCLevel.L, "mmPOWpjyRZWVC+JRJFDpufEqbUk=", 69)]
    [InlineData(331, ECCLevel.M, "nujBdyCZyO4HoHL4uLYIucd/MA4=", 69)]
    [InlineData(241, ECCLevel.Q, "IT+VAECAcwuZqJdQft5fWo/UTMs=", 69)]
    [InlineData(177, ECCLevel.H, "7JczSXSWYg5XXPhdqLx4Lb411lU=", 69)]
    // Version 14
    [InlineData(458, ECCLevel.L, "a/hMcMmEajVBC3kj8ILzRdGR4t0=", 73)]
    [InlineData(362, ECCLevel.M, "M7D1Cm0FeVbNeiZd+yUPp/8lDfU=", 73)]
    [InlineData(258, ECCLevel.Q, "KAnpA9g4esUdsXHLBpIVvbJ/Dsw=", 73)]
    [InlineData(194, ECCLevel.H, "gs48PFtIXdBTsNB5CIDK4IopcMU=", 73)]
    // Version 15
    [InlineData(520, ECCLevel.L, "L1/Q9lMcmGeNwY4RbBTfFKk2CAQ=", 77)]
    [InlineData(412, ECCLevel.M, "7Z4o8qbi+HXAh5wSBlg/KO8VWl8=", 77)]
    [InlineData(292, ECCLevel.Q, "v52Vt1lJpaEOWkfIsmRMeF2VkZ4=", 77)]
    [InlineData(220, ECCLevel.H, "0c8GsO3CIWhcJYcQDE92+l+w7rQ=", 77)]
    // Version 16
    [InlineData(586, ECCLevel.L, "M66FY6iMOawr2JoEInl0KBKQ1nI=", 81)]
    [InlineData(450, ECCLevel.M, "5u8qfYfrBGxzyesU/xepVqwaWWw=", 81)]
    [InlineData(322, ECCLevel.Q, "QnNtWtlQDmt6cu535YqceOZAyVY=", 81)]
    [InlineData(250, ECCLevel.H, "w2O4eKcEL43ibEH/dDzbNqGDFaM=", 81)]
    // Version 17
    [InlineData(644, ECCLevel.L, "7uG763m0mGPJdY9nwquzdiR4Yu8=", 85)]
    [InlineData(504, ECCLevel.M, "FJuxPTwkgTFHIiFOKfdMMjins2Y=", 85)]
    [InlineData(364, ECCLevel.Q, "JPuf2oD8xEeJSY/bhIO7VCbDxfI=", 85)]
    [InlineData(280, ECCLevel.H, "iepqbSMD9KO0jdaBHdDD3CN/ELA=", 85)]
    // Version 18
    [InlineData(718, ECCLevel.L, "tg9fRcelrfpz1muMC3bp9Rd+d+Q=", 89)]
    [InlineData(560, ECCLevel.M, "ZiJ3ALPxKefddqcbFsaLVtaqu4M=", 89)]
    [InlineData(394, ECCLevel.Q, "TetEWsqYm2DnePzsBN2n2TZI1qw=", 89)]
    [InlineData(310, ECCLevel.H, "g3SqiNtegQKKWz0fphMJNbMnauI=", 89)]
    // Version 19
    [InlineData(792, ECCLevel.L, "G3wlYoJhuxgOMAhwlBSlenIPzQE=", 93)]
    [InlineData(624, ECCLevel.M, "zlQfupN9mxSqabm5IH0Au5UltHA=", 93)]
    [InlineData(442, ECCLevel.Q, "ZTqF6EL1yCtaRxB2/fwuOUQVyDo=", 93)]
    [InlineData(338, ECCLevel.H, "R2zVkSuv/xkSn5tz4RW/8z1Tu+U=", 93)]
    // Version 20
    [InlineData(858, ECCLevel.L, "picn/dsww2hy+2gWQCFJfWRryvM=", 97)]
    [InlineData(666, ECCLevel.M, "65jjl86TbBbuzJk6n42DYMGWVtI=", 97)]
    [InlineData(482, ECCLevel.Q, "78k1kZhh228wPC1HNLuHR2E2rXI=", 97)]
    [InlineData(382, ECCLevel.H, "cjCcbhd7GuNpePeTVeEXS11ZrXk=", 97)]
    // Version 21
    [InlineData(929, ECCLevel.L, "88t3Y6RQA+g+6r8mp9RUuMkZGP0=", 101)]
    [InlineData(711, ECCLevel.M, "Xlq1Xfk881mzrCO+Iu8kK8brGBg=", 101)]
    [InlineData(509, ECCLevel.Q, "KkN2utAu40CZuKCUN0jLWJ+Vd6o=", 101)]
    [InlineData(403, ECCLevel.H, "L44GKy1dAMakEhA4UO3rZscrjys=", 101)]
    // Version 22
    [InlineData(1003, ECCLevel.L, "QN5/o8D8VuuH8hDP+DIItgKwPb0=", 105)]
    [InlineData(779, ECCLevel.M, "K6ss7doERSJbYd2EAWWgB8q/tjQ=", 105)]
    [InlineData(565, ECCLevel.Q, "uO4I8RZ1QBgOdlV36LGFCHeNOzk=", 105)]
    [InlineData(439, ECCLevel.H, "wdAxJat/xdz5D/A53Twe7LijnHg=", 105)]
    // Version 23
    [InlineData(1091, ECCLevel.L, "ZIkAmmyIUotcDSfA4APqqBrb1WY=", 109)]
    [InlineData(857, ECCLevel.M, "7h/MVR1ognfK4SVDRmfgyy7UJVA=", 109)]
    [InlineData(611, ECCLevel.Q, "NYW8AFfV48ojWSAiZjnMYR98o78=", 109)]
    [InlineData(461, ECCLevel.H, "7rQgH3LE/YyOjpqH3XficevFSaU=", 109)]
    // Version 24
    [InlineData(1171, ECCLevel.L, "0pNvS9HfwsAOLG6/U1PXKMyoc6I=", 113)]
    [InlineData(911, ECCLevel.M, "555aDmkhsRGE2li1z81j6mVMh7s=", 113)]
    [InlineData(661, ECCLevel.Q, "UJQmA6QoWlN3r8BEF+zIFvWsoJ4=", 113)]
    [InlineData(511, ECCLevel.H, "emTvRmvoSWFRHLljWXOzvUjpLX0=", 113)]
    // Version 25
    [InlineData(1273, ECCLevel.L, "vf+HWAPl5vJXIKHrZCPHjAInuo4=", 117)]
    [InlineData(997, ECCLevel.M, "v+H4HXOL3tO5/QIsK1IYPGu+zA0=", 117)]
    [InlineData(715, ECCLevel.Q, "rc+LXSs8ILA84TabFJ5b45gX0n8=", 117)]
    [InlineData(535, ECCLevel.H, "ZidXJ+kT23SwS9+xbLZ755ATt4g=", 117)]
    // Version 26
    [InlineData(1367, ECCLevel.L, "QPNW+iYKQdJClPaZsuTUju6dsQE=", 121)]
    [InlineData(1059, ECCLevel.M, "NLTDTTGBmvzR6TOUSxTF/EY4oLI=", 121)]
    [InlineData(751, ECCLevel.Q, "MoOh9EA/7kESiuzy6YHJWcjujMM=", 121)]
    [InlineData(593, ECCLevel.H, "16reau4y5ukinTo0YfM1ToP+/nE=", 121)]
    // Version 27
    [InlineData(1465, ECCLevel.L, "NeSmv01ZOFMeRCxIu6AshMdonOM=", 125)]
    [InlineData(1125, ECCLevel.M, "4xKzxZ7KvzpFAlFmEXYXDHuKh2A=", 125)]
    [InlineData(805, ECCLevel.Q, "LWbwDKIZKwqnfJQncZM+SXJ2qmg=", 125)]
    [InlineData(625, ECCLevel.H, "ks+3sSJU4VFavu6ILO7zFVQZVqc=", 125)]
    // Version 28
    [InlineData(1528, ECCLevel.L, "h64k62PMqcq+hpKSUEFbfNwOlxY=", 129)]
    [InlineData(1190, ECCLevel.M, "H8mNI2+l1xp6LAJKc54t6BcTgXE=", 129)]
    [InlineData(868, ECCLevel.Q, "s+IrQoE960X/7R6mFQyw6w3d0C4=", 129)]
    [InlineData(658, ECCLevel.H, "ugGZT7++E0kKqEi1lOwpgGIuilU=", 129)]
    // Version 29
    [InlineData(1628, ECCLevel.L, "ZJ2nILLA7z8tcMK/NUayp0gd+Ws=", 133)]
    [InlineData(1264, ECCLevel.M, "oVnYNf73ATNLaLNV+Kcsk/kkJ5g=", 133)]
    [InlineData(908, ECCLevel.Q, "G/4ZQB/lgaaP8cfzy6tyQkA4LCk=", 133)]
    [InlineData(698, ECCLevel.H, "snR+O40DDNFn6tGio9X3Qx7Yj6I=", 133)]
    // Version 30
    [InlineData(1732, ECCLevel.L, "QnOSXQy6rgnX3mOEFilBMYagxd4=", 137)]
    [InlineData(1370, ECCLevel.M, "JV+jGwxg94rTaQB1pYEbyznyBg4=", 137)]
    [InlineData(982, ECCLevel.Q, "nI2Xd3XP9ozg+YUns5VN1JvyLqg=", 137)]
    [InlineData(742, ECCLevel.H, "YZKS3IH1kVNvndDJHxQGdRk6WNY=", 137)]
    // Version 31
    [InlineData(1840, ECCLevel.L, "jjvNCmxgZzQnl0gtfx8AXGDzr64=", 141)]
    [InlineData(1452, ECCLevel.M, "8P5rLwbI3czsK65jSnWYHK540ps=", 141)]
    [InlineData(1030, ECCLevel.Q, "Z/+XPRRYe4AL90fdeD2pd9BtEJo=", 141)]
    [InlineData(790, ECCLevel.H, "cauLAUrqzrUlDce6QvLc0QDVb8o=", 141)]
    // Version 32
    [InlineData(1952, ECCLevel.L, "heo6yDq1jE3lrJBGQRWFq6Yxw4Y=", 145)]
    [InlineData(1538, ECCLevel.M, "Owl+uFMlPWFUDa14YKPHl6Iz2rw=", 145)]
    [InlineData(1112, ECCLevel.Q, "iH25k4mUj1LZFfH9RlRU7w1mvgU=", 145)]
    [InlineData(842, ECCLevel.H, "u6fkuEiZbHyROePmzV22/fi4BUQ=", 145)]
    // Version 33
    [InlineData(2068, ECCLevel.L, "d9ATU4zloW1TWbDyFUQRnstXX80=", 149)]
    [InlineData(1628, ECCLevel.M, "jJxIdcbC4JK6ilHF3tCLUN2z12Q=", 149)]
    [InlineData(1168, ECCLevel.Q, "ODr9TeLZGwhJOkKiFTNfzxGeD5E=", 149)]
    [InlineData(898, ECCLevel.H, "FxTOLRMB5YQw1y4Z2mxTOv+5p3g=", 149)]
    // Version 34
    [InlineData(2188, ECCLevel.L, "hOS3RBFGQAIBqVxI1dJD3DMbPiM=", 153)]
    [InlineData(1722, ECCLevel.M, "6ljLKSekU2f3IZSVYt2SmkCwxcQ=", 153)]
    [InlineData(1228, ECCLevel.Q, "CMPPw3e2cUtxuPp6vyW4FLgltdg=", 153)]
    [InlineData(958, ECCLevel.H, "ZHTja2LTLTgrd9Ha0awn5HiB9Pk=", 153)]
    // Version 35
    [InlineData(2303, ECCLevel.L, "pmSX3DjwSMpia+KAM+MYi+jAyN4=", 157)]
    [InlineData(1809, ECCLevel.M, "Uk6fudf3ij96QeYbDKMqyuC7ccY=", 157)]
    [InlineData(1283, ECCLevel.Q, "Sss6YeZZjl/eA019B0vHOMAivG0=", 157)]
    [InlineData(983, ECCLevel.H, "NXJCn5THFStoGeJdQ3gqVfpSQic=", 157)]
    // Version 36
    [InlineData(2431, ECCLevel.L, "3aSKsgEZlsJ6PisZN9f55NPecHg=", 161)]
    [InlineData(1911, ECCLevel.M, "OmKYn++0akH80oMbs/CAUCoHFsY=", 161)]
    [InlineData(1351, ECCLevel.Q, "WWyI18sPU1e4z7/D6/tND0oJhps=", 161)]
    [InlineData(1051, ECCLevel.H, "m38wtnevvqtYxDfS4dM7xDMaI/M=", 161)]
    // Version 37
    [InlineData(2563, ECCLevel.L, "YV6EWRw9/HGfVO0COUoQ94uGRRg=", 165)]
    [InlineData(1989, ECCLevel.M, "/KjlKPlECYqj/pX/Dz3wl9lLKsE=", 165)]
    [InlineData(1423, ECCLevel.Q, "mI+9aaDdXQHxir65csXwE487Zvg=", 165)]
    [InlineData(1093, ECCLevel.H, "f+oGwtL745K4S3x25qNBY8XCGyA=", 165)]
    // Version 38
    [InlineData(2699, ECCLevel.L, "jFAE8Cw77UN/uf8rndzymM7Idwc=", 169)]
    [InlineData(2099, ECCLevel.M, "roNsVHUcMngjE4/GyJop++9eeRs=", 169)]
    [InlineData(1499, ECCLevel.Q, "G2/TnwE1HSnoTytk0mr4552oIos=", 169)]
    [InlineData(1139, ECCLevel.H, "fou4BP/tpD9oxj7KeXyzZ6Fo+xc=", 169)]
    // Version 39
    [InlineData(2809, ECCLevel.L, "IZ2L6FrpTQdjqls020r8YEWiFU8=", 173)]
    [InlineData(2213, ECCLevel.M, "iybO3rmtGmgVRcOYkDnfrH7Scpo=", 173)]
    [InlineData(1579, ECCLevel.Q, "O8jQuoPVybJuJy9ULv/xfJKA29Y=", 173)]
    [InlineData(1219, ECCLevel.H, "FD2FN7bSh8ispau30YQRyNN5LL0=", 173)]
    // Version 40
    [InlineData(2953, ECCLevel.L, "a+80y4pPAhCywuraFrnMSTFRRmo=", 177)]
    [InlineData(2331, ECCLevel.M, "jLKuYZ7beIij+5j9Ko6GRxZVzaA=", 177)]
    [InlineData(1663, ECCLevel.Q, "G1vhiI8anCkTOgeQPQAVH3xcSk8=", 177)]
    [InlineData(1273, ECCLevel.H, "A0HAgMWn4TvnFfSnnEhQ0cVXcNU=", 177)]
    public void can_encode_various_strings_various_ecc(int inputChars, ECCLevel eccLevel, string expectedHash, int expectedSize)
    {
        var input = new string('a', inputChars);
        var gen = new QRCodeGenerator();
        var qrData = gen.CreateQrCode(input, eccLevel);
        (qrData.ModuleMatrix.Count - 8).ShouldBe(expectedSize); // exclude padding
        var result = string.Join("", qrData.ModuleMatrix.Select(x => x.ToBitString()).ToArray());
        var hash = System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(result));
        var hashString = Convert.ToBase64String(hash);
        hashString.ShouldBe(expectedHash);
    }
#endif

    [Fact]
    public void validate_alphanumencdict()
    {
        var gen = new QRCodeGenerator();

        var checkString = string.Empty;
        var gField = gen.GetType().GetField("_alphanumEncDict", BindingFlags.NonPublic | BindingFlags.Static);
        foreach (var listitem in (Dictionary<char, int>)gField.GetValue(gen))
        {
            checkString += $"{listitem.Key},{listitem.Value}:";
        }
        checkString.ShouldBe("0,0:1,1:2,2:3,3:4,4:5,5:6,6:7,7:8,8:9,9:A,10:B,11:C,12:D,13:E,14:F,15:G,16:H,17:I,18:J,19:K,20:L,21:M,22:N,23:O,24:P,25:Q,26:R,27:S,28:T,29:U,30:V,31:W,32:X,33:Y,34:Z,35: ,36:$,37:%,38:*,39:+,40:-,41:.,42:/,43::,44:");
    }

    [Fact]
    public void can_recognize_enconding_numeric()
    {
        var gen = new QRCodeGenerator();
        var method = gen.GetType().GetMethod("GetEncodingFromPlaintext", BindingFlags.NonPublic | BindingFlags.Static);
        var result = (int)method.Invoke(gen, new object[] { "0123456789", false });

        result.ShouldBe(1);
    }


    [Fact]
    public void can_recognize_enconding_alphanumeric()
    {
        var gen = new QRCodeGenerator();
        var method = gen.GetType().GetMethod("GetEncodingFromPlaintext", BindingFlags.NonPublic | BindingFlags.Static);
        var result = (int)method.Invoke(gen, new object[] { "0123456789ABC", false });

        result.ShouldBe(2);
    }


    [Fact]
    public void can_recognize_enconding_forced_bytemode()
    {
        var gen = new QRCodeGenerator();
        var method = gen.GetType().GetMethod("GetEncodingFromPlaintext", BindingFlags.NonPublic | BindingFlags.Static);
        var result = (int)method.Invoke(gen, new object[] { "0123456789", true });

        result.ShouldBe(4);
    }


    [Fact]
    public void can_recognize_enconding_byte()
    {
        var gen = new QRCodeGenerator();
        var method = gen.GetType().GetMethod("GetEncodingFromPlaintext", BindingFlags.NonPublic | BindingFlags.Static);
        var result = (int)method.Invoke(gen, new object[] { "0123456789äöüß", false });

        result.ShouldBe(4);
    }

    [Fact]
    public void can_encode_numeric()
    {
        var gen = new QRCodeGenerator();
        var qrData = gen.CreateQrCode("123", ECCLevel.L);
        var result = string.Join("", qrData.ModuleMatrix.Select(x => x.ToBitString()).ToArray());
        result.ShouldBe("00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000011111110111110111111100000000100000100101001000001000000001011101001100010111010000000010111010011100101110100000000101110100010101011101000000001000001000011010000010000000011111110101010111111100000000000000001111100000000000000001101101001101010000010000000011101100000010101011000000000001101111000011011100000000001011110100110000011110000000000111011111000100110100000000000000001111100101011000000001111111000101111100010000000010000010000111011100100000000101110101011101101101000000001011101010111000111000000000010111010011000100011100000000100000101010011010101000000001111111011010000011100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
    }

    [Fact]
    public void can_encode_numeric_2()
    {
        var gen = new QRCodeGenerator();
        var qrData = gen.CreateQrCode("1234567", ECCLevel.L);
        var result = string.Join("", qrData.ModuleMatrix.Select(x => x.ToBitString()).ToArray());
        result.ShouldBe("0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001111111011111011111110000000010000010010100100000100000000101110100110001011101000000001011101001110010111010000000010111010001010101110100000000100000100001101000001000000001111111010101011111110000000000000000000110000000000000000111100101111110011101000000000111100010011110001110000000000100010100100000001000000000011110011111001110011000000001111101110101001000000000000000000000111100100100100000000111111100001100100110000000001000001000100001111110000000010111010010011111010100000000101110101111001011110000000001011101010101011000000000000010000010111001000010000000000111111101010010010010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
    }

    [Fact]
    public void can_encode_numeric_3()
    {
        var gen = new QRCodeGenerator();
        var qrData = gen.CreateQrCode("12345678901", ECCLevel.L);
        var result = string.Join("", qrData.ModuleMatrix.Select(x => x.ToBitString()).ToArray());
        result.ShouldBe("0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001111111010111011111110000000010000010001100100000100000000101110101101001011101000000001011101011001010111010000000010111010100100101110100000000100000100111101000001000000001111111010101011111110000000000000000000110000000000000000111100101111110011101000000000111100010011110001110000000000100010100100000001000000000011110011111001110011000000001111101110101001000000000000000000000111100100100100000000111111100001100100110000000001000001000100001111110000000010111010010011111010100000000101110101111001011110000000001011101010101011000000000000010000010111001000010000000000111111101010010010010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
    }

    [Fact]
    public void can_encode_alphanumeric()
    {
        var gen = new QRCodeGenerator();
        var qrData = gen.CreateQrCode("123ABC", ECCLevel.L);
        var result = string.Join("", qrData.ModuleMatrix.Select(x => x.ToBitString()).ToArray());
        result.ShouldBe("0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001111111010111011111110000000010000010001100100000100000000101110101101001011101000000001011101011001010111010000000010111010100100101110100000000100000100111101000001000000001111111010101011111110000000000000000000110000000000000000111100101111110011101000000000111100010011110001110000000000100010100100000001000000000011110011111001110011000000001111101110101001000000000000000000000111100100100100000000111111100001100100110000000001000001000100001111110000000010111010010011111010100000000101110101111001011110000000001011101010101011000000000000010000010111001000010000000000111111101010010010010000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
    }
}

public static class ExtensionMethods
{
    public static string ToBitString(this BitArray bits)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < bits.Length; i++)
        {
            sb.Append(bits[i] ? '1' : '0');
        }
        return sb.ToString();
    }
}
