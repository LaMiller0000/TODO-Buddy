# TODO: Buddy
    A todolist, that is more than a todolist!
    
    A todo app made to motivate students with your studies!

    Our app brings you:
        - a fun little friend
        - real life consequences
        - cute rewards!

    Features:
        - Todo list
            - Task dependancies
            - Prioritzation
            - Task Web
        - Buddy
            - Customizable friend
                - Health based off of your productivity and time management skills
                - Experience based off of task completions streaks
            - Unique Sprites that you will love!

## Setup & Technology
    IDE: ANDROID STUDIO, VSCODE, GODOT, JETBRAINS RYDER
    VERSION CONTROL: GITHUB
    COMMUNICATION: DISCORD
    PLANNING: DRAW.IO
    LANGUAGES: Java, GDScript

##Assets
    Rogue Adventure World – ElvGames
    
### Goals
    As a user I want my list to adapt to my needs so I can use it in all scenerios.
    As a user I want to create tasks easily so I save time.
    As a user I want to easily manage my day so I don't have to refactor my entire list.
    As a user I want to categorize my tasks so I can easily decide what I need to do.
    
## Timeline
    
Report 1, Sprint 1 | Dates 01.18.26 - 01.31.26 | Objective: Complete basic design, IDE setup, basic documentation, start development

Report 2, Sprint 2 | Dates 02.01.26 - 02.14.26 | Objective: Build basic layouts, paging

Report 3, Sprint 3 | Dates 02.15.26 - 02.28.26 | Objective: Implement buddies, and dependencies on tasks

Report 4, Sprint 4 | Dates 03.01.26 - 03.14.26 | Objective: Complete design, implement error handling, create unified design

Technical Report | Dates 04.05.26 - 04.11.26 | Objective: As a group create a 15 page technical report on the documentation for the development of your project in the IEEE Standard.

Presentation | Dates 04.12.26 - 04.18.26 | Objective: Present your project at the school wide, CETA showcase

Final Technical Report | 04.25.26 | Objective: As a group, finalize your 15 page technical report on the documentation for the development of your project in the IEEE Standard.

## Group Contributions
    Report 1:
        Finalize basic app concepts.
        Start up development environment.
        Configure godot
        Create test page       
    Report 2:
        Task component for the UI was programmatically added
        Buddy class was created:
            - Attributes: ID, NAME
            - Behaviors: SLEEP, HURT, PLAY, DEAD, IDLE, HAPPY
            - Dialog
        Research sprint on Godot
    Report 3:
        Added addtional screens such as main, test, display list tab and a task creation panel
        Added tab container to hold each tab in view 
        Added a viewport resolution script
        Added 5 complete animations for buddies, with 3 default and 3 evolved animations
        Added features to display list tab
            has buttons and display list scene
            sorting options dropdown
            create new tab button
            task creation panel
            swipe interactable in certain areas
        Organized the project, and project folders in related areas.
        Added funtions to set sixe dictation, speed and font size for dialog
    Report 4:
        Finished the UI of the Task View Screen, Task Creation Tab, and most of the Buddy Screen
        Developed Buddy Interaction Class
            Connected Scripts for future buddy interactions and development
        Finished Presentation for the CETA Showcase
        Finished Paper for the CETA Showcase

## Individual Contributions
### Penny - Developer
    Report 1:
        - Theorized "psudeocode gold" as in game currency
        - Pulled project and got it working locally
    Report 2:
        - Individual learning on basics of Godot
    Report 4:
        - Created Task Creation Tab
        - Added the ability to input task values
        - Added user task modification
            - Save
            - Delete
            - Cancel

### Lucius - Developer
    Report 1:
        - Created Github, invited all to collaborate.
    Report 2:
        - Created Buddy Class
        - Created Attributes for buddy class
            - Id, Name
        - Created Enum for buddy class behaviors
            - Sleep, Dead, Idle, Play, Hurt, Happy
        - Added Dialog Script
    Report 3:
        -	Removed magic numbers from the buddy code
            - Size dictation, Speed, Font size for dialog
        -   Added functions
            - Getters and setters for the former magic number functions
        -   Renamed functions
            - TestDialogRoot  DialogRoot
            - TestDialog  DialogPlayer
            
### Gary - Developer, UI Lead
    Report 1:
        - Created outline of application
        - Drafted health logi (i.e. task completion being tied to buddy's health)
        - Came up with starting layouts for:
            - Views (List, Task)
            - Create Task
        - Imported Godot APK into application by:
            - Installing Android Studio & Java JDK 25.0.2
            - Created keystore in Program Files
            - In Godot 4.6 set the debug keystore to the one generated in previous step
            - Set the Java SDK path to the JDK path
            - Then Export the APK to the Android Studio Project
        - Exported godot APK to android studio project
        - Created a test page
    Report 2:
        - Created draw.io diagram for:
            - task class
            - modeled entire application
        - Created list element
        - Created task display list
        - Modification to the buddy system for task system interaction.
    Report 3:
        - Added main and test screen
        - Added display list tab and task creation panel
        - Added features to the main screen
            - tab container that will hold each tab in each view
            - created script for the main screen for viewport resolution
        - Added change text button to test screen
        - features to display lsit tab
            - has buttons and display list scene
            - sorting options dropdown
            - create new tab button
            - task creation panel
        - features to task creation panel
            - swipe interactable within certain areas
            - designed for object oriented implementation in mind to edit task data
                - add button
                - text boxes
        - moved task related file into the task folder instead of just in the project root directory
    Report 4:
        - Improved due and start date selectors and added short/long press detection to task element
        - Created structure for task list system
        - Fixed issues with the Display list editor
        - Added JSON file saving system

### Emma - Developer, Documentation Lead
    Report 1:
        - Explored Godot with Gary
        - Set up basic project in Android Studio
        - Defined (with team) and created documentation on:
            - Scope
            - Timeline
            - Roles
        - Developed extensive README
    Report 2:
        - Continued to develop README documentation on github
        - Research spike on Godot Engine:
            - Learned about IDE structure and OOP structure with GDScript
        - Found sprites for Buddy work
    Report 3:
        - Developed documentation and weekly reports
        - Updated the README on github
        - Sprite work with the existing sprites that I found last week
        - Animating sprites
            - Blue slime
            - Blue hero
            - Gold slime
            - Orange slime
            - Red demon
            - Large red demon
        - Trouble shooting missing scene in Godot
        - Re animated the sprites from the previous list, with extended animations such as:
            - default and evolved
            - idle, sleep and dead for both default and evolved versions
    Report 4:
        - Continued to develop documentation and weekly report
        - Wrote final technical report
        - Build Buddy Interactions Class
        - Built foundational scripts for Buddy Interactions Class
        - Added Drop down menu with buddy options to the UI of the Buddy Screen
