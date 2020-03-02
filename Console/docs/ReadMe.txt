

14:47:26.84>Console.exe show | more

でバグが発生

14:38:30.51>Console.exe show  | more
InputEncoding=shift_jis(932)
OutputEncoding=shift_jis(932)
BackgroundColor=DarkBlue
ForegroundColor=Gray
BufferHeight=2000
BufferWidth=80
WindowHeight=15
WindowWidth=80
LargestWindowWidth=0
LargestWindowHeight=0
WindowLeft=0
WindowTop=217
CursorLeft=0
CursorTop=233
-- More  --
ハンドルされていない例外: System.Reflection.TargetInvocationException: 呼び出し
のターゲットが例外をスローしました。 ---> System.IO.IOException: ハンドルが無効
です。

   場所 System.IO.__Error.WinIOError(Int32 errorCode, String maybeFullPath)
   場所 System.IO.__Error.WinIOError()
   場所 System.Console.get_CursorSize()
   --- 内部例外スタック トレースの終わり ---
   場所 System.RuntimeMethodHandle._InvokeMethodFast(Object target, Object[] arg
uments, SignatureStruct& sig, MethodAttributes methodAttributes, RuntimeTypeHand
le typeOwner)
   場所 System.RuntimeMethodHandle.InvokeMethodFast(Object target, Object[] argu
ments, Signature sig, MethodAttributes methodAttributes, RuntimeTypeHandle typeO
wner)
   場所 System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invo
keAttr, Binder binder, Object[] parameters, CultureInfo culture, Boolean skipVis
ibilityChecks)
   場所 System.Reflection.RuntimeMethodInfo.Invoke(Object obj, BindingFlags invo
keAttr, Binder binder, Object[] parameters, CultureInfo culture)
   場所 System.Reflection.RuntimePropertyInfo.GetValue(Object obj, BindingFlags
invokeAttr, Binder binder, Object[] index, CultureInfo culture)
   場所 System.Reflection.RuntimePropertyInfo.GetValue(Object obj, Object[] inde
x)
   場所 SConsole.ShowCommand.ShowAll() 場所 D:\kiyo\Windows\Deveropment\SETools2
\Console\ShowCommand.cs:行 51
   場所 SConsole.Program.Main(String[] args) 場所 D:\kiyo\Windows\Deveropment\SE
Tools2\Console\Program.cs:行 26








