GenericDictionary
--------------------
<ResourceDictionary Source="pack://application:,,,/Innouvous.Utils.ResourceDictionaries;component/GenericDictionary.xaml"/>

ResourceDictionary with basic component stylings:
-Centered Vertical Alignment

-TextBox
	-Margin = 5
	-Centered Vertical Alignment

-Label
	-Margin = 5
	-Centered Vertical Alignment

-Button 
	-Centered Vertical Alignment
	-Margin, Padding = 5

-----------------

Tips:

Modify but maintain existing Styles

<Style TargetType="Label" BasedOn="{StaticResource {x:Type Label}}">
                    