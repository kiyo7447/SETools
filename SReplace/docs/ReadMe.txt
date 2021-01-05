
--テストパラメータ
O:\20090526_swg_pc50台\w\a.txt O:\20090526_swg_pc50台\w\b.txt 10.81.104.51\n ""

/R /C 932 TextFile1.txt TextFile2.txt "\r\n***," ""



::sreplace /R /C 932 %server%_source.csv %server%_source2.csv "\r\n\*\*\*," ""

↓結局これを使用しました。
sreplace /R /C 932 %server%_source.csv %server%_source2.csv "\r\n\*\*\*\t" ""

/dl O:\20090526_xxx_pc50台\w\a.txt O:\20090526_xxx_pc50台\w\b.txt 10.10.1.1 ""

::上書き確認のテスト
/r a.txt b.txt a b

