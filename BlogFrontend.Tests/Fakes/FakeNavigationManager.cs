// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend.Tests     File: FakeNavigationManager.cs    Created at 2020/09/18/2:54 AM
// All rights reserved, for personal using only
// 

using Microsoft.AspNetCore.Components;

namespace BlogFrontend.Tests.Fakes
{
    public class FakeNavigationManager : NavigationManager
    {
        /// <inheritdoc />
        protected override void NavigateToCore(string uri, bool forceLoad)
        {
            
        }
    }
}