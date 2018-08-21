’
G/Users/cash/Github/Baymax/Src/Baymax.Tester/AcceptedResultAssertions.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public 

class $
AcceptedResultAssertions )
<) *
TController* 5
>5 6
where7 <
TController= H
:I J

ControllerK U
{ 
private		 
readonly		 
AcceptedResult		 '
_acceptedResult		( 7
;		7 8
public $
AcceptedResultAssertions '
(' (
AcceptedResult( 6
acceptedResult7 E
)E F
{ 	
_acceptedResult 
= 
acceptedResult ,
;, -
} 	
public $
AcceptedResultAssertions '
<' (
TController( 3
>3 4
WithLocation5 A
(A B
stringB H
expectedLocationI Y
)Y Z
{ 	
_acceptedResult 
. 
Location $
.$ %
Should% +
(+ ,
), -
.- .
Be. 0
(0 1
expectedLocation1 A
)A B
;B C
return 
this 
; 
} 	
public $
AcceptedResultAssertions '
<' (
TController( 3
>3 4
	WithValue5 >
(> ?
object? E
expectValueF Q
)Q R
{ 	
expectValue 
. 
ToExpectedObject (
(( )
)) *
.* +
ShouldEqual+ 6
(6 7
_acceptedResult7 F
.F G
ValueG L
)L M
;M N
return 
this 
; 
} 	
} 
} ä`
E/Users/cash/Github/Baymax/Src/Baymax.Tester/ActionResultAssertions.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public 

class "
ActionResultAssertions '
<' (
TController( 3
>3 4
where5 :
TController; F
:G H

ControllerI S
{ 
private 
readonly 
IActionResult &
_actionResult' 4
;4 5
private		 
readonly		 
TController		 $
_controller		% 0
;		0 1
public "
ActionResultAssertions %
(% &
IActionResult& 3
actionResult4 @
,@ A
TControllerB M

controllerN X
)X Y
{ 	
_actionResult 
= 
actionResult (
;( )
_controller 
= 

controller $
;$ %
} 	
public ,
 RedirectToActionResultAssertions /
</ 0
TController0 ;
>; <*
ShouldBeRedirectToActionResult= [
([ \
)\ ]
{ 	
_actionResult 
. 
Should  
(  !
)! "
." #
BeOfType# +
<+ ,"
RedirectToActionResult, B
>B C
(C D
)D E
;E F
return 
new ,
 RedirectToActionResultAssertions 7
<7 8
TController8 C
>C D
(D E
_actionResultE R
asS U"
RedirectToActionResultV l
,l m
_controllern y
)y z
;z {
} 	
public  
JsonResultAssertions #
ShouldBeJsonResult$ 6
(6 7
)7 8
{ 	
_actionResult 
. 
Should  
(  !
)! "
." #
BeOfType# +
<+ ,

JsonResult, 6
>6 7
(7 8
)8 9
;9 :
return 
new  
JsonResultAssertions +
(+ ,
_actionResult, 9
as: <

JsonResult= G
)G H
;H I
} 	
public  
ViewResultAssertions #
<# $
TController$ /
>/ 0
ShouldBeViewResult1 C
(C D
)D E
{   	
_actionResult!! 
.!! 
Should!!  
(!!  !
)!!! "
.!!" #
BeOfType!!# +
<!!+ ,

ViewResult!!, 6
>!!6 7
(!!7 8
)!!8 9
;!!9 :
return## 
new##  
ViewResultAssertions## +
<##+ ,
TController##, 7
>##7 8
(##8 9
_actionResult##9 F
as##G I

ViewResult##J T
,##T U
_controller##V a
)##a b
;##b c
}$$ 	
public&& #
ContentResultAssertions&& &!
ShouldBeContentResult&&' <
(&&< =
)&&= >
{'' 	
_actionResult(( 
.(( 
Should((  
(((  !
)((! "
.((" #
BeOfType((# +
<((+ ,
ContentResult((, 9
>((9 :
(((: ;
)((; <
;((< =
return** 
new** #
ContentResultAssertions** .
(**. /
_actionResult**/ <
as**= ?
ContentResult**@ M
)**M N
;**N O
}++ 	
public-- '
FileContentResultAssertions-- *
<--* +
TController--+ 6
>--6 7%
ShouldBeFileContentResult--8 Q
(--Q R
)--R S
{.. 	
_actionResult// 
.// 
Should//  
(//  !
)//! "
.//" #
BeOfType//# +
<//+ ,
ContentResult//, 9
>//9 :
(//: ;
)//; <
;//< =
return11 
new11 '
FileContentResultAssertions11 2
<112 3
TController113 >
>11> ?
(11? @
_actionResult11@ M
as11N P
FileContentResult11Q b
,11b c
_controller11d o
)11o p
;11p q
}22 	
public44 &
FileStreamResultAssertions44 )
<44) *
TController44* 5
>445 6$
ShouldBeFileStreamResult447 O
(44O P
)44P Q
{55 	
_actionResult66 
.66 
Should66  
(66  !
)66! "
.66" #
BeOfType66# +
<66+ ,
FileStreamResult66, <
>66< =
(66= >
)66> ?
;66? @
return88 
new88 &
FileStreamResultAssertions88 1
<881 2
TController882 =
>88= >
(88> ?
_actionResult88? L
as88M O
FileStreamResult88P `
,88` a
_controller88b m
)88m n
;88n o
}99 	
public;; 
void;; 
ShouldBeEmptyResult;; '
(;;' (
);;( )
{<< 	
_actionResult== 
.== 
Should==  
(==  !
)==! "
.==" #
BeOfType==# +
<==+ ,
EmptyResult==, 7
>==7 8
(==8 9
)==9 :
;==: ;
}>> 	
public@@ '
PartialViewResultAssertions@@ *
<@@* +
TController@@+ 6
>@@6 7%
ShouldBePartialViewResult@@8 Q
(@@Q R
)@@R S
{AA 	
_actionResultBB 
.BB 
ShouldBB  
(BB  !
)BB! "
.BB" #
BeOfTypeBB# +
<BB+ ,
PartialViewResultBB, =
>BB= >
(BB> ?
)BB? @
;BB@ A
returnDD 
newDD '
PartialViewResultAssertionsDD 2
<DD2 3
TControllerDD3 >
>DD> ?
(DD? @
_actionResultDD@ M
asDDN P
PartialViewResultDDQ b
,DDb c
_controllerDDd o
)DDo p
;DDp q
}EE 	
publicGG $
RedirectResultAssertionsGG '
<GG' (
TControllerGG( 3
>GG3 4"
ShouldBeRedirectResultGG5 K
(GGK L
)GGL M
{HH 	
_actionResultII 
.II 
ShouldII  
(II  !
)II! "
.II" #
BeOfTypeII# +
<II+ ,
RedirectResultII, :
>II: ;
(II; <
)II< =
;II= >
returnKK 
newKK $
RedirectResultAssertionsKK /
<KK/ 0
TControllerKK0 ;
>KK; <
(KK< =
_actionResultKK= J
asKKK M
RedirectResultKKN \
,KK\ ]
_controllerKK^ i
)KKi j
;KKj k
}LL 	
publicNN "
ForbidResultAssertionsNN % 
ShouldBeForbidResultNN& :
(NN: ;
)NN; <
{OO 	
_actionResultPP 
.PP 
ShouldPP  
(PP  !
)PP! "
.PP" #
BeOfTypePP# +
<PP+ ,
ForbidResultPP, 8
>PP8 9
(PP9 :
)PP: ;
;PP; <
returnRR 
newRR "
ForbidResultAssertionsRR ,
(RR, -
_actionResultRR- :
asRR; =
ForbidResultRR> J
)RRJ K
;RRK L
}SS 	
publicUU )
LocalRedirectResultAssertionsUU ,
<UU, -
TControllerUU- 8
>UU8 9'
ShouldBeLocalRedirectResultUU: U
(UUU V
)UUV W
{VV 	
_actionResultWW 
.WW 
ShouldWW  
(WW  !
)WW! "
.WW" #
BeOfTypeWW# +
<WW+ ,
LocalRedirectResultWW, ?
>WW? @
(WW@ A
)WWA B
;WWB C
returnYY 
newYY )
LocalRedirectResultAssertionsYY 3
<YY3 4
TControllerYY4 ?
>YY? @
(YY@ A
_actionResultYYA N
asYYO Q
LocalRedirectResultYYR e
,YYe f
_controllerYYg r
)YYr s
;YYs t
}ZZ 	
public\\ +
RedirectToRouteResultAssertions\\ .
<\\. /
TController\\/ :
>\\: ;)
ShouldBeRedirectToRouteResult\\< Y
(\\Y Z
)\\Z [
{]] 	
_actionResult^^ 
.^^ 
Should^^  
(^^  !
)^^! "
.^^" #
BeOfType^^# +
<^^+ ,!
RedirectToRouteResult^^, A
>^^A B
(^^B C
)^^C D
;^^D E
return`` 
new`` +
RedirectToRouteResultAssertions`` 6
<``6 7
TController``7 B
>``B C
(``C D
_actionResult``D Q
as``R T!
RedirectToRouteResult``U j
,``j k
_controller``l w
)``w x
;``x y
}aa 	
publiccc &
StatusCodeResultAssertionscc )
<cc) *
TControllercc* 5
>cc5 6$
ShouldBeStatusCodeResultcc7 O
(ccO P
)ccP Q
{dd 	
_actionResultee 
.ee 
Shouldee  
(ee  !
)ee! "
.ee" #
BeOfTypeee# +
<ee+ ,
StatusCodeResultee, <
>ee< =
(ee= >
)ee> ?
;ee? @
returngg 
newgg &
StatusCodeResultAssertionsgg 1
<gg1 2
TControllergg2 =
>gg= >
(gg> ?
_actionResultgg? L
asggM O
StatusCodeResultggP `
,gg` a
_controllerggb m
)ggm n
;ggn o
}hh 	
publicjj %
ChallengeResultAssertionsjj (#
ShouldBeChallengeResultjj) @
(jj@ A
)jjA B
{kk 	
_actionResultll 
.ll 
Shouldll  
(ll  !
)ll! "
.ll" #
BeOfTypell# +
<ll+ ,
ChallengeResultll, ;
>ll; <
(ll< =
)ll= >
;ll> ?
returnnn 
newnn %
ChallengeResultAssertionsnn 0
(nn0 1
_actionResultnn1 >
asnn? A
ChallengeResultnnB Q
)nnQ R
;nnR S
}oo 	
publicqq +
CreatedAtActionResultAssertionsqq .)
ShouldBeCreatedAtActionResultqq/ L
(qqL M
)qqM N
{rr 	
_actionResultss 
.ss 
Shouldss  
(ss  !
)ss! "
.ss" #
BeOfTypess# +
<ss+ ,!
CreatedAtActionResultss, A
>ssA B
(ssB C
)ssC D
;ssD E
returnuu 
newuu +
CreatedAtActionResultAssertionsuu 6
(uu6 7
_actionResultuu7 D
asuuE G!
CreatedAtActionResultuuH ]
)uu] ^
;uu^ _
}vv 	
publicxx *
CreatedAtRouteResultAssertionsxx -&
ShouldCreatedAtRouteResultxx. H
(xxH I
)xxI J
{yy 	
_actionResultzz 
.zz 
Shouldzz  
(zz  !
)zz! "
.zz" #
BeOfTypezz# +
<zz+ , 
CreatedAtRouteResultzz, @
>zz@ A
(zzA B
)zzB C
;zzC D
return|| 
new|| *
CreatedAtRouteResultAssertions|| 5
(||5 6
_actionResult||6 C
as||D F 
CreatedAtRouteResult||G [
)||[ \
;||\ ]
}}} 	
public $
AcceptedResultAssertions '
<' (
TController( 3
>3 4 
ShouldAcceptedResult5 I
(I J
)J K
{
ÄÄ 	
_actionResult
ÅÅ 
.
ÅÅ 
Should
ÅÅ  
(
ÅÅ  !
)
ÅÅ! "
.
ÅÅ" #
BeOfType
ÅÅ# +
<
ÅÅ+ ,
AcceptedResult
ÅÅ, :
>
ÅÅ: ;
(
ÅÅ; <
)
ÅÅ< =
;
ÅÅ= >
return
ÉÉ 
new
ÉÉ &
AcceptedResultAssertions
ÉÉ /
<
ÉÉ/ 0
TController
ÉÉ0 ;
>
ÉÉ; <
(
ÉÉ< =
_actionResult
ÉÉ= J
as
ÉÉK M
AcceptedResult
ÉÉN \
)
ÉÉ\ ]
;
ÉÉ] ^
}
ÑÑ 	
}
ÖÖ 
}ÜÜ ∂
H/Users/cash/Github/Baymax/Src/Baymax.Tester/ChallengeResultAssertions.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public 

class %
ChallengeResultAssertions *
{		 
private

 
readonly

 
ChallengeResult

 (
_challengeResult

) 9
;

9 :
public %
ChallengeResultAssertions (
(( )
ChallengeResult) 8
challengeResult9 H
)H I
{ 	
_challengeResult 
= 
challengeResult .
;. /
} 	
public %
ChallengeResultAssertions (%
WithAuthenticationSchemes) B
(B C
ListC G
<G H
stringH N
>N O)
expectedAuthenticationSchemesP m
)m n
{ 	)
expectedAuthenticationSchemes )
.) *
ToExpectedObject* :
(: ;
); <
.< =
ShouldEqual= H
(H I
_challengeResultI Y
.Y Z!
AuthenticationSchemesZ o
)o p
;p q
return 
this 
; 
} 	
public %
ChallengeResultAssertions ((
WithAuthenticationProperties) E
(E F$
AuthenticationPropertiesF ^,
 expectedAuthenticationProperties_ 
)	 Ä
{ 	,
 expectedAuthenticationProperties +
.+ ,
ToExpectedObject, <
(< =
)= >
.> ?
ShouldEqual? J
(J K
_challengeResultK [
.[ \

Properties\ f
)f g
;g h
return 
this 
; 
} 	
} 
} Á
F/Users/cash/Github/Baymax/Src/Baymax.Tester/ContentResultAssertions.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public 

class #
ContentResultAssertions (
{ 
private 
readonly 
ContentResult &
_contentResult' 5
;5 6
public

 #
ContentResultAssertions

 &
(

& '
ContentResult

' 4
contentResult

5 B
)

B C
{ 	
_contentResult 
= 
contentResult *
;* +
} 	
public #
ContentResultAssertions &
WithContent' 2
(2 3
string3 9
expectedContent: I
)I J
{ 	
_contentResult 
. 
Content "
." #
Should# )
() *
)* +
.+ ,
Be, .
(. /
expectedContent/ >
)> ?
;? @
return 
this 
; 
} 	
} 
} ¿
?/Users/cash/Github/Baymax/Src/Baymax.Tester/ControllerTester.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public 

class 
ControllerTester !
<! "
TController" -
>- .
where/ 4
TController5 @
:A B

ControllerC M
{ 
private		 
readonly		 
TController		 $
_controller		% 0
;		0 1
public 
ControllerTester 
(  
TController  +

controller, 6
)6 7
{ 	
_controller 
= 

controller $
;$ %
} 	
public "
ActionResultAssertions %
<% &
TController& 1
>1 2
Action3 9
(9 :
Func: >
<> ?
TController? J
,J K
IActionResultL Y
>Y Z
func[ _
)_ `
{ 	
return 
new "
ActionResultAssertions -
<- .
TController. 9
>9 :
(: ;
func; ?
?? @
.@ A
InvokeA G
(G H
_controllerH S
)S T
,T U
_controllerV a
)a b
;b c
} 	
public "
ActionResultAssertions %
<% &
TController& 1
>1 2
Action3 9
(9 :
Func: >
<> ?
TController? J
,J K
TaskL P
<P Q
IActionResultQ ^
>^ _
>_ `
funca e
)e f
{ 	
var 
result 
= 
func 
? 
. 
Invoke %
(% &
_controller& 1
)1 2
.2 3
Result3 9
;9 :
return 
new "
ActionResultAssertions -
<- .
TController. 9
>9 :
(: ;
result; A
,A B
_controllerC N
)N O
;O P
} 	
} 
} ç
I/Users/cash/Github/Baymax/Src/Baymax.Tester/ControllerTesterExtensions.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public 

static 
class &
ControllerTesterExtensions 2
{ 
public 
static 
ControllerTester &
<& '
TController' 2
>2 3
AsTester4 <
<< =
TController= H
>H I
(I J
thisJ N
TControllerO Z

controller[ e
)e f
where 
TController 
: 

Controller  *
{		 	
return

 
new

 
ControllerTester

 '
<

' (
TController

( 3
>

3 4
(

4 5

controller

5 ?
)

? @
;

@ A
} 	
} 
} µ
N/Users/cash/Github/Baymax/Src/Baymax.Tester/CreatedAtActionResultAssertions.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public 

class +
CreatedAtActionResultAssertions 0
{ 
private		 
readonly		 !
CreatedAtActionResult		 ."
_createdAtActionResult		/ E
;		E F
public +
CreatedAtActionResultAssertions .
(. /!
CreatedAtActionResult/ D!
createdAtActionResultE Z
)Z [
{ 	"
_createdAtActionResult "
=# $!
createdAtActionResult% :
;: ;
} 	
public +
CreatedAtActionResultAssertions .
WithActionName/ =
(= >
string> D
expectedActionNameE W
)W X
{ 	"
_createdAtActionResult "
." #

ActionName# -
.- .
Should. 4
(4 5
)5 6
.6 7
Be7 9
(9 :
expectedActionName: L
)L M
;M N
return 
this 
; 
} 	
public +
CreatedAtActionResultAssertions .
WithControllerName/ A
(A B
stringB H"
expectedControllerNameI _
)_ `
{ 	"
_createdAtActionResult "
." #
ControllerName# 1
.1 2
Should2 8
(8 9
)9 :
.: ;
Be; =
(= >"
expectedControllerName> T
)T U
;U V
return 
this 
; 
} 	
public +
CreatedAtActionResultAssertions .
WithRouteValue/ =
(= >
string> D
keyE H
,H I
objectJ P
expectedValueQ ^
)^ _
{ 	
expectedValue   
.   
ToExpectedObject   *
(  * +
)  + ,
.  , -
ShouldMatch  - 8
(  8 9"
_createdAtActionResult  9 O
.  O P
RouteValues  P [
[  [ \
key  \ _
]  _ `
)  ` a
;  a b
return"" 
this"" 
;"" 
}## 	
public%% +
CreatedAtActionResultAssertions%% .
	WithValue%%/ 8
(%%8 9
object%%9 ?
expectedValue%%@ M
)%%M N
{&& 	
expectedValue'' 
.'' 
ToExpectedObject'' *
(''* +
)''+ ,
.'', -
ShouldEqual''- 8
(''8 9"
_createdAtActionResult''9 O
.''O P
Value''P U
)''U V
;''V W
return)) 
this)) 
;)) 
}** 	
}++ 
},, ò
M/Users/cash/Github/Baymax/Src/Baymax.Tester/CreatedAtRouteResultAssertions.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public 

class *
CreatedAtRouteResultAssertions /
{ 
private		 
readonly		  
CreatedAtRouteResult		 -!
_createdAtRouteResult		. C
;		C D
public *
CreatedAtRouteResultAssertions -
(- . 
CreatedAtRouteResult. B 
createdAtRouteResultC W
)W X
{ 	!
_createdAtRouteResult !
=" # 
createdAtRouteResult$ 8
;8 9
} 	
public *
CreatedAtRouteResultAssertions -
WithRouteName. ;
(; <
string< B
expectedRouteNameC T
)T U
{ 	!
_createdAtRouteResult !
.! "
	RouteName" +
.+ ,
Should, 2
(2 3
)3 4
.4 5
Be5 7
(7 8
expectedRouteName8 I
)I J
;J K
return 
this 
; 
} 	
public *
CreatedAtRouteResultAssertions -
WithRouteValue. <
(< =
string= C
keyD G
,G H
objectI O
expectedValueP ]
)] ^
{ 	
expectedValue 
. 
ToExpectedObject *
(* +
)+ ,
., -
ShouldEqual- 8
(8 9!
_createdAtRouteResult9 N
.N O
RouteValuesO Z
[Z [
key[ ^
]^ _
)_ `
;` a
return 
this 
; 
} 	
public *
CreatedAtRouteResultAssertions -
	WithValue. 7
(7 8
object8 >
expectedValue? L
)L M
{ 	
expectedValue   
.   
ToExpectedObject   *
(  * +
)  + ,
.  , -
ShouldEqual  - 8
(  8 9!
_createdAtRouteResult  9 N
.  N O
Value  O T
)  T U
;  U V
return"" 
this"" 
;"" 
}## 	
}$$ 
}%% Î
J/Users/cash/Github/Baymax/Src/Baymax.Tester/FileContentResultAssertions.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public 

class '
FileContentResultAssertions ,
<, -
TController- 8
>8 9
{		 
private

 
readonly

 
FileContentResult

 *
_fileContentResult

+ =
;

= >
private 
readonly 
TController $
_controller% 0
;0 1
public '
FileContentResultAssertions *
(* +
FileContentResult+ <
fileContentResult= N
,N O
TControllerP [

controller\ f
)f g
{ 	
this 
. 
_fileContentResult #
=$ %
fileContentResult& 7
;7 8
this 
. 
_controller 
= 

controller )
;) *
} 	
public '
FileContentResultAssertions *
<* +
TController+ 6
>6 7
WithContentType8 G
(G H
stringH N
expectedContentTypeO b
)b c
{ 	
this 
. 
_fileContentResult #
.# $
ContentType$ /
./ 0
Should0 6
(6 7
)7 8
.8 9
Be9 ;
(; <
expectedContentType< O
)O P
;P Q
return 
this 
; 
} 	
public '
FileContentResultAssertions *
<* +
TController+ 6
>6 7
WithFileContents8 H
(H I
byteI M
[M N
]N O 
expectedFileContentsP d
)d e
{ 	 
expectedFileContents  
.  !
ToExpectedObject! 1
(1 2
)2 3
.3 4
ShouldEqual4 ?
(? @
_fileContentResult@ R
.R S
FileContentsS _
)_ `
;` a
return 
this 
; 
} 	
public!! '
FileContentResultAssertions!! *
<!!* +
TController!!+ 6
>!!6 7 
WithFileDownloadName!!8 L
(!!L M
string!!M S$
expectedFileDownloadName!!T l
)!!l m
{"" 	
_fileContentResult## 
.## 
FileDownloadName## /
.##/ 0
Should##0 6
(##6 7
)##7 8
.##8 9
Be##9 ;
(##; <$
expectedFileDownloadName##< T
)##T U
;##U V
return%% 
this%% 
;%% 
}&& 	
public(( '
FileContentResultAssertions(( *
<((* +
TController((+ 6
>((6 7
WithLastModified((8 H
(((H I
DateTimeOffset((I W 
expectedLastModified((X l
)((l m
{)) 	
_fileContentResult** 
.** 
LastModified** +
.**+ ,
Value**, 1
.**1 2
Should**2 8
(**8 9
)**9 :
.**: ;
Be**; =
(**= > 
expectedLastModified**> R
)**R S
;**S T
return,, 
this,, 
;,, 
}-- 	
}.. 
}// ∞
I/Users/cash/Github/Baymax/Src/Baymax.Tester/FileStreamResultAssertions.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public		 

class		 &
FileStreamResultAssertions		 +
<		+ ,
TController		, 7
>		7 8
{

 
private 
readonly 
FileStreamResult )
_fileStreamResult* ;
;; <
private 
readonly 
TController $
_controller% 0
;0 1
public &
FileStreamResultAssertions )
() *
FileStreamResult* :
fileStreamResult; K
,K L
TControllerM X

controllerY c
)c d
{ 	
this 
. 
_fileStreamResult "
=# $
fileStreamResult% 5
;5 6
this 
. 
_controller 
= 

controller )
;) *
} 	
public &
FileStreamResultAssertions )
<) *
TController* 5
>5 6
WithContentType7 F
(F G
stringG M
expectedContentTypeN a
)a b
{ 	
this 
. 
_fileStreamResult "
." #
ContentType# .
.. /
Should/ 5
(5 6
)6 7
.7 8
Be8 :
(: ;
expectedContentType; N
)N O
;O P
return 
this 
; 
} 	
public &
FileStreamResultAssertions )
<) *
TController* 5
>5 6
WithFileContents7 G
(G H
StreamH N
expectedStreamO ]
)] ^
{ 	
expectedStream 
. 
ToExpectedObject +
(+ ,
), -
.- .
ShouldEqual. 9
(9 :
_fileStreamResult: K
.K L

FileStreamL V
)V W
;W X
return 
this 
; 
}   	
public"" &
FileStreamResultAssertions"" )
<"") *
TController""* 5
>""5 6 
WithFileDownloadName""7 K
(""K L
string""L R$
expectedFileDownloadName""S k
)""k l
{## 	
_fileStreamResult$$ 
.$$ 
FileDownloadName$$ .
.$$. /
Should$$/ 5
($$5 6
)$$6 7
.$$7 8
Be$$8 :
($$: ;$
expectedFileDownloadName$$; S
)$$S T
;$$T U
return&& 
this&& 
;&& 
}'' 	
public)) &
FileStreamResultAssertions)) )
<))) *
TController))* 5
>))5 6
WithLastModified))7 G
())G H
DateTimeOffset))H V 
expectedLastModified))W k
)))k l
{** 	
_fileStreamResult++ 
.++ 
LastModified++ *
.++* +
Value+++ 0
.++0 1
Should++1 7
(++7 8
)++8 9
.++9 :
Be++: <
(++< = 
expectedLastModified++= Q
)++Q R
;++R S
return-- 
this-- 
;-- 
}.. 	
}// 
}00 é
E/Users/cash/Github/Baymax/Src/Baymax.Tester/ForbidResultAssertions.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public 

class "
ForbidResultAssertions '
{		 
private

 
readonly

 
ForbidResult

 %
_forbidResult

& 3
;

3 4
public "
ForbidResultAssertions %
(% &
ForbidResult& 2
forbidResult3 ?
)? @
{ 	
_forbidResult 
= 
forbidResult (
;( )
} 	
public "
ForbidResultAssertions %%
WithAuthenticationSchemes& ?
(? @
List@ D
<D E
stringE K
>K L)
expectedAuthenticationSchemesM j
)j k
{ 	)
expectedAuthenticationSchemes )
.) *
ToExpectedObject* :
(: ;
); <
.< =
ShouldEqual= H
(H I
_forbidResultI V
.V W!
AuthenticationSchemesW l
)l m
;m n
return 
this 
; 
} 	
public "
ForbidResultAssertions %(
WithAuthenticationProperties& B
(B C$
AuthenticationPropertiesC [,
 expectedAuthenticationProperties\ |
)| }
{ 	,
 expectedAuthenticationProperties +
.+ ,
ToExpectedObject, <
(< =
)= >
.> ?
ShouldEqual? J
(J K
_forbidResultK X
.X Y

PropertiesY c
)c d
;d e
return 
this 
; 
} 	
} 
} µ
C/Users/cash/Github/Baymax/Src/Baymax.Tester/JsonResultAssertions.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public 

class  
JsonResultAssertions %
{ 
private		 
readonly		 

JsonResult		 #
_jsonResult		$ /
;		/ 0
public  
JsonResultAssertions #
(# $

JsonResult$ .

jsonResult/ 9
)9 :
{ 	
_jsonResult 
= 

jsonResult $
;$ %
} 	
public  
JsonResultAssertions #
WithData$ ,
<, -
TModel- 3
>3 4
(4 5
TModel5 ;
data< @
)@ A
where 
TModel 
: 
class  
{ 	
_jsonResult 
. 
Value 
. 
Should $
($ %
)% &
.& '
BeOfType' /
</ 0
TModel0 6
>6 7
(7 8
)8 9
;9 :
data 
. 
ToExpectedObject !
(! "
)" #
.# $
ShouldEqual$ /
(/ 0
_jsonResult0 ;
.; <
Value< A
asB D
TModelE K
)K L
;L M
return 
this 
; 
} 	
public  
JsonResultAssertions #
WithAnonymousData$ 5
(5 6
object6 <
data= A
)A B
{ 	
_jsonResult 
. 
Value 
. 
ToObjectString ,
(, -
)- .
.. /
Should/ 5
(5 6
)6 7
.7 8
Be8 :
(: ;
data; ?
.? @
ToObjectString@ N
(N O
)O P
)P Q
;Q R
return 
this 
; 
} 	
} 
}   ¸
L/Users/cash/Github/Baymax/Src/Baymax.Tester/LocalRedirectResultAssertions.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public 

class )
LocalRedirectResultAssertions .
<. /
TController/ :
>: ;
where< A
TControllerB M
:N O

ControllerP Z
{ 
private 
readonly 
LocalRedirectResult , 
_localRedirectResult- A
;A B
private		 
readonly		 
TController		 $
_controller		% 0
;		0 1
public )
LocalRedirectResultAssertions ,
(, -
LocalRedirectResult- @
localRedirectResultA T
,T U
TControllerV a

controllerb l
)l m
{ 	 
_localRedirectResult  
=! "
localRedirectResult# 6
;6 7
_controller 
= 

controller $
;$ %
} 	
public )
LocalRedirectResultAssertions ,
<, -
TController- 8
>8 9
WithUrl: A
(A B
stringB H
expectedUrlI T
)T U
{ 	 
_localRedirectResult  
.  !
Url! $
.$ %
Should% +
(+ ,
), -
.- .
Be. 0
(0 1
expectedUrl1 <
)< =
;= >
return 
this 
; 
} 	
public )
LocalRedirectResultAssertions ,
<, -
TController- 8
>8 9
WithPermanent: G
(G H
boolH L
expectedPermanentM ^
)^ _
{ 	 
_localRedirectResult  
.  !
	Permanent! *
.* +
Should+ 1
(1 2
)2 3
.3 4
Be4 6
(6 7
expectedPermanent7 H
)H I
;I J
return 
this 
; 
} 	
} 
} ï%
J/Users/cash/Github/Baymax/Src/Baymax.Tester/PartialViewResultAssertions.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public 

class '
PartialViewResultAssertions ,
<, -
TController- 8
>8 9
where: ?
TController@ K
:L M

ControllerN X
{ 
private		 
readonly		 
PartialViewResult		 *
_partialViewResult		+ =
;		= >
private

 
readonly

 

Controller

 #
_controller

$ /
;

/ 0
public '
PartialViewResultAssertions *
(* +
PartialViewResult+ <
partialViewResult= N
,N O

ControllerP Z

controller[ e
)e f
{ 	
_partialViewResult 
=  
partialViewResult! 2
;2 3
_controller 
= 

controller $
;$ %
} 	
public '
PartialViewResultAssertions *
<* +
TController+ 6
>6 7
	WithModel8 A
<A B
TB C
>C D
(D E
TE F
expectedG O
)O P
{ 	
_partialViewResult 
. 
Model $
.$ %
Should% +
(+ ,
), -
.- .
BeAssignableTo. <
<< =
T= >
>> ?
(? @
)@ A
;A B
expected 
. 
ToExpectedObject %
(% &
)& '
.' (
ShouldEqual( 3
(3 4
_partialViewResult4 F
.F G
ModelG L
)L M
;M N
return 
this 
; 
} 	
public '
PartialViewResultAssertions *
<* +
TController+ 6
>6 7
WithViewName8 D
(D E
stringE K
viewNameL T
)T U
{ 	
_partialViewResult 
. 
ViewName '
.' (
Should( .
(. /
)/ 0
.0 1
Be1 3
(3 4
viewName4 <
)< =
;= >
return 
this 
; 
}   	
public"" '
PartialViewResultAssertions"" *
<""* +
TController""+ 6
>""6 7
WithDefaultViewName""8 K
(""K L
)""L M
{## 	
_partialViewResult$$ 
.$$ 
ViewName$$ '
.$$' (
Should$$( .
($$. /
)$$/ 0
.$$0 1
BeEmpty$$1 8
($$8 9
)$$9 :
;$$: ;
return&& 
this&& 
;&& 
}'' 	
public)) '
PartialViewResultAssertions)) *
<))* +
TController))+ 6
>))6 7
WithViewBag))8 C
())C D
string))D J
key))K N
,))N O
object))P V
expectedValue))W d
)))d e
{** 	
expectedValue++ 
.++ 
ToExpectedObject++ *
(++* +
)+++ ,
.++, -
ShouldEqual++- 8
(++8 9
_partialViewResult++9 K
.++K L
ViewData++L T
[++T U
key++U X
]++X Y
)++Y Z
;++Z [
return-- 
this-- 
;-- 
}.. 	
public00 '
PartialViewResultAssertions00 *
<00* +
TController00+ 6
>006 7
WithNotTempData008 G
(00G H
)00H I
{11 	
_partialViewResult22 
.22 
TempData22 '
.22' (
Count22( -
.22- .
Should22. 4
(224 5
)225 6
.226 7
Be227 9
(229 :
$num22: ;
)22; <
;22< =
return44 
this44 
;44 
}55 	
public77 '
PartialViewResultAssertions77 *
<77* +
TController77+ 6
>776 7
WithTempData778 D
(77D E
string77E K
key77L O
,77O P
object77Q W
expectedValue77X e
)77e f
{88 	
expectedValue99 
.99 
ToExpectedObject99 *
(99* +
)99+ ,
.99, -
ShouldEqual99- 8
(998 9
_partialViewResult999 K
.99K L
TempData99L T
[99T U
key99U X
]99X Y
)99Y Z
;99Z [
return;; 
this;; 
;;; 
}<< 	
}== 
}>> ª
G/Users/cash/Github/Baymax/Src/Baymax.Tester/RedirectResultAssertions.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public 

class $
RedirectResultAssertions )
<) *
TController* 5
>5 6
where7 <
TController= H
:I J

ControllerK U
{ 
private 
readonly 
RedirectResult '
_redirectResult( 7
;7 8
private		 
readonly		 
TController		 $
_controller		% 0
;		0 1
public $
RedirectResultAssertions '
(' (
RedirectResult( 6
redirectResult7 E
,E F
TControllerG R

controllerS ]
)] ^
{ 	
_redirectResult 
= 
redirectResult ,
;, -
_controller 
= 

controller $
;$ %
} 	
public $
RedirectResultAssertions '
<' (
TController( 3
>3 4
WithUrl5 <
(< =
string= C
expectedUrlD O
)O P
{ 	
_redirectResult 
. 
Url 
.  
Should  &
(& '
)' (
.( )
Be) +
(+ ,
expectedUrl, 7
)7 8
;8 9
return 
this 
; 
} 	
public $
RedirectResultAssertions '
<' (
TController( 3
>3 4
WithPermanent5 B
(B C
boolC G
expectedPermanentH Y
)Y Z
{ 	
_redirectResult 
. 
	Permanent %
.% &
Should& ,
(, -
)- .
.. /
Be/ 1
(1 2
expectedPermanent2 C
)C D
;D E
return 
this 
; 
} 	
} 
} Ü
O/Users/cash/Github/Baymax/Src/Baymax.Tester/RedirectToActionResultAssertions.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public 

class ,
 RedirectToActionResultAssertions 1
<1 2
TController2 =
>= >
where? D
TControllerE P
:Q R

ControllerS ]
{ 
private 
readonly "
RedirectToActionResult /
_actionResult0 =
;= >
private		 
readonly		 
TController		 $
_controller		% 0
;		0 1
public ,
 RedirectToActionResultAssertions /
(/ 0"
RedirectToActionResult0 F
actionResultG S
,S T
TControllerU `

controllera k
)k l
{ 	
_actionResult 
= 
actionResult (
;( )
_controller 
= 

controller $
;$ %
} 	
public ,
 RedirectToActionResultAssertions /
</ 0
TController0 ;
>; <

WithAction= G
(G H
stringH N
expectedActionO ]
)] ^
{ 	
_actionResult 
. 

ActionName $
.$ %
Should% +
(+ ,
), -
.- .
Be. 0
(0 1
expectedAction1 ?
)? @
;@ A
return 
this 
; 
} 	
public ,
 RedirectToActionResultAssertions /
</ 0
TController0 ;
>; <
WithController= K
(K L
stringL R
expectedControllerS e
)e f
{ 	
_actionResult 
. 
ControllerName (
.( )
Should) /
(/ 0
)0 1
.1 2
Be2 4
(4 5
expectedController5 G
)G H
;H I
return 
this 
; 
} 	
public ,
 RedirectToActionResultAssertions /
</ 0
TController0 ;
>; <
WithRouteValue= K
(K L
stringL R
keyS V
,V W
objectX ^
expectedValue_ l
)l m
{   	
_actionResult!! 
.!! 
RouteValues!! %
[!!% &
key!!& )
]!!) *
.!!* +
Should!!+ 1
(!!1 2
)!!2 3
.!!3 4
Be!!4 6
(!!6 7
expectedValue!!7 D
)!!D E
;!!E F
return## 
this## 
;## 
}$$ 	
public&& ,
 RedirectToActionResultAssertions&& /
<&&/ 0
TController&&0 ;
>&&; <
WithTempData&&= I
(&&I J
string&&J P
key&&Q T
,&&T U
object&&V \
expectedValue&&] j
)&&j k
{'' 	
_controller(( 
.(( 
TempData((  
[((  !
key((! $
](($ %
.((% &
Should((& ,
(((, -
)((- .
.((. /
Be((/ 1
(((1 2
expectedValue((2 ?
)((? @
;((@ A
return** 
this** 
;** 
}++ 	
public-- ,
 RedirectToActionResultAssertions-- /
<--/ 0
TController--0 ;
>--; <
WithNotTempData--= L
(--L M
)--M N
{.. 	
_controller// 
.// 
TempData//  
.//  !
Count//! &
.//& '
Should//' -
(//- .
)//. /
./// 0
Be//0 2
(//2 3
$num//3 4
)//4 5
;//5 6
return11 
this11 
;11 
}22 	
}33 
}44 ≠
N/Users/cash/Github/Baymax/Src/Baymax.Tester/RedirectToRouteResultAssertions.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public 

class +
RedirectToRouteResultAssertions 0
<0 1
TController1 <
>< =
where> C
TControllerD O
:P Q

ControllerR \
{ 
private		 
readonly		 !
RedirectToRouteResult		 ."
_redirectToRouteResult		/ E
;		E F
private

 
readonly

 
TController

 $
_controller

% 0
;

0 1
public +
RedirectToRouteResultAssertions .
(. /!
RedirectToRouteResult/ D!
redirectToRouteResultE Z
,Z [
TController\ g

controllerh r
)r s
{ 	"
_redirectToRouteResult "
=# $!
redirectToRouteResult% :
;: ;
_controller 
= 

controller $
;$ %
} 	
public +
RedirectToRouteResultAssertions .
<. /
TController/ :
>: ;
WithFragment< H
(H I
stringI O
expectedFragmentP `
)` a
{ 	"
_redirectToRouteResult "
." #
Fragment# +
.+ ,
Should, 2
(2 3
)3 4
.4 5
Be5 7
(7 8
expectedFragment8 H
)H I
;I J
return 
this 
; 
} 	
public +
RedirectToRouteResultAssertions .
<. /
TController/ :
>: ;
WithPermanent< I
(I J
boolJ N
expectedPermanentO `
)` a
{ 	"
_redirectToRouteResult "
." #
	Permanent# ,
., -
Should- 3
(3 4
)4 5
.5 6
Be6 8
(8 9
expectedPermanent9 J
)J K
;K L
return 
this 
; 
} 	
public   +
RedirectToRouteResultAssertions   .
<  . /
TController  / :
>  : ;
WithRouteName  < I
(  I J
string  J P
expectedRouteName  Q b
)  b c
{!! 	"
_redirectToRouteResult"" "
.""" #
	RouteName""# ,
."", -
Should""- 3
(""3 4
)""4 5
.""5 6
Be""6 8
(""8 9
expectedRouteName""9 J
)""J K
;""K L
return$$ 
this$$ 
;$$ 
}%% 	
public'' +
RedirectToRouteResultAssertions'' .
<''. /
TController''/ :
>'': ;
WithRouteValue''< J
(''J K
string''K Q
key''R U
,''U V
object''W ]
expectedValue''^ k
)''k l
{(( 	
expectedValue)) 
.)) 
ToExpectedObject)) *
())* +
)))+ ,
.)), -
Should))- 3
())3 4
)))4 5
.))5 6
Be))6 8
())8 9"
_redirectToRouteResult))9 O
.))O P
RouteValues))P [
[))[ \
key))\ _
]))_ `
)))` a
;))a b
return++ 
this++ 
;++ 
},, 	
}-- 
}.. Õ
I/Users/cash/Github/Baymax/Src/Baymax.Tester/StatusCodeResultAssertions.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public 

class &
StatusCodeResultAssertions +
<+ ,
TController, 7
>7 8
where9 >
TController? J
:K L

ControllerM W
{ 
private 
readonly 
StatusCodeResult )
_statusCodeResult* ;
;; <
private		 
readonly		 
TController		 $
_controller		% 0
;		0 1
public &
StatusCodeResultAssertions )
() *
StatusCodeResult* :
statusCodeResult; K
,K L
TControllerM X

controllerY c
)c d
{ 	
_statusCodeResult 
= 
statusCodeResult  0
;0 1
_controller 
= 

controller $
;$ %
} 	
public &
StatusCodeResultAssertions )
<) *
TController* 5
>5 6
WithStatusCode7 E
(E F
intF I
expectedStatudCodeJ \
)\ ]
{ 	
_statusCodeResult 
. 

StatusCode (
.( )
Should) /
(/ 0
)0 1
.1 2
Be2 4
(4 5
expectedStatudCode5 G
)G H
;H I
return 
this 
; 
} 	
} 
} ˝#
C/Users/cash/Github/Baymax/Src/Baymax.Tester/ViewResultAssertions.cs
	namespace 	
Baymax
 
. 
Tester 
{ 
public 

class  
ViewResultAssertions %
<% &
TController& 1
>1 2
where3 8
TController9 D
:E F

ControllerG Q
{ 
private		 
readonly		 

ViewResult		 #
_viewResult		$ /
;		/ 0
private

 
readonly

 
TController

 $
_controller

% 0
;

0 1
public  
ViewResultAssertions #
(# $

ViewResult$ .

viewResult/ 9
,9 :
TController; F

controllerG Q
)Q R
{ 	
_viewResult 
= 

viewResult $
;$ %
_controller 
= 

controller $
;$ %
} 	
public  
ViewResultAssertions #
<# $
TController$ /
>/ 0
	WithModel1 :
<: ;
T; <
>< =
(= >
T> ?
expected@ H
)H I
{ 	
_viewResult 
. 
Model 
. 
Should $
($ %
)% &
.& '
BeAssignableTo' 5
<5 6
T6 7
>7 8
(8 9
)9 :
;: ;
expected 
. 
ToExpectedObject %
(% &
)& '
.' (
ShouldEqual( 3
(3 4
_viewResult4 ?
.? @
Model@ E
)E F
;F G
return 
this 
; 
} 	
public  
ViewResultAssertions #
<# $
TController$ /
>/ 0
WithViewName1 =
(= >
string> D
viewNameE M
)M N
{ 	
_viewResult 
. 
ViewName  
.  !
Should! '
(' (
)( )
.) *
Be* ,
(, -
viewName- 5
)5 6
;6 7
return 
this 
; 
}   	
public""  
ViewResultAssertions"" #
<""# $
TController""$ /
>""/ 0
WithDefaultViewName""1 D
(""D E
)""E F
{## 	
_viewResult$$ 
.$$ 
ViewName$$  
.$$  !
Should$$! '
($$' (
)$$( )
.$$) *
BeEmpty$$* 1
($$1 2
)$$2 3
;$$3 4
return&& 
this&& 
;&& 
}'' 	
public))  
ViewResultAssertions)) #
<))# $
TController))$ /
>))/ 0
WithViewBag))1 <
())< =
string))= C
key))D G
,))G H
object))I O
expectedValue))P ]
)))] ^
{** 	
expectedValue++ 
.++ 
ToExpectedObject++ *
(++* +
)+++ ,
.++, -
ShouldEqual++- 8
(++8 9
_viewResult++9 D
.++D E
ViewData++E M
[++M N
key++N Q
]++Q R
)++R S
;++S T
return-- 
this-- 
;-- 
}.. 	
public00  
ViewResultAssertions00 #
<00# $
TController00$ /
>00/ 0
WithNotTempData001 @
(00@ A
)00A B
{11 	
_viewResult22 
.22 
TempData22  
.22  !
Count22! &
.22& '
Should22' -
(22- .
)22. /
.22/ 0
Be220 2
(222 3
$num223 4
)224 5
;225 6
return44 
this44 
;44 
}55 	
public77  
ViewResultAssertions77 #
<77# $
TController77$ /
>77/ 0
WithTempData771 =
(77= >
string77> D
key77E H
,77H I
object77J P
expectedValue77Q ^
)77^ _
{88 	
expectedValue99 
.99 
ToExpectedObject99 *
(99* +
)99+ ,
.99, -
ShouldEqual99- 8
(998 9
_viewResult999 D
.99D E
TempData99E M
[99M N
key99N Q
]99Q R
)99R S
;99S T
return;; 
this;; 
;;; 
}<< 	
}== 
}>> 