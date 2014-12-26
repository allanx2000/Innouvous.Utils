Innouvous.Utils
===============

Various utility classes/assemblies

***Important: Unless noted, NONE of these are guaranteed to be stable and functions change greatly.***

# Innouvous.Utils.Dialogs #
Helper/Wrapper class for Win32 dialog windows. 

More intuitive Dialogs Factory and some utility functions such as for creating Extension strings.

# Innouvous.Utils.DialogWindow #
Probably should integrate this into a single generic Utils library.

The project only contains 1 class: NonClosableWindow which basically a window that has the X button disabled.

To close the window, CanClose() must be called first.

# Innouvous.Utils.DialogWindow.Windows #
A WPF UserControl and framework.

The control has 2 modes:

## Text Display ##
Shows some text (not that much different from a textbox)
    
    DialogControlOptions.SetTextBoxMessageOptions

## Data Input ##
Easily create user input windows such as for options or forms.

*This class library is subject to major changes as still tweaking the framework. There's a bit of redundancy in setting the values*

### Current Components ###
- TextBox with data type validation
- CheckBox
- DateTime text input
- ComboBox 
- Path/File Selector

- Create own components by extending ValueComponent abstract class (not IValueComponent, probably should be removed)

### Usage ###

    DialogControlOptions.SetDataInputOptions(fields);
  
Where fields is a `List<ValueComponent>`:

    var fields = new List<ValueComponent>() {
	    new TextBoxComponent(
		    new ComponentArgs()
		    {
			    DisplayName="Fund Name",
				FieldName=FldFundName,
			    InitialData= IsEdit? lastFund.FundName : null
			}),
    	new TextBoxComponent(
		    new ComponentArgs()
		    {
			    DisplayName="Fund Page (URL)",
			    FieldName=FldURL,
			    InitialData= IsEdit? lastFund.URL : null
		    }),
		...

# Innouvous.Utils.MessageBoxFactory #
Factory class for creating common MessageBoxes

**MessageBox Types**
- Show Info
- Show Error Message
- Confirm Action

# Innouvous.Utils.MVVM #
Helper class for learning and understanding MVVM from scratch. Currently, all WPF projects use this but eventually would probably move them to an existing framework like PRISM

# Innouvous.Utils.NestedDictionary #
***Untested: Haven't found a use for it yet***

The idea was to replace multi-level Dictionaries (i.e Dictionary<string,Dictionary<string,Dictionary<string, object>>>) with a friendly single object that also abstracts the data retrieval/storage and branch creation logic.

# Innouvous.Utils.ResourceDictionaries #
Contains one ResourceDictionary (at this point) that has sensible configurations for many common elements such as TextBox, Button, RadioButton, etc.

The goal is to not have to keep creating similar Setters for each individual project.

The Dictionary also contains the ToggleButton (basically a RadioButton that looks like a Button; the original implementation though is not mine.)

# Innouvous.Utils.SQLiteWrapper #
Wraps the SQLite Library. Probably not that useful but does provide a more familiar ADO-like class to query with. 

# Innouvous.Utils.TimeoutWebClient #
WebClient that allows setting a timeout value.