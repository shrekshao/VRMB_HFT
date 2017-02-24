HappyFunTimes Unity Plugin and Example Scenes
=============================================

HappyFunTimes lets you use your phone as a controller
to control a game. This lets you make unique controllers
or make locally multiplayer games that support tens or 
hundreds of people.

## Docs

Docs and Videos are available from the menus at Window->HappyFunTimes->Docs
or at http://docs.happyfuntimes.net/docs/unity

## Samples

There are samples in both `Assets/HappyFunTimes/Samples/Scenes` and `Assets/HappyFunTimes/MoreSamples`
Be sure to check them all out.

## Publishing

Please see this page on certain things you should be aware of if you plan to publish a
commercial game using this plugin.

## Plugin Structure

```
Assets
├── HappyFunTimes
│   ├── HappyFunTimesCore           required
│   ├── MoreSamples                 just samples - deletable
│   ├── Resources                   required
│   ├── Samples                     deletable
│   ├── Scripts                     HFTGamepad etc.. see below
│   ├── Shaders                     deletable
│   └── Tests                       deletable
├── Plugins                         required
└── WebPlayerTemplates
    └── HappyFunTimes
        ├── 3rdparty                see below
        ├── controllers
        │   ├── character-select    deletable
        │   ├── gamepad             see below
        │   ├── multi-machine       deletable
        │   └── simple              deletable
        ├── hft                     required
        └── sample-ui               see below
```

## Stuff marked as See Below

HappyFunTimes is just the part that connects phones to Unity, serves them webpages
and facilitates communication between them. **EVERYTHING ELSE IS A SAMPLE MEANT TO BE MODIFIED**

Included in those samples are the `HFTGamepad` sample. A premade controller that supports
a bunch of different modes. See docs at http://docs.happyfuntimes.net/docs/unity/gamepad.html

To use the HFTGamepad controller in your own projects keep everything about that says "see below".
If you are not using the HFTGamepad but are making your own custom controller then you may
delete `HappyFunTimes/Scripts` except for `HFTPlayerNameManager.cs`. You can also delete
all of `WebPlayerTemplates/HappyFunTimes/3rdparty, gamepad, controllers`

The samples also include a sample-ui. This code tries to manage the phone, trying to take it fullscreen
if possible. Trying to set the orientation if possible. Displaying a message to the user to rotate the
phone if not possible. It asks the user for a name. It manages the `gear` icon menu. It displays
"Switching Games" when the user is disconnected. Y

ou are free to use that or delete it and write your own. If you write your own
feel free to delete `WebPlayerTemplates/HappyFunTimes/sample-ui` and `HFTPlayerNameManager.cs`

## Upgrading from 1.x

If you are upgrading from 1.x, follow the directions in
`HappyFunTimes-1.x-to-2.x-Migration-README.txt`

## Debugging

If you are making custom controllers I hope I'd provided enough info to get started. For example
if you send a message from the phone to the game and there is no handler assigned for that message
you'll get an error in the console.

That said, if you'd like to see every message passed between the phones and the game you can go to
`Window->HappyFunTimes->Settings` and check `showMessages`. You'll see the raw JSON being passed
for every message.

There is also a `debug` setting. Putting `*` in there will show all kinds of info from various
parts of happyfuntimes. To filter that you can also put specific class names. For example putting
`HFTWebServer` will show only info from the webserer. Putting `HFTGame` will show only messages
from the game. You may put more than one name (eg, `HFTGame HFTWebServer`). You may also put
prefixes (eg. `HFTG*` which would match `HFTGame`, `HFTGameManager` etc...)

## License

HappyFunTimes is licensed under the MIT license.

It also uses the `websocket-sharp` library (MIT)
https://github.com/sta/websocket-sharp

and the `DNS` library (MIT)
https://github.com/kapetan/dns

also see some details about publishing your game here
http://docs.happyfuntimes.net/docs/unity/rendezvous-server.html

## Change List

*   2.0.5

    *   Handle more Android cases when in Installation Mode

*   2.0.4

    *   Update for change in Chrome when going fullscreen. Background styles
        can not be on body tag.

*   2.0

    *   Make it 100% self contained.

        No more need for node.js. Now you can easily build using the normal builder
        and post your games on itch.io or steam, etc...

    *   Move almost all the samples into the plugin.

        Before each sample was a separate repo. This was to make it easy to export
        and install each one into the HappyFunTimes app. Now that it's all
        standalone there's no need for that feature and all the samples can
        happily co-exist in the same project.

*   1.3

    *   Fixed `GetAxis` bug where asking for the wrong axis returned the first axis

    *   Added Instructions support.

        Start HappyFunTimes with `hft start --instrutions` for instructions to appear.
        [See docs](commands.html#-hft-start-)

*   1.2

    *   Added Export/Install/Publish menu items

    *   added HFTGamepad.BUTTON_TOUCH for whether or not the user is touching the screen
        on the orientation and touch controllers.

    *   added ability to play sound through controller

        There are currently 2 ways to make sounds for the controllers.

        1.  JSFX

            This is the recommended way as it's light weight.

            1.  Go to the [JSFX sound maker](http://egonelbre.com/project/jsfx/).

            2.  Adjust the values until you get a sound you like.

            3.  Copy the sound values (near top of the page just below the buttons) to a text file with the extension ".jsfx.txt"
                putting a name in front of each set of values. Save that file in Assets/WebPlayerTemplates/HappyFunTimes

            For example here is the sample `sounds.jsfx.txt` file

                coin      ["square",0.0000,0.4000,0.0000,0.0240,0.4080,0.3480,20.0000,909.0000,2400.0000,0.0000,0.0000,0.0000,0.0100,0.0003,0.0000,0.2540,0.1090,0.0000,0.0000,0.0000,0.0000,0.0000,1.0000,0.0000,0.0000,0.0000,0.0000]
                jump      ["square",0.0000,0.4000,0.0000,0.0960,0.0000,0.1720,20.0000,245.0000,2400.0000,0.3500,0.0000,0.0000,0.0100,0.0003,0.0000,0.0000,0.0000,0.5000,0.0000,0.0000,0.0000,0.0000,1.0000,0.0000,0.0000,0.0000,0.0000]
                coinland  ["square",0.0000,0.4000,0.0000,0.0520,0.3870,0.1160,20.0000,1050.0000,2400.0000,0.0000,0.0000,0.0000,0.0100,0.0003,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,1.0000,0.0000,0.0000,0.0000,0.0000]
                bonkhead  ["sine",0.0000,0.4000,0.0000,0.0000,0.5070,0.1400,20.0000,1029.0000,2400.0000,-0.7340,0.0000,0.0000,0.0100,0.0003,0.0000,0.0000,0.0000,0.3780,0.0960,0.0000,0.0000,0.0000,1.0000,0.0000,0.0000,0.0000,0.0000]
                land      ["sine",0.0000,0.4000,0.0000,0.1960,0.0000,0.1740,20.0000,1012.0000,2400.0000,-0.7340,0.0000,0.0000,0.0100,0.0003,0.0000,0.0000,0.0000,0.3780,0.0960,0.0000,0.0000,0.0000,1.0000,0.0000,0.0000,0.0000,0.0000]
                kicked    ["noise",0.0000,1.0000,0.0000,0.0400,0.0000,0.2320,20.0000,822.0000,2400.0000,-0.6960,0.0000,0.0000,0.0100,0.0003,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,1.0000,0.0000,0.0000,0.0270,0.0000]
                dropkick  ["sine",0.0000,1.0000,0.0000,0.0880,0.0000,0.3680,20.0000,653.0000,2400.0000,0.2360,0.0000,0.1390,47.1842,0.9623,-0.4280,0.0000,0.0000,0.4725,0.0000,0.0000,-0.0060,-0.0260,1.0000,0.0000,0.0000,0.0000,0.0000]
                bounce    ["noise",0.0000,1.0000,0.0000,0.0220,0.0000,0.1480,20.0000,309.0000,2400.0000,-0.3300,0.0000,0.0000,0.0100,0.0003,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,1.0000,0.0000,0.0000,0.0000,0.0000]

        2.  Use sounds files

            Put sound files (`.mp3` or `.wav`) in `Assets/WebPlayerTemplates/HappyFunTimes/sounds`
            They have to be in there because they must be served to the phone. Note that it's a
            good idea to keep them as small as possible because each time a player connects to
            the game his phone will have to download all the sounds. JSFX sounds comming soon.

        ## Playing Sounds

        To use the sounds, on some global gameobject (like LevelManager in all the samples)
        add an `HFTGlobalSoundHelper` script component.

        Then, on the prefab that gets spawned for your players, the same prefab you put
        an `HFTInput` or `HFTGamepad` script component add the `HFTSoundPlayer` script
        component.

        In your `Awake` or `Start` look it up

            private HFTSoundPlayer m_soundPlayer;

            void Awake()
            {
                 m_soundPlayer = GetComponent<HFTSoundPlayer>();
            }

        To play a sound call `m_soundPlayer.PlaySound` with the name of the sound (no extension).
        In other words if you have `sounds/explosion.mp3` then you can trigger that sound on
        the user's phone with

            m_soundPlayer.PlaySound("explosion");

        Or if you named a JSFX sound like

            bounce    ["noise",0.0000,1.0000,0.0000,0.0220,0.0000,0.1480,20.0000,309.0000,2400.0000,-0.3300,0.0000,0.0000,0.0100,0.0003,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,1.0000,0.0000,0.0000,0.0000,0.0000]

        Then

            m_soundPlayer.PlaySound("bounce");


    *   added mulit-touch support to HFTInput via the standard `GetTouch` function
        as well as `axes` and `buttons`

            public const int AXIS_TOUCH0_X = 13;
            public const int AXIS_TOUCH0_Y = 14;
            public const int AXIS_TOUCH1_X = 15;
            public const int AXIS_TOUCH1_Y = 16;
            public const int AXIS_TOUCH2_X = 17;
            public const int AXIS_TOUCH2_Y = 18;
            public const int AXIS_TOUCH3_X = 19;
            public const int AXIS_TOUCH3_Y = 20;
            public const int AXIS_TOUCH4_X = 21;
            public const int AXIS_TOUCH4_Y = 22;
            public const int AXIS_TOUCH5_X = 23;
            public const int AXIS_TOUCH5_Y = 24;
            public const int AXIS_TOUCH6_X = 25;
            public const int AXIS_TOUCH6_Y = 26;
            public const int AXIS_TOUCH7_X = 27;
            public const int AXIS_TOUCH7_Y = 28;
            public const int AXIS_TOUCH8_X = 29;
            public const int AXIS_TOUCH8_Y = 30;
            public const int AXIS_TOUCH9_X = 31;
            public const int AXIS_TOUCH9_Y = 32;

            public const int BUTTON_TOUCH0 = 18;
            public const int BUTTON_TOUCH1 = 19;
            public const int BUTTON_TOUCH2 = 20;
            public const int BUTTON_TOUCH3 = 21;
            public const int BUTTON_TOUCH4 = 22;
            public const int BUTTON_TOUCH5 = 23;
            public const int BUTTON_TOUCH6 = 24;
            public const int BUTTON_TOUCH7 = 25;
            public const int BUTTON_TOUCH8 = 26;
            public const int BUTTON_TOUCH9 = 27;

        Note: The Touch.rawPosition is currently in screen pixels of Unity
        not the controller. It's not clear what the best way to handle this
        is.

        The Unity `Input` API says those value are in pixels but they are
        assuming the game is running on the phone. In the case of HappyFunTimes
        though each phone is different so having it be in phone screen pixels
        would make no sense unless you also knew the resolution of each phone.
        I could provide that but that would make it more complicated for you.

        Personally I'd prefer normalized values (0.0 to 1.0). If you want those
        then take  `Touch.rawPosition` and divide `x` by `Screen.width` and `y` by `Screen.height`
        as in

            HFTInput.Touch touch = m_hftInput.GetTouch(0);
            float normalizedX = touch.x / Screen.width;
            float normalziedY = touch.y / Screen.height;

        Also note the AXIS versions of these are already normalized to
        a -1.0 to +1.0 range.

    *   Changed sample HFTGamepad code to not issue an error if it's not actually
        connected to a controller.

        Users often leave a prefab in their scene. That prefab would generate
        errors because it's not actually connected to a controller and was
        trying to access the controllers `NetPlayer`.

*   1.1

    *   Moved samples into samples folder

*   1.0

    *   made the plugin based on [hft-unity-gamepad](http://github.com/greggman/hft-unity-gamepad)
        that implements 12 controllers.

*   0.0.7

    *   Added `maxPlayers` to PlayerSpawner



