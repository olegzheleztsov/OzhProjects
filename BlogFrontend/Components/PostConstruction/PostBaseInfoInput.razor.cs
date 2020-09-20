// Create By: Oleg Gelezcov                        (olegg )
// Project: BlogFrontend     File: PostBaseInfoInput.razor.cs    Created at 2020/09/13/10:50 PM
// All rights reserved, for personal using only
// 

using System;
using ElevatorClient.Models.PostConstruction;
using MatBlazor;
using Microsoft.AspNetCore.Components;

namespace ElevatorClient.Components.PostConstruction
{
    public partial class PostBaseInfoInput : ComponentBase
    {
        private MatTextField<string> _authorField;
        private MatTextField<string> _subTitleField;

        private MatTextField<string> _titleField;

        public PostBaseInfo PostBaseInfo { get; } = new PostBaseInfo();


        private void OnKeyPress(FieldName fieldName)
        {
            switch (fieldName)
            {
                case FieldName.Title:
                    PostBaseInfo.Title = _titleField.Value;
                    break;
                case FieldName.SubTitle:
                    PostBaseInfo.SubTitle = _subTitleField.Value;
                    break;
                case FieldName.Author:
                    PostBaseInfo.Author = _subTitleField.Value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fieldName), fieldName, null);
            }
        }

        private enum FieldName
        {
            Title,
            SubTitle,
            Author
        }
    }
}