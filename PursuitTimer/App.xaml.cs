﻿namespace PursuitTimer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

#nullable enable
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}