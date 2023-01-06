# Create new Settings Category
---

- Create new **Page XAML** in **Views/Pages**
    - *Name it correctly* e.g. **Settings_CategoryName.xaml**
- Create a **ViewModel** for the page
    - **SettingsCategoryNameViewModel.xaml**
- In **XAML**
    - Add correct *Namespace*, e.g. ``xmlns:vm="clr-namespace:Battleship2000.ViewModels"``
    - Add DataContext in **XAML**, e.g. 
    ```
    <Page.DataContext>
        <vm:SettingsAudioViewModel/>
    </Page.DataContext>
    ```
    - Add XAML Structure/Style for a settings page, example of single textbox setting
    ```
    <StackPanel Margin="10,10,10,10">
        <StackPanel Orientation="Vertical">
            <Grid Margin="0,0,0,10">
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="140"/>
                    <ColumnDefinition Width="*"/>
                </Grid.ColumnDefinitions>
                <TextBlock Grid.Column="0" Text="Something:" VerticalAlignment="Center" HorizontalAlignment="Left" TextAlignment="Center" Style="{StaticResource TextHeadline}" FontSize="18"/>
                <TextBox x:Name="TXT_Something" Grid.Column="1" HorizontalAlignment="Left" MinWidth="180" Style="{StaticResource TextBoxStyle}" />
            </Grid>
        </StackPanel>
    </StackPanel>
    ```
- In **Models.Configuration**
    - Create a new **SubClass** for the category and set the default value, unless *null*
    ```
    internal class Something
    {
        [JsonProperty("myoption")]
        public float MyOption { get; set; } = 1.0f;
    }
    ```
    - Create a new property with the freshly created *Class*
    ```
    [JsonProperty("something")]
    public Something Something { get; set; } = new();
    ```
- In **ViewModel**
    - Link the Configuration property to the *ViewModel*
    ```
    public float MyOption
    {
        get
        {
            return ObjectStorage.Config.Something.MyOption;
        }
        set
        {
            ObjectStorage.Config.Something.MyOption = value;
            base.OnPropertyChanged(nameof(MyOption));
        }
    }
    ```
    *Note:* The binding should be *TwoWay*, so it gets automatically applied and can be saved by the ``Save()`` Method in **Logic.Configuration**
- In **CodeBehind** of **Page XAML**
    - Add the static Properties of ``Instance`` and ``ViewModel``, this way it's possible to access the instances with ease
    ```
    public static Settings_Something Instance { get; private set; }
    public static SettingsSomethingViewModel Vm { get; private set; }
    ```
    In *Construtor* add the binding
    ```
    Instance = this;
    Vm = (SettingsSomethingViewModel)this.DataContext;
    ```

**You're almost there!**

- Now for the SettingsMenu, open ``Settings.xaml``
- Add a new *Button*
    ```
    <StackPanel Orientation="Horizontal" Margin="0,0,0,10">
        <Button Style="{StaticResource ButtonMenu}" Width="120" Content="Something" Margin="0" HorizontalAlignment="Left" Command="{Binding SomethingCommand}" />
        <elem:SelectionArrow Visibility="{Binding SomethingArrowVisibility}" />
    </StackPanel>
    ```
    - Binding errors appear, since we didn't created a ``Command`` and ``Arrow Visibility`` for our new category
    - Open the file ``SettingsViewModel.cs`` and first of all add a new *enum* entry to ``MenuCategories``, in our case ``Something``
    - Now add two new entries for our new category
    ```
    public ICommand SomethingCommand { get; } = new RelayCommand((c) =>
    {
        HelperFunctions.NavigateSettingsframeTo("settings_something");
        Views.Pages.Settings.Vm.SetArrow(MenuCategories.Something);
    });
    ```
    ```
    private Visibility _SomethingArrowVisibility = Visibility.Hidden;
    public Visibility SomethingArrowVisibility
    {
        get
        {
            return _SomethingArrowVisibility;
        }
        set
        {
            _SomethingArrowVisibility = value;
            base.OnPropertyChanged(nameof(SomethingArrowVisibility));
        }
    }
    ```
    - Finally add a new *case* to the selector *switch*
    ``this.SomethingArrowVisibility = Visibility.Hidden;``
    and
    ```
    case MenuCategories.Something:
        this.SomethingArrowVisibility = Visibility.Visible;
        break;
    ```

**Last Step, I promise!**

- Finally, we need to add our **Page** to the **Preload**-engine, since pages are all loaded infront to speed up things
  and only preloaded pages will be displayed this way.
- Open the ``Preload.cs`` in ``Logic``
- Locate the *Method* ``LoadPages()``
- And add a new entry:
  ```
  ObjectStorage.pages.Add(new Settings_Something());
  ```