# Simplified Dialogue System Documentation

## Overview
The dialogue system supports a detailed multi-stage conversation flow with simple **Success** or **Failure** outcomes. The enhanced interaction flow is:

1. **Approach debtor** → Press E or F key to start dialogue
2. **Intro dialogue** → Character's opening statement → Left-click to continue
3. **First choice options** → Choose Intimidate, Persuade, or Neutral
4. **Player's dialogue** → Shows what you said → Left-click to continue
5. **Character response** → Debtor reacts to your choice → Left-click to continue
6. **Second choice options** → Choose again from the 3 options
7. **Player's dialogue** → Shows what you said → Left-click to continue
8. **Final outcome** → Success/failure dialogue → Left-click to complete mission

## Interaction Flow

### Enhanced Step-by-Step Process:
1. **Approach**: Walk near a debtor character
2. **Initiate**: Press **E** or **F** key when in range
3. **Listen**: Debtor says their intro dialogue
4. **Continue**: **Left-click** to proceed to choices
5. **Choose**: Click one of three dialogue options
6. **Read**: See exactly what you said to the debtor
7. **Continue**: **Left-click** to see debtor's reaction
8. **Listen**: Debtor responds to your choice
9. **Continue**: **Left-click** to proceed to second round
10. **Choose**: Click one of three dialogue options again
11. **Read**: See exactly what you said in response
12. **Continue**: **Left-click** to see final result
13. **Result**: See success or failure message
14. **Complete**: **Left-click** to finish mission

### Control Scheme:
- **E/F Keys**: Start dialogue interaction
- **Left Mouse Click**: Progress through conversation stages
- **Button Clicks**: Select dialogue options

## Features

### Simple Binary Outcomes
- **Success**: Complete debt payment collected
- **Failure**: No payment, mission fails

### Multi-Stage Dialogue Flow
- Players make two choices per conversation
- Character responses change based on first choice
- Some first choices can immediately trigger failure
- Left-click progression keeps players engaged

### Character-Specific Logic
Each personality type has unique reaction patterns:

#### Andri (Arrogant)
- Intimidation immediately fails
- Success: Persuade → Neutral OR Neutral → Neutral
- Responds well to respectful but firm approach

#### Bu Wati/Bu Siti (Gentle)
- Intimidation immediately fails
- Success: Persuade → Neutral OR Neutral → Persuade
- Needs empathy and understanding

#### Pak Riko (Cunning)
- Business-minded, responds to logical arguments
- Success: Persuade → Neutral OR Neutral → Neutral
- Appreciates straightforward professional approach

#### Yusuf (Aggressive)
- Intimidation immediately fails
- Success: Persuade → Persuade OR Persuade → Neutral
- Needs psychological approach and patience

#### Bu Rini (Humble)
- Very responsive to kind treatment
- Success: Multiple combinations work (Persuade → Neutral, Neutral → Neutral, Neutral → Persuade)
- Most forgiving personality

#### Rizwan (Stubborn)
- Hardest to succeed with
- Success: Only Neutral → Persuade works
- Requires very specific approach

## UI Requirements
The DialogueManager expects these UI elements:
- `dialoguePanel`: Main dialogue window
- `debtorNameText`: Character name display
- `dialogueText`: Main dialogue text
- `debtAmountText`: Debt amount display
- `intimidateButton`: Intimidation choice button
- `persuadeButton`: Persuasion choice button
- `neutralButton`: Neutral choice button
- `clickToContinuePrompt`: UI element showing "Click to continue" (optional)

## Technical Implementation

### Key Scripts
- **DebtorDataHelper.cs**: Contains all pre-configured dialogue content
- **DebtorCreationMenu.cs**: Editor menu for quick character creation
- **DialogueManager.cs**: Handles multi-stage conversation flow

### Dialogue Stages
```csharp
public enum DialogueStage
{
    ShowingIntro,           // Showing debtor's intro dialogue
    FirstDialogue,          // Showing first choice options
    ShowingPlayerChoice1,   // Showing what player said (first choice)
    ShowingResponse,        // Showing debtor's response to first choice
    SecondDialogue,         // Showing second choice options
    ShowingPlayerChoice2,   // Showing what player said (second choice)
    ShowingOutcome,         // Showing final success/failure dialogue
    Completed
}
```

### Success Patterns by Character
```
Andri (Arrogant):     Persuade → Neutral OR Neutral → Neutral
Bu Wati (Gentle):     Persuade → Neutral OR Neutral → Persuade  
Pak Riko (Cunning):   Persuade → Neutral OR Neutral → Neutral
Yusuf (Aggressive):   Persuade → Persuade OR Persuade → Neutral
Bu Rini (Humble):     Persuade → Neutral OR Neutral → Neutral OR Neutral → Persuade
Rizwan (Stubborn):    Neutral → Persuade (ONLY)
```

### Integration Points
- **Money System**: Full payment on success, nothing on failure
- **Mission System**: Clear success/failure tracking
- **UI System**: Simple progress indicators
- **Input System**: E/F keys to start, left-click to continue

The system provides an engaging conversation experience where players must pay attention to character responses and choose their approach carefully. The left-click progression keeps players actively engaged throughout the conversation.
