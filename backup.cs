using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;
using System.Text;
using System.Numerics;
using System.Globalization;
using System.Linq;
using UnityEngine.Networking;

public class DiscoSea_Web3 : MonoBehaviour
{

    byte[] feePayer_sK;
    byte[] feePayer_pK;
    byte[] Token_MetaData_Program = converTobyte("0b7065b1e3d17c45389d527f6b04c3cd58b86c731aa0fdb549b6d1bc03f82946");
    byte[] Associated_Token_Program = converTobyte("8c97258f4e2489f1bb3d1029148e0d830b5a1399daff1084048e7bd8dbe9f859");
    byte[] TOKEN_PROGRAM_ID = converTobyte("06ddf6e1d765a193d9cbe146ceeb79ac1cb485ed5f5b37913a8cf5857eff00a9");
    byte[] CMV2 = converTobyte("092aee3dfc2d0e55782313837969eaf52151c096c06b5c2a82f086a503e82c34");
    byte[] Collection_Mint = converTobyte("3cf0d1d9528cafadd61325b9b0b7245b15e17f39b67e3d96c6a30981b3dce193");
    Pda metaDataPda = new Pda();
    Pda collectionPda = new Pda();
    Pda masterEditionPda = new Pda();

    public class Keypair
    {
        public byte[] PublicKey;
        public byte[] SecretKey;
        public string PublicKeyBase58;
        public string[] mnemonic = new string[12];
    }


    public class Pda
    {
        public byte[] PublicKey;
        public byte nonce;
    }



    string[] bip39_2022 = new string[2048]  {

"abandon",
"ability",
"able",
"about",
"above",
"absent",
"absorb",
"abstract",
"absurd",
"abuse",
"access",
"accident",
"account",
"accuse",
"achieve",
"acid",
"acoustic",
"acquire",
"across",
"act",
"action",
"actor",
"actress",
"actual",
"adapt",
"add",
"addict",
"address",
"adjust",
"admit",
"adult",
"advance",
"advice",
"aerobic",
"affair",
"afford",
"afraid",
"again",
"age",
"agent",
"agree",
"ahead",
"aim",
"air",
"airport",
"aisle",
"alarm",
"album",
"alcohol",
"alert",
"alien",
"all",
"alley",
"allow",
"almost",
"alone",
"alpha",
"already",
"also",
"alter",
"always",
"amateur",
"amazing",
"among",
"amount",
"amused",
"analyst",
"anchor",
"ancient",
"anger",
"angle",
"angry",
"animal",
"ankle",
"announce",
"annual",
"another",
"answer",
"antenna",
"antique",
"anxiety",
"any",
"apart",
"apology",
"appear",
"apple",
"approve",
"april",
"arch",
"arctic",
"area",
"arena",
"argue",
"arm",
"armed",
"armor",
"army",
"around",
"arrange",
"arrest",
"arrive",
"arrow",
"art",
"artefact",
"artist",
"artwork",
"ask",
"aspect",
"assault",
"asset",
"assist",
"assume",
"asthma",
"athlete",
"atom",
"attack",
"attend",
"attitude",
"attract",
"auction",
"audit",
"august",
"aunt",
"author",
"auto",
"autumn",
"average",
"avocado",
"avoid",
"awake",
"aware",
"away",
"awesome",
"awful",
"awkward",
"axis",
"baby",
"bachelor",
"bacon",
"badge",
"bag",
"balance",
"balcony",
"ball",
"bamboo",
"banana",
"banner",
"bar",
"barely",
"bargain",
"barrel",
"base",
"basic",
"basket",
"battle",
"beach",
"bean",
"beauty",
"because",
"become",
"beef",
"before",
"begin",
"behave",
"behind",
"believe",
"below",
"belt",
"bench",
"benefit",
"best",
"betray",
"better",
"between",
"beyond",
"bicycle",
"bid",
"bike",
"bind",
"biology",
"bird",
"birth",
"bitter",
"black",
"blade",
"blame",
"blanket",
"blast",
"bleak",
"bless",
"blind",
"blood",
"blossom",
"blouse",
"blue",
"blur",
"blush",
"board",
"boat",
"body",
"boil",
"bomb",
"bone",
"bonus",
"book",
"boost",
"border",
"boring",
"borrow",
"boss",
"bottom",
"bounce",
"box",
"boy",
"bracket",
"brain",
"brand",
"brass",
"brave",
"bread",
"breeze",
"brick",
"bridge",
"brief",
"bright",
"bring",
"brisk",
"broccoli",
"broken",
"bronze",
"broom",
"brother",
"brown",
"brush",
"bubble",
"buddy",
"budget",
"buffalo",
"build",
"bulb",
"bulk",
"bullet",
"bundle",
"bunker",
"burden",
"burger",
"burst",
"bus",
"business",
"busy",
"butter",
"buyer",
"buzz",
"cabbage",
"cabin",
"cable",
"cactus",
"cage",
"cake",
"call",
"calm",
"camera",
"camp",
"can",
"canal",
"cancel",
"candy",
"cannon",
"canoe",
"canvas",
"canyon",
"capable",
"capital",
"captain",
"car",
"carbon",
"card",
"cargo",
"carpet",
"carry",
"cart",
"case",
"cash",
"casino",
"castle",
"casual",
"cat",
"catalog",
"catch",
"category",
"cattle",
"caught",
"cause",
"caution",
"cave",
"ceiling",
"celery",
"cement",
"census",
"century",
"cereal",
"certain",
"chair",
"chalk",
"champion",
"change",
"chaos",
"chapter",
"charge",
"chase",
"chat",
"cheap",
"check",
"cheese",
"chef",
"cherry",
"chest",
"chicken",
"chief",
"child",
"chimney",
"choice",
"choose",
"chronic",
"chuckle",
"chunk",
"churn",
"cigar",
"cinnamon",
"circle",
"citizen",
"city",
"civil",
"claim",
"clap",
"clarify",
"claw",
"clay",
"clean",
"clerk",
"clever",
"click",
"client",
"cliff",
"climb",
"clinic",
"clip",
"clock",
"clog",
"close",
"cloth",
"cloud",
"clown",
"club",
"clump",
"cluster",
"clutch",
"coach",
"coast",
"coconut",
"code",
"coffee",
"coil",
"coin",
"collect",
"color",
"column",
"combine",
"come",
"comfort",
"comic",
"common",
"company",
"concert",
"conduct",
"confirm",
"congress",
"connect",
"consider",
"control",
"convince",
"cook",
"cool",
"copper",
"copy",
"coral",
"core",
"corn",
"correct",
"cost",
"cotton",
"couch",
"country",
"couple",
"course",
"cousin",
"cover",
"coyote",
"crack",
"cradle",
"craft",
"cram",
"crane",
"crash",
"crater",
"crawl",
"crazy",
"cream",
"credit",
"creek",
"crew",
"cricket",
"crime",
"crisp",
"critic",
"crop",
"cross",
"crouch",
"crowd",
"crucial",
"cruel",
"cruise",
"crumble",
"crunch",
"crush",
"cry",
"crystal",
"cube",
"culture",
"cup",
"cupboard",
"curious",
"current",
"curtain",
"curve",
"cushion",
"custom",
"cute",
"cycle",
"dad",
"damage",
"damp",
"dance",
"danger",
"daring",
"dash",
"daughter",
"dawn",
"day",
"deal",
"debate",
"debris",
"decade",
"december",
"decide",
"decline",
"decorate",
"decrease",
"deer",
"defense",
"define",
"defy",
"degree",
"delay",
"deliver",
"demand",
"demise",
"denial",
"dentist",
"deny",
"depart",
"depend",
"deposit",
"depth",
"deputy",
"derive",
"describe",
"desert",
"design",
"desk",
"despair",
"destroy",
"detail",
"detect",
"develop",
"device",
"devote",
"diagram",
"dial",
"diamond",
"diary",
"dice",
"diesel",
"diet",
"differ",
"digital",
"dignity",
"dilemma",
"dinner",
"dinosaur",
"direct",
"dirt",
"disagree",
"discover",
"disease",
"dish",
"dismiss",
"disorder",
"display",
"distance",
"divert",
"divide",
"divorce",
"dizzy",
"doctor",
"document",
"dog",
"doll",
"dolphin",
"domain",
"donate",
"donkey",
"donor",
"door",
"dose",
"double",
"dove",
"draft",
"dragon",
"drama",
"drastic",
"draw",
"dream",
"dress",
"drift",
"drill",
"drink",
"drip",
"drive",
"drop",
"drum",
"dry",
"duck",
"dumb",
"dune",
"during",
"dust",
"dutch",
"duty",
"dwarf",
"dynamic",
"eager",
"eagle",
"early",
"earn",
"earth",
"easily",
"east",
"easy",
"echo",
"ecology",
"economy",
"edge",
"edit",
"educate",
"effort",
"egg",
"eight",
"either",
"elbow",
"elder",
"electric",
"elegant",
"element",
"elephant",
"elevator",
"elite",
"else",
"embark",
"embody",
"embrace",
"emerge",
"emotion",
"employ",
"empower",
"empty",
"enable",
"enact",
"end",
"endless",
"endorse",
"enemy",
"energy",
"enforce",
"engage",
"engine",
"enhance",
"enjoy",
"enlist",
"enough",
"enrich",
"enroll",
"ensure",
"enter",
"entire",
"entry",
"envelope",
"episode",
"equal",
"equip",
"era",
"erase",
"erode",
"erosion",
"error",
"erupt",
"escape",
"essay",
"essence",
"estate",
"eternal",
"ethics",
"evidence",
"evil",
"evoke",
"evolve",
"exact",
"example",
"excess",
"exchange",
"excite",
"exclude",
"excuse",
"execute",
"exercise",
"exhaust",
"exhibit",
"exile",
"exist",
"exit",
"exotic",
"expand",
"expect",
"expire",
"explain",
"expose",
"express",
"extend",
"extra",
"eye",
"eyebrow",
"fabric",
"face",
"faculty",
"fade",
"faint",
"faith",
"fall",
"FALSE",
"fame",
"family",
"famous",
"fan",
"fancy",
"fantasy",
"farm",
"fashion",
"fat",
"fatal",
"father",
"fatigue",
"fault",
"favorite",
"feature",
"february",
"federal",
"fee",
"feed",
"feel",
"female",
"fence",
"festival",
"fetch",
"fever",
"few",
"fiber",
"fiction",
"field",
"figure",
"file",
"film",
"filter",
"final",
"find",
"fine",
"finger",
"finish",
"fire",
"firm",
"first",
"fiscal",
"fish",
"fit",
"fitness",
"fix",
"flag",
"flame",
"flash",
"flat",
"flavor",
"flee",
"flight",
"flip",
"float",
"flock",
"floor",
"flower",
"fluid",
"flush",
"fly",
"foam",
"focus",
"fog",
"foil",
"fold",
"follow",
"food",
"foot",
"force",
"forest",
"forget",
"fork",
"fortune",
"forum",
"forward",
"fossil",
"foster",
"found",
"fox",
"fragile",
"frame",
"frequent",
"fresh",
"friend",
"fringe",
"frog",
"front",
"frost",
"frown",
"frozen",
"fruit",
"fuel",
"fun",
"funny",
"furnace",
"fury",
"future",
"gadget",
"gain",
"galaxy",
"gallery",
"game",
"gap",
"garage",
"garbage",
"garden",
"garlic",
"garment",
"gas",
"gasp",
"gate",
"gather",
"gauge",
"gaze",
"general",
"genius",
"genre",
"gentle",
"genuine",
"gesture",
"ghost",
"giant",
"gift",
"giggle",
"ginger",
"giraffe",
"girl",
"give",
"glad",
"glance",
"glare",
"glass",
"glide",
"glimpse",
"globe",
"gloom",
"glory",
"glove",
"glow",
"glue",
"goat",
"goddess",
"gold",
"good",
"goose",
"gorilla",
"gospel",
"gossip",
"govern",
"gown",
"grab",
"grace",
"grain",
"grant",
"grape",
"grass",
"gravity",
"great",
"green",
"grid",
"grief",
"grit",
"grocery",
"group",
"grow",
"grunt",
"guard",
"guess",
"guide",
"guilt",
"guitar",
"gun",
"gym",
"habit",
"hair",
"half",
"hammer",
"hamster",
"hand",
"happy",
"harbor",
"hard",
"harsh",
"harvest",
"hat",
"have",
"hawk",
"hazard",
"head",
"health",
"heart",
"heavy",
"hedgehog",
"height",
"hello",
"helmet",
"help",
"hen",
"hero",
"hidden",
"high",
"hill",
"hint",
"hip",
"hire",
"history",
"hobby",
"hockey",
"hold",
"hole",
"holiday",
"hollow",
"home",
"honey",
"hood",
"hope",
"horn",
"horror",
"horse",
"hospital",
"host",
"hotel",
"hour",
"hover",
"hub",
"huge",
"human",
"humble",
"humor",
"hundred",
"hungry",
"hunt",
"hurdle",
"hurry",
"hurt",
"husband",
"hybrid",
"ice",
"icon",
"idea",
"identify",
"idle",
"ignore",
"ill",
"illegal",
"illness",
"image",
"imitate",
"immense",
"immune",
"impact",
"impose",
"improve",
"impulse",
"inch",
"include",
"income",
"increase",
"index",
"indicate",
"indoor",
"industry",
"infant",
"inflict",
"inform",
"inhale",
"inherit",
"initial",
"inject",
"injury",
"inmate",
"inner",
"innocent",
"input",
"inquiry",
"insane",
"insect",
"inside",
"inspire",
"install",
"intact",
"interest",
"into",
"invest",
"invite",
"involve",
"iron",
"island",
"isolate",
"issue",
"item",
"ivory",
"jacket",
"jaguar",
"jar",
"jazz",
"jealous",
"jeans",
"jelly",
"jewel",
"job",
"join",
"joke",
"journey",
"joy",
"judge",
"juice",
"jump",
"jungle",
"junior",
"junk",
"just",
"kangaroo",
"keen",
"keep",
"ketchup",
"key",
"kick",
"kid",
"kidney",
"kind",
"kingdom",
"kiss",
"kit",
"kitchen",
"kite",
"kitten",
"kiwi",
"knee",
"knife",
"knock",
"know",
"lab",
"label",
"labor",
"ladder",
"lady",
"lake",
"lamp",
"language",
"laptop",
"large",
"later",
"latin",
"laugh",
"laundry",
"lava",
"law",
"lawn",
"lawsuit",
"layer",
"lazy",
"leader",
"leaf",
"learn",
"leave",
"lecture",
"left",
"leg",
"legal",
"legend",
"leisure",
"lemon",
"lend",
"length",
"lens",
"leopard",
"lesson",
"letter",
"level",
"liar",
"liberty",
"library",
"license",
"life",
"lift",
"light",
"like",
"limb",
"limit",
"link",
"lion",
"liquid",
"list",
"little",
"live",
"lizard",
"load",
"loan",
"lobster",
"local",
"lock",
"logic",
"lonely",
"long",
"loop",
"lottery",
"loud",
"lounge",
"love",
"loyal",
"lucky",
"luggage",
"lumber",
"lunar",
"lunch",
"luxury",
"lyrics",
"machine",
"mad",
"magic",
"magnet",
"maid",
"mail",
"main",
"major",
"make",
"mammal",
"man",
"manage",
"mandate",
"mango",
"mansion",
"manual",
"maple",
"marble",
"march",
"margin",
"marine",
"market",
"marriage",
"mask",
"mass",
"master",
"match",
"material",
"math",
"matrix",
"matter",
"maximum",
"maze",
"meadow",
"mean",
"measure",
"meat",
"mechanic",
"medal",
"media",
"melody",
"melt",
"member",
"memory",
"mention",
"menu",
"mercy",
"merge",
"merit",
"merry",
"mesh",
"message",
"metal",
"method",
"middle",
"midnight",
"milk",
"million",
"mimic",
"mind",
"minimum",
"minor",
"minute",
"miracle",
"mirror",
"misery",
"miss",
"mistake",
"mix",
"mixed",
"mixture",
"mobile",
"model",
"modify",
"mom",
"moment",
"monitor",
"monkey",
"monster",
"month",
"moon",
"moral",
"more",
"morning",
"mosquito",
"mother",
"motion",
"motor",
"mountain",
"mouse",
"move",
"movie",
"much",
"muffin",
"mule",
"multiply",
"muscle",
"museum",
"mushroom",
"music",
"must",
"mutual",
"myself",
"mystery",
"myth",
"naive",
"name",
"napkin",
"narrow",
"nasty",
"nation",
"nature",
"near",
"neck",
"need",
"negative",
"neglect",
"neither",
"nephew",
"nerve",
"nest",
"net",
"network",
"neutral",
"never",
"news",
"next",
"nice",
"night",
"noble",
"noise",
"nominee",
"noodle",
"normal",
"north",
"nose",
"notable",
"note",
"nothing",
"notice",
"novel",
"now",
"nuclear",
"number",
"nurse",
"nut",
"oak",
"obey",
"object",
"oblige",
"obscure",
"observe",
"obtain",
"obvious",
"occur",
"ocean",
"october",
"odor",
"off",
"offer",
"office",
"often",
"oil",
"okay",
"old",
"olive",
"olympic",
"omit",
"once",
"one",
"onion",
"online",
"only",
"open",
"opera",
"opinion",
"oppose",
"option",
"orange",
"orbit",
"orchard",
"order",
"ordinary",
"organ",
"orient",
"original",
"orphan",
"ostrich",
"other",
"outdoor",
"outer",
"output",
"outside",
"oval",
"oven",
"over",
"own",
"owner",
"oxygen",
"oyster",
"ozone",
"pact",
"paddle",
"page",
"pair",
"palace",
"palm",
"panda",
"panel",
"panic",
"panther",
"paper",
"parade",
"parent",
"park",
"parrot",
"party",
"pass",
"patch",
"path",
"patient",
"patrol",
"pattern",
"pause",
"pave",
"payment",
"peace",
"peanut",
"pear",
"peasant",
"pelican",
"pen",
"penalty",
"pencil",
"people",
"pepper",
"perfect",
"permit",
"person",
"pet",
"phone",
"photo",
"phrase",
"physical",
"piano",
"picnic",
"picture",
"piece",
"pig",
"pigeon",
"pill",
"pilot",
"pink",
"pioneer",
"pipe",
"pistol",
"pitch",
"pizza",
"place",
"planet",
"plastic",
"plate",
"play",
"please",
"pledge",
"pluck",
"plug",
"plunge",
"poem",
"poet",
"point",
"polar",
"pole",
"police",
"pond",
"pony",
"pool",
"popular",
"portion",
"position",
"possible",
"post",
"potato",
"pottery",
"poverty",
"powder",
"power",
"practice",
"praise",
"predict",
"prefer",
"prepare",
"present",
"pretty",
"prevent",
"price",
"pride",
"primary",
"print",
"priority",
"prison",
"private",
"prize",
"problem",
"process",
"produce",
"profit",
"program",
"project",
"promote",
"proof",
"property",
"prosper",
"protect",
"proud",
"provide",
"public",
"pudding",
"pull",
"pulp",
"pulse",
"pumpkin",
"punch",
"pupil",
"puppy",
"purchase",
"purity",
"purpose",
"purse",
"push",
"put",
"puzzle",
"pyramid",
"quality",
"quantum",
"quarter",
"question",
"quick",
"quit",
"quiz",
"quote",
"rabbit",
"raccoon",
"race",
"rack",
"radar",
"radio",
"rail",
"rain",
"raise",
"rally",
"ramp",
"ranch",
"random",
"range",
"rapid",
"rare",
"rate",
"rather",
"raven",
"raw",
"razor",
"ready",
"real",
"reason",
"rebel",
"rebuild",
"recall",
"receive",
"recipe",
"record",
"recycle",
"reduce",
"reflect",
"reform",
"refuse",
"region",
"regret",
"regular",
"reject",
"relax",
"release",
"relief",
"rely",
"remain",
"remember",
"remind",
"remove",
"render",
"renew",
"rent",
"reopen",
"repair",
"repeat",
"replace",
"report",
"require",
"rescue",
"resemble",
"resist",
"resource",
"response",
"result",
"retire",
"retreat",
"return",
"reunion",
"reveal",
"review",
"reward",
"rhythm",
"rib",
"ribbon",
"rice",
"rich",
"ride",
"ridge",
"rifle",
"right",
"rigid",
"ring",
"riot",
"ripple",
"risk",
"ritual",
"rival",
"river",
"road",
"roast",
"robot",
"robust",
"rocket",
"romance",
"roof",
"rookie",
"room",
"rose",
"rotate",
"rough",
"round",
"route",
"royal",
"rubber",
"rude",
"rug",
"rule",
"run",
"runway",
"rural",
"sad",
"saddle",
"sadness",
"safe",
"sail",
"salad",
"salmon",
"salon",
"salt",
"salute",
"same",
"sample",
"sand",
"satisfy",
"satoshi",
"sauce",
"sausage",
"save",
"say",
"scale",
"scan",
"scare",
"scatter",
"scene",
"scheme",
"school",
"science",
"scissors",
"scorpion",
"scout",
"scrap",
"screen",
"script",
"scrub",
"sea",
"search",
"season",
"seat",
"second",
"secret",
"section",
"security",
"seed",
"seek",
"segment",
"select",
"sell",
"seminar",
"senior",
"sense",
"sentence",
"series",
"service",
"session",
"settle",
"setup",
"seven",
"shadow",
"shaft",
"shallow",
"share",
"shed",
"shell",
"sheriff",
"shield",
"shift",
"shine",
"ship",
"shiver",
"shock",
"shoe",
"shoot",
"shop",
"short",
"shoulder",
"shove",
"shrimp",
"shrug",
"shuffle",
"shy",
"sibling",
"sick",
"side",
"siege",
"sight",
"sign",
"silent",
"silk",
"silly",
"silver",
"similar",
"simple",
"since",
"sing",
"siren",
"sister",
"situate",
"six",
"size",
"skate",
"sketch",
"ski",
"skill",
"skin",
"skirt",
"skull",
"slab",
"slam",
"sleep",
"slender",
"slice",
"slide",
"slight",
"slim",
"slogan",
"slot",
"slow",
"slush",
"small",
"smart",
"smile",
"smoke",
"smooth",
"snack",
"snake",
"snap",
"sniff",
"snow",
"soap",
"soccer",
"social",
"sock",
"soda",
"soft",
"solar",
"soldier",
"solid",
"solution",
"solve",
"someone",
"song",
"soon",
"sorry",
"sort",
"soul",
"sound",
"soup",
"source",
"south",
"space",
"spare",
"spatial",
"spawn",
"speak",
"special",
"speed",
"spell",
"spend",
"sphere",
"spice",
"spider",
"spike",
"spin",
"spirit",
"split",
"spoil",
"sponsor",
"spoon",
"sport",
"spot",
"spray",
"spread",
"spring",
"spy",
"square",
"squeeze",
"squirrel",
"stable",
"stadium",
"staff",
"stage",
"stairs",
"stamp",
"stand",
"start",
"state",
"stay",
"steak",
"steel",
"stem",
"step",
"stereo",
"stick",
"still",
"sting",
"stock",
"stomach",
"stone",
"stool",
"story",
"stove",
"strategy",
"street",
"strike",
"strong",
"struggle",
"student",
"stuff",
"stumble",
"style",
"subject",
"submit",
"subway",
"success",
"such",
"sudden",
"suffer",
"sugar",
"suggest",
"suit",
"summer",
"sun",
"sunny",
"sunset",
"super",
"supply",
"supreme",
"sure",
"surface",
"surge",
"surprise",
"surround",
"survey",
"suspect",
"sustain",
"swallow",
"swamp",
"swap",
"swarm",
"swear",
"sweet",
"swift",
"swim",
"swing",
"switch",
"sword",
"symbol",
"symptom",
"syrup",
"system",
"table",
"tackle",
"tag",
"tail",
"talent",
"talk",
"tank",
"tape",
"target",
"task",
"taste",
"tattoo",
"taxi",
"teach",
"team",
"tell",
"ten",
"tenant",
"tennis",
"tent",
"term",
"test",
"text",
"thank",
"that",
"theme",
"then",
"theory",
"there",
"they",
"thing",
"this",
"thought",
"three",
"thrive",
"throw",
"thumb",
"thunder",
"ticket",
"tide",
"tiger",
"tilt",
"timber",
"time",
"tiny",
"tip",
"tired",
"tissue",
"title",
"toast",
"tobacco",
"today",
"toddler",
"toe",
"together",
"toilet",
"token",
"tomato",
"tomorrow",
"tone",
"tongue",
"tonight",
"tool",
"tooth",
"top",
"topic",
"topple",
"torch",
"tornado",
"tortoise",
"toss",
"total",
"tourist",
"toward",
"tower",
"town",
"toy",
"track",
"trade",
"traffic",
"tragic",
"train",
"transfer",
"trap",
"trash",
"travel",
"tray",
"treat",
"tree",
"trend",
"trial",
"tribe",
"trick",
"trigger",
"trim",
"trip",
"trophy",
"trouble",
"truck",
"true",
"truly",
"trumpet",
"trust",
"truth",
"try",
"tube",
"tuition",
"tumble",
"tuna",
"tunnel",
"turkey",
"turn",
"turtle",
"twelve",
"twenty",
"twice",
"twin",
"twist",
"two",
"type",
"typical",
"ugly",
"umbrella",
"unable",
"unaware",
"uncle",
"uncover",
"under",
"undo",
"unfair",
"unfold",
"unhappy",
"uniform",
"unique",
"unit",
"universe",
"unknown",
"unlock",
"until",
"unusual",
"unveil",
"update",
"upgrade",
"uphold",
"upon",
"upper",
"upset",
"urban",
"urge",
"usage",
"use",
"used",
"useful",
"useless",
"usual",
"utility",
"vacant",
"vacuum",
"vague",
"valid",
"valley",
"valve",
"van",
"vanish",
"vapor",
"various",
"vast",
"vault",
"vehicle",
"velvet",
"vendor",
"venture",
"venue",
"verb",
"verify",
"version",
"very",
"vessel",
"veteran",
"viable",
"vibrant",
"vicious",
"victory",
"video",
"view",
"village",
"vintage",
"violin",
"virtual",
"virus",
"visa",
"visit",
"visual",
"vital",
"vivid",
"vocal",
"voice",
"void",
"volcano",
"volume",
"vote",
"voyage",
"wage",
"wagon",
"wait",
"walk",
"wall",
"walnut",
"want",
"warfare",
"warm",
"warrior",
"wash",
"wasp",
"waste",
"water",
"wave",
"way",
"wealth",
"weapon",
"wear",
"weasel",
"weather",
"web",
"wedding",
"weekend",
"weird",
"welcome",
"west",
"wet",
"whale",
"what",
"wheat",
"wheel",
"when",
"where",
"whip",
"whisper",
"wide",
"width",
"wife",
"wild",
"will",
"win",
"window",
"wine",
"wing",
"wink",
"winner",
"winter",
"wire",
"wisdom",
"wise",
"wish",
"witness",
"wolf",
"woman",
"wonder",
"wood",
"wool",
"word",
"work",
"world",
"worry",
"worth",
"wrap",
"wreck",
"wrestle",
"wrist",
"write",
"wrong",
"yard",
"year",
"yellow",
"you",
"young",
"youth",
"zebra",
"zero",
"zone",
"zoo"
    };
    string[] mnemonicArray = new string[12];

    // Declare variables to hold the generated mnemonic string
    string mnemonic_string = "";

    // Declare a static RNGCryptoServiceProvider instance for generating random numbers
    private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

    // Declare a byte array to hold the generated random number
    byte[] randomNumber = new byte[32];


    //byte[] msg24;
    string txSignature = "";
    byte[] msg24;


    BigInteger q = BigInteger.Pow(2, 252) + BigInteger.Parse("27742317777372353535851937790883648493");

    static BigInteger p = BigInteger.Parse("57896044618658097711785492504343953926634992332820282019728792003956564819949");
    static BigInteger d = BigInteger.Parse("37095705934669439343138083508754565189542113879843219016388785533085940283555");

    BigInteger[] G = {
        BigInteger.Parse("15112221349535400772501151409588531511454012693041857206046113283949847762202"),
        BigInteger.Parse("46316835694926478169428394003475163141307993866256225615783033603165251855960"),
        BigInteger.Parse("1"),
        BigInteger.Parse("46827403850823179245072216630277197565144205554125654976674165829533817101731")
    };

    // Compute the square root of -1 modulo p
    static readonly BigInteger modp_sqrt_m1 = BigInteger.ModPow(2, (p - 1) / 4, p);


    public Keypair GenerateKeyPair()
    {
        // Fill the array with a random value.
        rngCsp.GetBytes(randomNumber);

        //test vector
        //https://medium.com/coinmonks/mnemonic-generation-bip39-simply-explained-e9ac18db9477
        //byte[] testByte = new byte[] { 0x0, 0x6, 0x3, 0x6, 0x7, 0x9, 0xc, 0xa,0x1,0xb,0x2,0x8,0xb,0x5,0xc,0xf,0xd,0xa,0x9,0xc,0x1,0x8,0x6,0xb,0x3,0x6,0x7,0xe,0x2,0x7,0x1,0xe };

        string byteRow = "";
        string entrapy = "";
        foreach (byte b in randomNumber)
        {
            byteRow = byteRow + b.ToString("X2");
            entrapy = entrapy + Convert.ToString(b, 2).PadLeft(4, '0');
        }
        Debug.Log("TestByte");
        Debug.Log(byteRow);
        Debug.Log(byteRow.Length);

        int NumberChars = byteRow.Length;
        byte[] bytes = new byte[NumberChars / 2];

        for (int i = 0; i < NumberChars; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(byteRow.Substring(i, 2), 16);
        }


        //hash entropy
        byte[] result;
        SHA256 mySHA256 = SHA256.Create();
        result = mySHA256.ComputeHash(bytes);



        Debug.Log(result);

        //byteRow = "";
        //foreach (byte b in result)
        //{
        //    //byteRow = byteRow + "," + Convert.ToString(b,2).PadLeft(4,'0');
        //    byteRow = byteRow + b.ToString("X");
        //}
        //Debug.Log("Byte Row");
        //Debug.Log(byteRow);

        Debug.Log(result[0].ToString("X"));

        string firstByte = Convert.ToString(result[0], 2).PadLeft(8, '0');
        //firstByte = Convert.ToString(Convert.ToByte(firstByte), 2).PadLeft(4, '0');

        Debug.Log(firstByte);
        entrapy = entrapy + firstByte;


        int z = 0;
        mnemonic_string = "";
        for (int i = 0; i < 12; i++)
        {
            mnemonicArray[i] = bip39_2022[Convert.ToInt32(entrapy.Substring(z, 11), 2)];

            if (i < 11)
            {
                mnemonic_string = mnemonic_string + bip39_2022[Convert.ToInt32(entrapy.Substring(z, 11), 2)] + " ";
            }
            else
            {
                mnemonic_string = mnemonic_string + bip39_2022[Convert.ToInt32(entrapy.Substring(z, 11), 2)];
            }

            z = z + 11;
        }

        string passphrase = "";

        // Convert mnemonic and passphrase to UTF-8 NFKD byte arrays
        byte[] mnemonicBytes = Encoding.UTF8.GetBytes(mnemonic_string.Normalize(NormalizationForm.FormKD));
        byte[] saltBytes = Encoding.UTF8.GetBytes("mnemonic" + passphrase.Normalize(NormalizationForm.FormKD));

        // Generate key using PBKDF2 with HMAC-SHA512
        Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(mnemonicBytes, saltBytes, 2048, HashAlgorithmName.SHA512);
        byte[] keyBytes = pbkdf2.GetBytes(64);

        // sK
        byte[] sK = new byte[32]; // new array to hold the 32 bytes

        // Copy the first 32 bytes of the full array to the sub array
        Array.Copy(keyBytes, 0, sK, 0, 32);

        // pK
        byte[] pK = new byte[32];
        pK = getPubV2(sK);


        ////print pK and sK
        //int[] subArray = new int[32];
        //Array.Copy(pK, 0, subArray, 0, subArray.Length);
        //string subArrayString = string.Join(", ", subArray);
        //Debug.Log("PublicKey: "+subArrayString);

        //int[] subArray2 = new int[32];
        //Array.Copy(sK, 0, subArray2, 0, subArray2.Length);
        //string subArrayString2 = string.Join(", ", subArray2);
        //Debug.Log("SecretKey: " + subArrayString2);

        Keypair keypair = new Keypair();
        keypair.PublicKey = pK;
        keypair.SecretKey = sK;
        keypair.PublicKeyBase58 = returnSolanaPubKey(0, 32, pK);
        keypair.mnemonic = mnemonicArray;

        // Return the generated mnemonic string
        return keypair;
    }
    
    BigInteger[] point_add(BigInteger[] P, BigInteger[] Q)
    {
        BigInteger A = (((P[1] - P[0]) * (Q[1] - Q[0]) % p) + p) % p;
        BigInteger B = (((P[1] + P[0]) * (Q[1] + Q[0]) % p) + p) % p;

        BigInteger C = ((2 * P[3] * Q[3] * d % p) + p) % p;
        BigInteger D = ((2 * P[2] * Q[2] % p) + p) % p;

        BigInteger E = B - A;
        BigInteger F = D - C;
        BigInteger G = D + C;
        BigInteger H = B + A;

        BigInteger[] bByte = { E * F, G * H, F * G, E * H };


        return bByte;
    }

    BigInteger[] point_mul(BigInteger s, BigInteger[] P)
    {

        BigInteger[] Q = { BigInteger.Parse("0"), BigInteger.Parse("1"), BigInteger.Parse("1"), BigInteger.Parse("0") };


        while (s > 0)
        {

            if ((s & 1) == 1)
            {
                Q = point_add(Q, P);
            }

            P = point_add(P, P);

            s >>= 1;

        }

        return Q;

    }

    byte[] point_compress(BigInteger[] P)
    {

        BigInteger zinv = BigInteger.ModPow(P[2], p - 2, p);
        BigInteger x = ((P[0] * zinv % p) + p) % p;
        BigInteger y = ((P[1] * zinv % p) + p) % p;

        byte[] publickKey = (y | ((x & 1) << 255)).ToByteArray();

        return publickKey;
       
    }

    string expand_sk_a(byte[] sK)
    {

        //ed25519 sequence
        byte[] hash;
        SHA512 shaM = new SHA512Managed();
        hash = shaM.ComputeHash(sK);

       
         //   Debug.Log("SHA-512 of Secret is: " + BitConverter.ToString(hash).Replace("-", ""));
        

        hash[0] = (byte)(hash[0] & 0xF8);
        hash[31] = (byte)(hash[31] & 0x7F);
        hash[31] = (byte)(hash[31] | 0x40);
        byte[] tempRes = new byte[32];

        for (int i = 0; i < 32; i++)
        {
            tempRes[i] = hash[i];
        }

        hash = tempRes;


        //little endian 
        Array.Reverse(hash);

        string hexString = BitConverter.ToString(hash).Replace("-", "");

        //a part of sK expand
      
        //    Debug.Log("Secret Scalar: " + BigInteger.Parse(hexString, NumberStyles.AllowHexSpecifier));
        
            


        return hexString;



    }

    byte[] expand_sk_prefix(byte[] sK)
    {
        //ed25519 sequence
        byte[] hash;
        SHA512 shaM = new SHA512Managed();
        hash = shaM.ComputeHash(sK);

        byte[] subarray = hash.Skip(32).ToArray();
        return subarray;
    }

    //base58 encoding
    string base_58_encoding(byte[] publicKey)
    {
        byte[] truncArray = new byte[32];
        Array.Copy(publicKey, truncArray, truncArray.Length);

        BigInteger Base58Divisor = 58;

        char[] Base58Map = new char[] {
            '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F',
            'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W',
            'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };

        var input = new BigInteger(truncArray, isUnsigned: true, isBigEndian: true);
        var remainders = new List<int>();
        while (input > 0)
        {
            input = BigInteger.DivRem(input, Base58Divisor, out var remainder);
            remainders.Add((int)remainder);
        }

        //preserve leading zeros
        foreach (var b in truncArray)
        {
            if (b == 0) { remainders.Add(0); } else { break; }
        }

        remainders.Reverse();

        var sbOutput = new StringBuilder();
        foreach (var remainder in remainders)
        {
            sbOutput.Append(Base58Map[remainder]);
        }
        string base58_pubkey = sbOutput.ToString();

        return base58_pubkey;

    }

    BigInteger Sha512Modq(byte[] s)
    {

        SHA512 shaM = new SHA512Managed();
        byte[] hash = shaM.ComputeHash(s);

     
        Debug.Log("********SHA TIME*************");
        Debug.Log("Secret");
        Debug.Log(returnArrayString(0, s.Length, s));
        Debug.Log(BitConverter.ToString(s).Replace("-", ""));
        Debug.Log("HASH");
        Debug.Log(returnArrayString(0, 64, hash));
        Debug.Log(BitConverter.ToString(hash).Replace("-", ""));
     


        byte[] unsignedBytes = new byte[hash.Length + 1]; // add an extra byte
        hash.CopyTo(unsignedBytes, 0); // copy the original bytes
        BigInteger unsignedBigInt = new BigInteger(unsignedBytes);

      
        
        Debug.Log(unsignedBigInt);
        Debug.Log(unsignedBigInt % q);
        

        return unsignedBigInt % q;
    }

    byte[] sign_msg(byte[] sK, byte[] msg)
    {

        string hexString = expand_sk_a(sK);

        BigInteger sNum = BigInteger.Parse(hexString, NumberStyles.AllowHexSpecifier);
        BigInteger[] Q = point_mul(sNum, G);
        byte[] A = point_compress(point_mul(sNum, G));

        byte[] prefix = expand_sk_prefix(sK);

        BigInteger r = Sha512Modq(prefix.Concat(msg).ToArray());

        BigInteger[] R = point_mul(r, G);
        byte[] Rs = point_compress(R);
        
        BigInteger h = Sha512Modq(Rs.Take(32).Concat(A.Take(32)).Concat(msg).ToArray());
        BigInteger s = (r + h * sNum) % q;

        byte[] bytes = s.ToByteArray();

      
            //Debug.Log("*****************SIGNATURE LOCAL VARS*********************");
            //Debug.Log("hexString");
            //Debug.Log(hexString);
            //Debug.Log("sNum");
            //Debug.Log(sNum);
            //Debug.Log("Q");
            //Debug.Log(Q);
            //Debug.Log("A");
            //Debug.Log(returnArrayString(0,32,A));
            //Debug.Log(prefix);
            //Debug.Log("r");
            //Debug.Log(r);
            //Debug.Log(R);
            //Debug.Log("Rs");
            //Debug.Log(returnArrayString(0,32, Rs));
            //Debug.Log("Rs.Take(32).Concat(A).Concat(msg).ToArray().Length");
            //Debug.Log(Rs.Take(32).Concat(A).Concat(msg).ToArray().Length);
            //Debug.Log((Rs.Take(32).Concat(A).Concat(msg).ToArray().Sum(b=>(int)b)));
            //Debug.Log("h");
            //Debug.Log(h);
            //Debug.Log(s);
            //Debug.Log(bytes);
            //Debug.Log(Rs.Take(32).Concat(bytes).ToArray());
            
        

        return Rs.Take(32).Concat(bytes).ToArray();

    }

    public byte[] GetPublicKey(string mnemonic_string)
    {

        string passphrase = "";

        // Convert mnemonic and passphrase to UTF-8 NFKD byte arrays
        byte[] mnemonicBytes = Encoding.UTF8.GetBytes(mnemonic_string.Normalize(NormalizationForm.FormKD));
        byte[] saltBytes = Encoding.UTF8.GetBytes("mnemonic" + passphrase.Normalize(NormalizationForm.FormKD));

        // Generate key using PBKDF2 with HMAC-SHA512
        Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(mnemonicBytes, saltBytes, 2048, HashAlgorithmName.SHA512);
        byte[] keyBytes = pbkdf2.GetBytes(64);

        // sK
        byte[] sK = new byte[32]; // new array to hold the 32 bytes

        // Copy the first 32 bytes of the full array to the sub array
        Array.Copy(keyBytes, 0, sK, 0, 32);

        //rfc test vector
        //string secret = "c5aa8df43f9f837bedb7442f31dcb7b166d38535076f094b85ce3a2e0b4458f7";

        //string secret = "EA1622C08C673538021A4CD547033162A0C52C2168FCABC3D1BE0914B0F6DB35";

        string hexString = expand_sk_a(sK);


        BigInteger sNum = BigInteger.Parse(hexString, NumberStyles.AllowHexSpecifier);


        BigInteger[] Q = point_mul(sNum, G);


        byte[] publicKey = point_compress(point_mul(sNum, G));


        //hexString = BitConverter.ToString(publicKey).Replace("-", "");


       
        //    //Debug.Log("ed25519 PubKey: " + hexString);
        //    //Debug.Log("Solana PubKey: " + base_58_encoding(publicKey));
        


        //byte[] msg1 = { 0x01, 0x00, 0x01, 0x03 };

        ////from pubkey
        //byte[] msg2 = { 0x82, 0x09, 0x69, 0x6e, 0x38, 0x5a, 0x2a, 0x50, 0x80, 0x65, 0xfa, 0xe4, 0x6b,
        //                0xd2, 0x7a, 0xc8, 0xbd, 0x7c, 0xfa, 0xae, 0x6c, 0xe0, 0x94, 0xe3, 0x66, 0xec,
        //                0x05, 0x11, 0x13, 0x2b, 0x00, 0xfb };
        ////to pubkey
        //byte[] msg3 = { 0x9b, 0x88, 0xa9, 0x04, 0xc8, 0x5b, 0xa6, 0xaa, 0x4c, 0x07, 0xa1, 0x79, 0xfb,
        //                0xe9, 0x11, 0xc7, 0xcf, 0x68, 0x8e, 0x21, 0x62, 0x8c, 0xbf, 0xea, 0x24, 0x0e,
        //                0x12, 0x43, 0x27, 0xcb, 0x65, 0x50 };

        ////solana system programID
        //byte[] msg4 = new byte[32];

        ////recent block hash
        //byte[] msg5 = { 0x3e, 0xd8, 0x93, 0x98, 0x01, 0x2f, 0x67, 0x06, 0x5f, 0xd5, 0xe6, 0x7f, 0xcc,
        //                0x77, 0x6f, 0x19, 0x5a, 0xfd, 0x52, 0x12, 0x07, 0x53, 0x47, 0x07, 0xfa, 0x4f,
        //                0xcd, 0x09, 0x3c, 0x29, 0x29, 0x06};

        //byte[] msg6 = { 0x01, 0x02, 0x02, 0x00, 0x01, 0x0c };

        //byte[] msg7 = { 0x02, 0x00, 0x00, 0x00, 0xe8, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };



        //byte[] msg = msg1
        //    .Concat(msg2)
        //    .Concat(msg3)
        //    .Concat(msg4)
        //    .Concat(msg5)
        //    .Concat(msg6)
        //    .Concat(msg7)
        //    .ToArray();

     
        //   // Debug.Log("msg: " + BitConverter.ToString(msg).Replace("-", ""));
        

        //byte[] signature = sign_msg(sK, msg);

       
        //   // Debug.Log("Signature: " + BitConverter.ToString(signature).Replace("-", ""));
        

        //byte[] msgPreFix = { 0x01 };

        //string rawTransaction = Convert.ToBase64String(msgPreFix.Concat(signature).Concat(msg).ToArray());

       
        ////Debug.Log(rawTransaction);
        


        return publicKey;


    }


    public byte[] GetSkFromMnemonic(string mnemonic_string)
    {

        string passphrase = "";

        // Convert mnemonic and passphrase to UTF-8 NFKD byte arrays
        byte[] mnemonicBytes = Encoding.UTF8.GetBytes(mnemonic_string.Normalize(NormalizationForm.FormKD));
        byte[] saltBytes = Encoding.UTF8.GetBytes("mnemonic" + passphrase.Normalize(NormalizationForm.FormKD));

        // Generate key using PBKDF2 with HMAC-SHA512
        Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(mnemonicBytes, saltBytes, 2048, HashAlgorithmName.SHA512);
        byte[] keyBytes = pbkdf2.GetBytes(64);

        // sK
        byte[] sK = new byte[32]; // new array to hold the 32 bytes

        // Copy the first 32 bytes of the full array to the sub array
        Array.Copy(keyBytes, 0, sK, 0, 32);

        return sK;


    }

    public byte[] GetPkFromSk(byte[] sK)
    {
        string hexString = expand_sk_a(sK);

        BigInteger sNum = BigInteger.Parse(hexString, NumberStyles.AllowHexSpecifier);

        BigInteger[] Q = point_mul(sNum, G);

        byte[] publicKey = point_compress(point_mul(sNum, G));

        // sK
        byte[] pK = new byte[32]; // new array to hold the 32 bytes

        // Copy the first 32 bytes of the full array to the sub array
        Array.Copy(publicKey, 0, pK, 0, 32);

        return pK;

    }





    // Define the coefficient a of the elliptic curve equation
    static readonly BigInteger a = 486662;

    // Define the coefficient b of the elliptic curve equation
    static readonly BigInteger b = 1;

    // Compute the modular inverse of x modulo p
    static BigInteger modp_inv(BigInteger x)
    {
        return BigInteger.ModPow(x, p - 2, p);
    }


    // Compute corresponding x-coordinate, with low bit corresponding to
    // sign, or return null on failure
    static BigInteger? RecoverX(BigInteger y, int sign)
    {
        if (y >= p)
        {
            return null;
        }

        BigInteger x2 = (y * y - 1) * modp_inv(d * y * y + 1);
        

        if (x2 == 0)
        {
            if (sign == 1)
            {
                return null;
            }
            else
            {
                return 0;
            }
        }

        // Compute square root of x2
        BigInteger x = BigInteger.ModPow(x2, (p + 3) / 8, p);

     
        if ((x * x - x2) % p != 0)
        {
            x = (x * modp_sqrt_m1) % p;
        }

        if ((x * x - x2) % p != 0)
        {
            return null;
        }

        if ((x & 1) != sign)
        {
            x = p - x;
        }

        return x;
    }

    // Decompress a compressed point (y, sign) and return the corresponding
    // uncompressed point (x, y, 1, x*y % p)
    static Tuple<BigInteger, BigInteger, BigInteger, BigInteger> PointDecompress(byte[] s)
    {
        if (s.Length != 32)
        {
            throw new ArgumentException("Invalid input length for decompression");
            
        }

        BigInteger y = new BigInteger(s);


        BigInteger sign = y >> 255;
        y &= (BigInteger.Pow(2, 255) - 1);

   

        BigInteger? x = RecoverX(y, (int)sign);
        if (x == null)
        {
            return null;
        }

        return new Tuple<BigInteger, BigInteger, BigInteger, BigInteger>(
            x.Value, y, BigInteger.One, (x.Value * y) % p);
    }


    static byte[] converTobyte(string str)
    {

        byte[] res = Enumerable.Range(0, str.Length)
                 .Where(x => x % 2 == 0)
                 .Select(x => Convert.ToByte(str.Substring(x, 2), 16))
                 .ToArray();

        return res;

    }

    byte[] getPubV2(byte[] sK)
    {
        string hexString = expand_sk_a(sK);


        BigInteger sNum = BigInteger.Parse(hexString, NumberStyles.AllowHexSpecifier);


        BigInteger[] Q = point_mul(sNum, G);


        byte[] publicKey = point_compress(point_mul(sNum, G));

        if (publicKey.Length > 32)
        {
            // If the byte array is too long, truncate it to 32 bytes
            Array.Resize(ref publicKey, 32);
        }


        hexString = BitConverter.ToString(publicKey).Replace("-", "");

        
           // Debug.Log("ed25519 PubKey: " + hexString);
        
        return publicKey;

    }

    byte[] mnemonic_to_sk(string mnemonic_string)
    {
        string passphrase = "";

        // Convert mnemonic and passphrase to UTF-8 NFKD byte arrays
        byte[] mnemonicBytes = Encoding.UTF8.GetBytes(mnemonic_string.Normalize(NormalizationForm.FormKD));
        byte[] saltBytes = Encoding.UTF8.GetBytes("mnemonic" + passphrase.Normalize(NormalizationForm.FormKD));

        // Generate key using PBKDF2 with HMAC-SHA512
        Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(mnemonicBytes, saltBytes, 2048, HashAlgorithmName.SHA512);
        byte[] keyBytes = pbkdf2.GetBytes(64);

        // sK
        byte[] sK = new byte[32]; // new array to hold the 32 bytes

        // Copy the first 32 bytes of the full array to the sub array
        Array.Copy(keyBytes, 0, sK, 0, 32);

        return keyBytes;
    }



    void metaMint()
    {
        
        string mnemonic = "pill tomorrow foster begin walnut borrow virtual kick shift mutual shoe scatter";


        feePayer_sK = GetSkFromMnemonic(mnemonic);
        feePayer_pK = GetPkFromSk(feePayer_sK);


        //byte[] mint_account_sK = converTobyte("CAABC08196BDCCF18E47CD02516102B030512D01608ADC84E783784D422861AC");
        //byte[] mint_account_pK = GetPkFromSk(mint_account_sK);

        Keypair keypair = new Keypair();
        keypair = GenerateKeyPair();
        byte[] mint_account_sK = keypair.SecretKey;
        byte[] mint_account_pK = keypair.PublicKey;


        byte[] seed;

        //byte[] mint_account_sK = keypair.SecretKey;

        //transaction base [2,0,14,23]
        byte[] msg0 = { 0x02, 0x00, 0x0E, 0x17 };

        //1 feePayer public key
        //byte[] msg1 = getPubV2(feePayer_sK);
        byte[] msg1 = feePayer_pK;

        //2: Mint Account public key
        byte[] msg2 = mint_account_pK;


        //3: Collection Pda
        //byte[] msg3 = converTobyte("2375d6c64234d05e0a487a48f75b794a6daae9695f555f57060907480fbf1718");
        string collectionPDA = "collection";
        byte[] collectionA = Encoding.ASCII.GetBytes(collectionPDA);
        seed = collectionA.Concat(converTobyte("f95cb71bb699fb7d31f9c7b7e95dd58719594455448ac64e822f87dbc8630800")).ToArray();
        collectionPda = findProgramAddressSync(seed, CMV2, collectionPda);
        byte[] msg3 = collectionPda.PublicKey;

        //4: Master Edition
        //byte[] msg4 = converTobyte("2fa4528add9677f48ae1bf55ff73522749009964d9e846dbe6d0b1f1046d2fdd");
        string metadata_string = "metadata";
        byte[] metadata_byte = Encoding.ASCII.GetBytes(metadata_string);
        string edition_string = "edition";
        byte[] edition_byte = Encoding.ASCII.GetBytes(edition_string);
        seed = metadata_byte.Concat(Token_MetaData_Program).Concat(mint_account_pK).Concat(edition_byte).ToArray();
        masterEditionPda = findProgramAddressSync(seed, Token_MetaData_Program, masterEditionPda);
        byte[] msg4 = masterEditionPda.PublicKey;

        //5: Associated Token Program: Account
        seed = feePayer_pK.Concat(TOKEN_PROGRAM_ID).Concat(msg2).ToArray();
        byte[] msg5 = findProgramAddressSync(seed, Associated_Token_Program, metaDataPda).PublicKey;

        //6: Collection Metadata
        //byte[] msg6 = converTobyte("f0a630e4329b26a08e2241c2f511597ef2731074429ccccc0aec0be1ea1355bf");
        seed = metadata_byte.Concat(Token_MetaData_Program).Concat(Collection_Mint).ToArray();
        masterEditionPda = findProgramAddressSync(seed, Token_MetaData_Program, masterEditionPda);
        byte[] msg6 = masterEditionPda.PublicKey;


        //7: Wallet Account / Creator Real
        byte[] msg7 = converTobyte("f294ce018e8ab0fdb02d86321f8a727cecce3ac3821f4241faf73a0f792e2fae");

        //8: Candy Machine ID
        byte[] msg8 = converTobyte("f95cb71bb699fb7d31f9c7b7e95dd58719594455448ac64e822f87dbc8630800");


        //9: METADATA
        //byte[] msg9 = converTobyte("f50068f43e7b6541a4ff1cbb0f4d4d3888d853d69db702b638827203c72602f0");
        string myString = "metadata";
        byte[] t1 = Encoding.ASCII.GetBytes(myString);
        Debug.Log(returnArrayString(0, t1.Length, t1));
        //17 Token_MetaData_Program
        byte[] t2 = Token_MetaData_Program;
        //mint_account_pK
        byte[] t3 = msg2;
        seed = t1.Concat(t2).Concat(t3).ToArray();
        metaDataPda = findProgramAddressSync(seed, Token_MetaData_Program, metaDataPda);
        byte[] msg9 = metaDataPda.PublicKey;

        //10: SystemProgram
        byte[] msg10 = converTobyte("0000000000000000000000000000000000000000000000000000000000000000");

        //11 Collection Mint
        //byte[] msg11 = converTobyte("3cf0d1d9528cafadd61325b9b0b7245b15e17f39b67e3d96c6a30981b3dce193");
        byte[] msg11 = Collection_Mint;

        //12 Collection Authority Record
        //byte[] msg12 = converTobyte("7f4378eabb6dae1f2edc41b923ef56978555ed4ecd85af848907ec509f966ecd");
        string collection_authority = "collection_authority";
        byte[] collection_authority_byte = Encoding.ASCII.GetBytes(collection_authority);
        seed = metadata_byte.Concat(Token_MetaData_Program).Concat(Collection_Mint).Concat(collection_authority_byte).Concat(collectionPda.PublicKey).ToArray();
        byte[] msg12 = findProgramAddressSync(seed, Token_MetaData_Program, masterEditionPda).PublicKey;

        //13 Collection Master Edition
        //byte[] msg13 = converTobyte("9022aec47631a8aba7c7bdac46003110ee4ae5adef07f3aed843a925e80da253");
        seed = metadata_byte.Concat(Token_MetaData_Program).Concat(Collection_Mint).Concat(edition_byte).ToArray();
        byte[] msg13 = findProgramAddressSync(seed, Token_MetaData_Program, masterEditionPda).PublicKey;

        //14 Associated Token Program
        byte[] msg14 = converTobyte("8c97258f4e2489f1bb3d1029148e0d830b5a1399daff1084048e7bd8dbe9f859");

        //15 cmV2
        byte[] msg15 = converTobyte("092aee3dfc2d0e55782313837969eaf52151c096c06b5c2a82f086a503e82c34");

        //16 Authority
        byte[] msg16 = converTobyte("ed5af20364c5381facde2ab2a9990dd349048b4568970b2637f8090f3c90329e");

        //17 Token_MetaData_Program
        byte[] msg17 = converTobyte("0b7065b1e3d17c45389d527f6b04c3cd58b86c731aa0fdb549b6d1bc03f82946");

        //18 Sysvar1nstructions1111111111111111111111111
        byte[] msg18 = converTobyte("06a7d517187bd16635dad40455fdc2c0c124c68f215675a5dbbacb5f08000000");

        //19 SysvarC1ock11111111111111111111111111111111
        byte[] msg19 = converTobyte("06a7d51718c774c928566398691d5eb68b5eb8a39b4b6d5c73555b2100000000");

        //20 SysvarRent111111111111111111111111111111111
        byte[] msg20 = converTobyte("06a7d517192c5c51218cc94c3d4af17f58daee089ba1fd44e3dbd98a00000000");

        //21 SysvarS1otHashes111111111111111111111111111
        byte[] msg21 = converTobyte("06a7d517192f0aafc6f265e3fb77cc7ada82c529d0be3b136e2d005520000000");

        //22 TokenkegQfeZyiNwAJbNbGKPFXCWuBvf9Ss623VQ5DA
        byte[] msg22 = converTobyte("06ddf6e1d765a193d9cbe146ceeb79ac1cb485ed5f5b37913a8cf5857eff00a9");

        //23 Candy Machine Creator
        byte[] msg23 = converTobyte("078746a0daa3db24de0d282500a2a58fde0b2b874a33950fbd881d16b189b535");

        //recent hash
        //byte[] msg24 = converTobyte("331FE3F2343FC69C07987F6E7DA8F8291E27EE2DAA8E4E6BAF93D5B808FD614C");
        

        //number of instructions
        byte[] msg25 = { 0x06 };

        //prep IX1
        byte[] msg26 = { 0x09, 0x02, 0x00, 0x01, 0x34 };

        //IX1 data header
        //0,0,0,0,96,77,22,0,0,0,0,0,82,0,0,0,0,0,0,0
        byte[] msg27 = { 0x00, 0x00, 0x00, 0x00, 0x60, 0x4D, 0x16, 0x00, 0x00, 0x00, 0x00, 0x00, 0x52, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        //IX1data owner TokenkegQfeZyiNwAJbNbGKPFXCWuBvf9Ss623VQ5DA
        byte[] msg28 = converTobyte("06ddf6e1d765a193d9cbe146ceeb79ac1cb485ed5f5b37913a8cf5857eff00a9");

        //prep IX2 [21,2,1,19,67]
        byte[] msg29 = { 0x15, 0x02, 0x01, 0x13, 0x43 };

        //prep IX2 data
        // 0,0,feepayer,1,feepayer
        byte[] msg30 = { 0x00, 0x00 };

        //prep IX2 data
        byte[] msg31 = msg1;

        byte[] msg32 = { 0x01 };

        byte[] msg33 = msg1;

        //prep IX3
        //[13,7,0,4,0,1,9,21,19,0]
        byte[] msg34 = { 0x0D, 0x07, 0x00, 0x04, 0x00, 0x01, 0x09, 0x15, 0x13, 0x00 };

        //prep IX4
        //[21,3,1,4,0,9]
        byte[] msg35 = { 0x15, 0x03, 0x01, 0x04, 0x00, 0x09 };

        //IX4 data [7,1,0,0,0,0,0,0,0]
        byte[] msg36 = { 0x07, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        //prep IX5
        //[14,16,7,22,0,6,8,1,0,0,3,16,21,9,19,18,20,17,9]
        byte[] msg37 = { 0x0E, 0x10, 0x07, 0x16, 0x00, 0x06, 0x08, 0x01, 0x00, 0x00, 0x03, 0x10, 0x15, 0x09, 0x13, 0x12, 0x14, 0x11, 0x09 };

        //prep IX5 data [211,57,6,167,15,219,35,251,254] timestamp data,PDA bump
        byte[] msg38 = { 0xD3, 0x39, 0x06, 0xA7 , 0x0F, 0xDB , 0x23, 0xFB, collectionPda.nonce };

        //prep IX6
        //[14,11,7,8,0,2,16,17,10,5,12,15,11,8]
        byte[] msg39 = { 0x0E, 0x0B, 0x07, 0x08, 0x00, 0x02, 0x10, 0x11, 0x0A, 0x05, 0x0C, 0x0F, 0x0B, 0x08 };

        //prep IX6 data [103,17,200,25,118,95,125,61]
        byte[] msg40 = { 0x67, 0x11, 0xC8, 0x19, 0x76, 0x5F, 0x7D, 0x3D };


        byte[] msg = msg0
            .Concat(msg1)
            .Concat(msg2)
            .Concat(msg3)
            .Concat(msg4)
            .Concat(msg5)
            .Concat(msg6)
            .Concat(msg7)
            .Concat(msg8)
            .Concat(msg9)
            .Concat(msg10)
            .Concat(msg11)
            .Concat(msg12)
            .Concat(msg13)
            .Concat(msg14)
            .Concat(msg15)
            .Concat(msg16)
            .Concat(msg17)
            .Concat(msg18)
            .Concat(msg19)
            .Concat(msg20)
            .Concat(msg21)
            .Concat(msg22)
            .Concat(msg23)
            .Concat(msg24)
            .Concat(msg25)
            .Concat(msg26)
            .Concat(msg27)
            .Concat(msg28)
            .Concat(msg29)
            .Concat(msg30)
            .Concat(msg31)
            .Concat(msg32)
            .Concat(msg33)
            .Concat(msg34)
            .Concat(msg35)
            .Concat(msg36)
            .Concat(msg37)
            .Concat(msg38)
            .Concat(msg39)
            .Concat(msg40)
            .ToArray();


        byte[] signature1 = sign_msg(feePayer_sK, msg);

  
        byte[] signature2 = sign_msg(mint_account_sK,msg);

      
            //Debug.Log("Signatures");
            //Debug.Log(returnArrayString(0, 64, signature1));
            //Debug.Log(returnArrayString(0, 64, signature2));
        

        byte[] transactionPreFix = { 0x02 };
        byte[] rawTransaction = transactionPreFix
         .Concat(signature1)
         .Concat(signature2)
         .Concat(msg)
         .ToArray();




        //byte[] rawTransaction = transactionPreFix
        // .Concat(signature1)
        // .Concat(signature2)
        // .Concat(msg)
        // .ToArray();

        //Debug.Log(rawTransaction.Length);

        //Debug.Log(signature1.Length);
        //Debug.Log(signature2.Length);
        //Debug.Log("Account 1 " + msg1.Length);
        //Debug.Log("Account 2 " + msg2.Length);
        //Debug.Log("Account 3 " + msg3.Length);
        //Debug.Log("Account 4 " + msg4.Length);
        //Debug.Log("Account 5 " + msg5.Length);
        //Debug.Log("Account 6 " + msg6.Length);
        //Debug.Log("Account 7 " + msg7.Length);
        //Debug.Log("Account 8 " + msg8.Length);
        //Debug.Log("Account 9 " + msg9.Length);
        //Debug.Log("Account 10 " + msg10.Length);
        //Debug.Log("Account 11 " + msg11.Length);
        //Debug.Log("Account 12 " + msg12.Length);
        //Debug.Log("Account 13 " + msg13.Length);
        //Debug.Log("Account 14 " + msg14.Length);
        //Debug.Log("Account 15 " + msg15.Length);
        //Debug.Log("Account 16 " + msg16.Length);
        //Debug.Log("Account 17 " + msg17.Length);
        //Debug.Log("Account 18 " + msg18.Length);
        //Debug.Log("Account 19 " + msg19.Length);
        //Debug.Log("Account 20 " + msg20.Length);
        //Debug.Log("Account 21 " + msg21.Length);
        //Debug.Log("Account 22 " + msg22.Length);
        //Debug.Log("Account 23 " + msg23.Length);
        //Debug.Log("hash " + msg24.Length);
        //Debug.Log(msg25.Length);
        //Debug.Log(msg26.Length);
        //Debug.Log(msg27.Length);
        //Debug.Log(msg28.Length);
        //Debug.Log(msg29.Length);
        //Debug.Log(msg30.Length);
        //Debug.Log(msg31.Length);
        //Debug.Log(msg32.Length);
        //Debug.Log(msg33.Length);
        //Debug.Log(msg34.Length);
        //Debug.Log(msg35.Length);
        //Debug.Log(msg36.Length);
        //Debug.Log(msg37.Length);
        //Debug.Log(msg38.Length);
        //Debug.Log(msg39.Length);
        //Debug.Log(msg40.Length);


        Debug.Log(rawTransaction[0]);

        //Debug.Log(returnArrayString(0, 1107, rawTransaction));
        Debug.Log(returnArrayString(0, 1, rawTransaction));
        Debug.Log(returnArrayString(1, 64, rawTransaction));
        Debug.Log(returnArrayString(65, 64, rawTransaction));
        Debug.Log(returnArrayString(65 + 64, 4, rawTransaction));


        Debug.Log(returnSolanaPubKey(65 + 64 + 4 + 32 * 0, 32, rawTransaction));
        Debug.Log(returnSolanaPubKey(65 + 64 + 4 + 32 * 1, 32, rawTransaction));
        Debug.Log(returnSolanaPubKey(65 + 64 + 4 + 32 * 2, 32, rawTransaction));
        Debug.Log(returnSolanaPubKey(65 + 64 + 4 + 32 * 3, 32, rawTransaction));
        Debug.Log(returnSolanaPubKey(65 + 64 + 4 + 32 * 4, 32, rawTransaction));
        Debug.Log(returnSolanaPubKey(65 + 64 + 4 + 32 * 5, 32, rawTransaction));
        Debug.Log(returnSolanaPubKey(65 + 64 + 4 + 32 * 6, 32, rawTransaction));


        if (rawTransaction.Length == 1106)
        {
            string payload = Convert.ToBase64String(rawTransaction);

            string url = "https://patient-fragrant-card.solana-devnet.quiknode.pro/3e373e17b6007e1d1eddf7f1d394df2d7a81b67e/";
            string method = "sendTransaction";
            string jsonrpc = "2.0";
            string param1 = payload;
            string param2 = "{\"encoding\":\"base64\",\"skipPreflight\":true,\"preflightCommitment\":\"confirmed\"}";
            string id;

            id = System.Guid.NewGuid().ToString();
            string postData = $"{{\"method\":\"{method}\",\"jsonrpc\":\"{jsonrpc}\",\"params\":[\"{param1}\",{param2}],\"id\":\"{id}\"}}";
            Debug.Log(postData);

            StartCoroutine(Upload(url, postData));

        }
        else
        {
            Debug.Log("raw transaction has wrong length size");
            Debug.Log(rawTransaction.Length);
        }

       




    }

    public static byte[] Base58ToByteArray(string base58)
    {
        const string alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
        const int baseSize = 58;

        byte[] bytes = new byte[base58.Length];

        // Convert the Base58 string to a decimal number
        BigInteger value = BigInteger.Zero;
        for (int i = 0; i < base58.Length; i++)
        {
            int digit = alphabet.IndexOf(base58[i]);
            if (digit < 0)
                throw new FormatException("Invalid Base58 String");

            value = value * baseSize + digit;
        }

        // Convert the decimal number to a byte array
        int index = 0;
        while (value > 0)
        {
            BigInteger remainder;
            value = BigInteger.DivRem(value, 256, out remainder);
            bytes[index++] = (byte)remainder;
        }

        // Trim leading zero bytes
        Array.Resize(ref bytes, index);

        // Reverse the byte array to restore the original byte order
        Array.Reverse(bytes);

        return bytes;
    }

    string returnSolanaPubKey(int startIndex,int length, byte[] x)
    {
        byte[] hold = new byte[length];
        for (int i = 0; i < 32; i++)
        {
            Array.Copy(x, startIndex, hold, 0, hold.Length);
        }
        return base_58_encoding(hold);
    }

    string returnArrayString(int startIndex,int length,byte[] rawTx)
    {

        int[] subArray = new int[length];
        Array.Copy(rawTx, startIndex, subArray, 0, subArray.Length);
        string subArrayString = string.Join(", ", subArray);
        //Debug.Log(subArrayString);

        //return base_58_encoding(inputBytes);
        return subArrayString;

    }

    byte[] returnArrayByte(int startIndex, int length, byte[] rawTx)
    {

        int[] subArray = new int[length];
        Array.Copy(rawTx, startIndex, subArray, 0, subArray.Length);
        string subArrayString = string.Join(", ", subArray);
        //Debug.Log(subArrayString);

        byte[] inputBytes = Encoding.ASCII.GetBytes(subArrayString);

        return inputBytes;

    }

    [System.Serializable]
    public class BlockhashResponse
    {
        public string jsonrpc;
        public string id;
        public string result;
    }

    [Serializable]
    public class MyJsonData
    {
        public string jsonrpc;
        public Result result;
        public string id;

        [Serializable]
        public class Result
        {
            public Context context;
            public Value value;

            [Serializable]
            public class Context
            {
                public string apiVersion;
                public int slot;
            }

            [Serializable]
            public class Value
            {
                public string blockhash;
                public FeeCalculator feeCalculator;

                [Serializable]
                public class FeeCalculator
                {
                    public int lamportsPerSignature;
                }
            }
        }
    }

    [Serializable]
    public class sendTransactionResponse
    {
        public string jsonrpc;
        public string result;
        public string id;
    }

    [System.Serializable]
    public class StatusResponse
    {
        public string jsonrpc;
        public string id;
        public StatusResult[] result;

        [System.Serializable]
        public class StatusResult
        {
            public string slot;
            public string confirmations;
            public string err;
        }
    }



    IEnumerator Upload(string url, string postData)
    {
        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(url, postData))
        {
            byte[] bodyRaw = new System.Text.UTF8Encoding(true).GetBytes(postData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();

            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log($"Response data: {www.downloadHandler.text}");

                if(postData.ToString().Contains("getRecentBlockhash"))
                {
                    MyJsonData myJsonData = JsonUtility.FromJson<MyJsonData>(www.downloadHandler.text);
                    // Access the parsed data
                    Debug.Log("jsonrpc: " + myJsonData.jsonrpc);
                    Debug.Log("id: " + myJsonData.id);
                    Debug.Log("apiVersion: " + myJsonData.result.context.apiVersion);
                    Debug.Log("slot: " + myJsonData.result.context.slot);
                    Debug.Log("blockhash: " + myJsonData.result.value.blockhash);
                    Debug.Log("lamportsPerSignature: " + myJsonData.result.value.feeCalculator.lamportsPerSignature);
                    msg24 = Base58ToByteArray(myJsonData.result.value.blockhash);

                    metaMint();

                }

                if (postData.ToString().Contains("sendTransaction"))
                {
                    sendTransactionResponse txResponse = JsonUtility.FromJson<sendTransactionResponse>(www.downloadHandler.text);

                    // Access the parsed data
                    Debug.Log("jsonrpc: " + txResponse.jsonrpc);
                    Debug.Log("id: " + txResponse.id);
                    Debug.Log("result: " + txResponse.result);

                    txSignature = txResponse.result;
                    string id = System.Guid.NewGuid().ToString();
                    string sigData = $"{{\"method\":\"getSignatureStatuses\",\"jsonrpc\":\"2.0\",\"params\":[[\"{txSignature}\"]],\"id\":\"{id}\"}}";
                    StartCoroutine(Upload(url, sigData));
                }


                if (postData.ToString().Contains("getSignatureStatuses"))
                {
                    StatusResponse response = JsonUtility.FromJson<StatusResponse>(www.downloadHandler.text);
                    string id = System.Guid.NewGuid().ToString();
                    string sigData = $"{{\"method\":\"getSignatureStatuses\",\"jsonrpc\":\"2.0\",\"params\":[[\"{txSignature}\"]],\"id\":\"{id}\"}}";
                    Debug.Log(txSignature);            
                    
                }





            }
        }
    }


    Pda findProgramAddressSync(byte[] seed, byte[] programID, Pda pda)
    {
        byte nonce = 0xFF;

        Debug.Log(returnArrayString(0, seed.Length, seed));

        Array.Resize(ref seed, seed.Length + 1);
        seed[seed.Length - 1] = nonce;

        string pdaString = "ProgramDerivedAddress";
        byte[] pdaSeed = Encoding.ASCII.GetBytes(pdaString);

        byte[] buffer = seed.Concat(programID).Concat(pdaSeed).ToArray();

        // Create an instance of the SHA256 algorithm class
        SHA256 sha256 = SHA256.Create();

        // Compute hash value of the input byte array
        byte[] publicKeyBytes = sha256.ComputeHash(buffer);


        Tuple<BigInteger, BigInteger, BigInteger, BigInteger> res = PointDecompress(publicKeyBytes);

        while (res != null)
        {
            nonce = (byte)(nonce - 0x01);
            seed[seed.Length - 1] = nonce;
            pdaSeed = Encoding.ASCII.GetBytes(pdaString);

            buffer = seed.Concat(programID).Concat(pdaSeed).ToArray();

            Debug.Log(returnArrayString(0, buffer.Length, buffer));

            publicKeyBytes = sha256.ComputeHash(buffer);

            Debug.Log(returnArrayString(0, 32, publicKeyBytes));

            res = PointDecompress(publicKeyBytes);

            if (nonce == 0)
            {
                break;
            }
        }

        if (nonce != 0)
        {
            Debug.Log("PDA Found");
            Debug.Log(nonce);
            Debug.Log(res);
            Debug.Log(returnSolanaPubKey(0, 32, publicKeyBytes));

            pda.PublicKey = publicKeyBytes;
            pda.nonce = nonce;

            return pda;
        }
        else
        {
            Debug.Log("PDA never found");

            return null;
        }





        
    }


    // Start is called before the first frame update
    void Start()
    {

        string mnemonic = "pill tomorrow foster begin walnut borrow virtual kick shift mutual shoe scatter";


        feePayer_sK = GetSkFromMnemonic(mnemonic);
        feePayer_pK = GetPkFromSk(feePayer_sK);
        

        //Debug.Log("GetPublicKey");
        //Debug.Log(returnSolanaPubKey(0, 32, feePayer_pK));




     
        //Pda masterEditionPda = new Pda();
        //Pda candyMachinePda = new Pda();
        //Pda freezePda = new Pda();
        //Pda collectionPda = new Pda();


        //Keypair keypair = new Keypair();
        //keypair = GenerateKeyPair();
        //Debug.Log(returnArrayString(0, 32, keypair.PublicKey));
        //Debug.Log(returnArrayString(0, 32, keypair.SecretKey));
        //Debug.Log(string.Join(" ", keypair.mnemonic));
        //Debug.Log(keypair.PublicKeyBase58);

        //byte[] feePayer_sK = converTobyte    ("1FD39E5146E3EC66120326216A9B6435593EBC20D9C62B9A3DF5C83511096D02");

        //byte[] feePayer_sK = mnemonic_to_sk(mnemonic);
        //byte[] mint_account_sK = converTobyte("EA1622C08C673538021A4CD547033162A0C52C2168FCABC3D1BE0914B0F6DB35");


        //Debug.Log("Fee Payer Sk");
        //Debug.Log(returnArrayString(0, 32, feePayer_sK));
        //Debug.Log("Fee Payer pubkey Array");
        //Debug.Log(returnArrayString(0, 32, getPubV2(feePayer_sK)));





        //string myString3 = "candy_machine";
        //byte[] t5 = Encoding.ASCII.GetBytes(myString3);
        //byte[] seed2;
        //seed2 = t5.Concat(converTobyte("f95cb71bb699fb7d31f9c7b7e95dd58719594455448ac64e822f87dbc8630800")).ToArray();
        //candyMachinePda = findProgramAddressSync(seed2, converTobyte("092aee3dfc2d0e55782313837969eaf52151c096c06b5c2a82f086a503e82c34"), candyMachinePda);

        //string myString4 = "freeze";
        //byte[] t6 = Encoding.ASCII.GetBytes(myString4);
        //byte[] seed3;
        //seed3 = t6.Concat(converTobyte("f95cb71bb699fb7d31f9c7b7e95dd58719594455448ac64e822f87dbc8630800")).ToArray();
        //freezePda = findProgramAddressSync(seed3, converTobyte("092aee3dfc2d0e55782313837969eaf52151c096c06b5c2a82f086a503e82c34"), freezePda);




        //Debug.Log(returnSolanaPubKey(0, 32, collectionPda.PublicKey));


        //Debug.Log("Testing MetaMint");
        //metaMint();

        string id = Guid.NewGuid().ToString();
        string postData = $"{{\"method\":\"getRecentBlockhash\",\"jsonrpc\":\"2.0\",\"params\":[{{\"commitment\":\"confirmed\"}}],\"id\":\"{id}\"}}";
        string url = "https://patient-fragrant-card.solana-devnet.quiknode.pro/3e373e17b6007e1d1eddf7f1d394df2d7a81b67e/";

        StartCoroutine(Upload(url, postData));

        //GetPublicKey("test");



        //// Example compressed point as a byte string
        //string publicKeyHex = "3d4017c3e843895a92b70aa74d1b7ebc9c982ccf2ec4968cc0cd55f12af4660c";

        //byte[] pK = Enumerable.Range(0, publicKeyHex.Length)
        //         .Where(x => x % 2 == 0)
        //         .Select(x => Convert.ToByte(publicKeyHex.Substring(x, 2), 16))
        //         .ToArray();

        //Debug.Log(PointDecompress(pK));


        //// Example compressed point as a byte string
        //publicKeyHex = "5bf554dd9dbe0991b082192b486bbee54b58bf8807a76d5baaa4ba0f8e240c17";

        //pK = Enumerable.Range(0, publicKeyHex.Length)
        //         .Where(x => x % 2 == 0)
        //         .Select(x => Convert.ToByte(publicKeyHex.Substring(x, 2), 16))
        //         .ToArray();

        //Debug.Log(PointDecompress(pK));

        //// Example compressed point as a byte string
        //publicKeyHex = "c8e572c0390bd4edb2fdff3fa6cd5b8758ed1176b0ced598b243b235a05a3b85";

        //pK = Enumerable.Range(0, publicKeyHex.Length)
        //         .Where(x => x % 2 == 0)
        //         .Select(x => Convert.ToByte(publicKeyHex.Substring(x, 2), 16))
        //         .ToArray();

        //Debug.Log(PointDecompress(pK));



    }

    // Update is called once per frame
    void Update()
    {

    }
}
