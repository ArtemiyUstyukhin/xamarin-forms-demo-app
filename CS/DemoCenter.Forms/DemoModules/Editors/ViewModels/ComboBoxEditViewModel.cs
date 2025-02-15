/*
               Copyright (c) 2015-2020 Developer Express Inc.
{*******************************************************************}
{                                                                   }
{       Developer Express Mobile UI for Xamarin.Forms               }
{                                                                   }
{                                                                   }
{       Copyright (c) 2015-2020 Developer Express Inc.              }
{       ALL RIGHTS RESERVED                                         }
{                                                                   }
{   The entire contents of this file is protected by U.S. and       }
{   International Copyright Laws. Unauthorized reproduction,        }
{   reverse-engineering, and distribution of all or any portion of  }
{   the code contained in this file is strictly prohibited and may  }
{   result in severe civil and criminal penalties and will be       }
{   prosecuted to the maximum extent possible under the law.        }
{                                                                   }
{   RESTRICTIONS                                                    }
{                                                                   }
{   THIS SOURCE CODE AND ALL RESULTING INTERMEDIATE FILES           }
{   ARE CONFIDENTIAL AND PROPRIETARY TRADE                          }
{   SECRETS OF DEVELOPER EXPRESS INC. THE REGISTERED DEVELOPER IS   }
{   LICENSED TO DISTRIBUTE THE PRODUCT AND ALL ACCOMPANYING         }
{   CONTROLS AS PART OF AN EXECUTABLE PROGRAM ONLY.                 }
{                                                                   }
{   THE SOURCE CODE CONTAINED WITHIN THIS FILE AND ALL RELATED      }
{   FILES OR ANY PORTION OF ITS CONTENTS SHALL AT NO TIME BE        }
{   COPIED, TRANSFERRED, SOLD, DISTRIBUTED, OR OTHERWISE MADE       }
{   AVAILABLE TO OTHER INDIVIDUALS WITHOUT EXPRESS WRITTEN CONSENT  }
{   AND PERMISSION FROM DEVELOPER EXPRESS INC.                      }
{                                                                   }
{   CONSULT THE END USER LICENSE AGREEMENT FOR INFORMATION ON       }
{   ADDITIONAL RESTRICTIONS.                                        }
{                                                                   }
{*******************************************************************}
*/
using System.Collections.Generic;
using System.Windows.Input;
using DemoCenter.Forms.ViewModels;
using DevExpress.XamarinForms.Editors;
using Xamarin.Forms;

namespace DemoCenter.Forms.DemoModules.Editors.ViewModels {
    public class ComboBoxEditViewModel : NotificationObject {
        const double DefaultCornerRadius = 4.0;
        const string DefaultHelpText = "Select a state";
        const string DefaultErrorText = "This field is required";

        string selectedState;
        BoxMode selectedBoxMode;
        FilterMode selectedFilterMode;
        CornerMode selectedCornerMode;
        ColorViewModel selectedAccentColor;

        double topLeftCornerRadius;
        double topRightCornerRadius;
        double bottomLeftCornerRadius;
        double bottomRightCornerRadius;
        CornerRadius cornerRadius;

        string actualHelpText;
        string actualErrorText;
        bool actualHasError;

        bool showStartIcon;
        bool showHelpText;
        bool enableFilter;

        public string SelectedState { get => selectedState; set => SetProperty(ref selectedState, value); }
        public BoxMode SelectedBoxMode {
            get => selectedBoxMode;
            set => SetProperty(ref selectedBoxMode, value, () => {
                OnPropertyChanged(nameof(CanSetBottomCorners));
            });
        }
        public FilterMode SelectedFilterMode { get => selectedFilterMode; set => SetProperty(ref selectedFilterMode, value); }
        public CornerMode SelectedCornerMode { get => selectedCornerMode; set => SetProperty(ref selectedCornerMode, value); }
        public ColorViewModel SelectedAccentColor { get => selectedAccentColor; set => SetProperty(ref selectedAccentColor, value); }

        public string ActualHelpText { get => actualHelpText; private set => SetProperty(ref actualHelpText, value); }
        public string ActualErrorText { get => actualErrorText; private set => SetProperty(ref actualErrorText, value); }
        public bool ActualHasError { get => actualHasError; private set => SetProperty(ref actualHasError, value); }

        public double TopLeftCornerRadius { get => topLeftCornerRadius; set => SetProperty(ref topLeftCornerRadius, value, UpdateCornerRadius); }
        public double TopRightCornerRadius { get => topRightCornerRadius; set => SetProperty(ref topRightCornerRadius, value, UpdateCornerRadius); }
        public double BottomLeftCornerRadius { get => bottomLeftCornerRadius; set => SetProperty(ref bottomLeftCornerRadius, value, UpdateCornerRadius); }
        public double BottomRightCornerRadius { get => bottomRightCornerRadius; set => SetProperty(ref bottomRightCornerRadius, value, UpdateCornerRadius); }
        public CornerRadius CornerRadius { get => cornerRadius; set => SetProperty(ref cornerRadius, value); }
        public bool CanSetBottomCorners { get => SelectedBoxMode == BoxMode.Outlined; }

        public bool ShowStartIcon { get => showStartIcon; set => SetProperty(ref showStartIcon, value); }
        public bool ShowHelpText {
            get => showHelpText;
            set => SetProperty(ref showHelpText, value, () => {
                ActualHelpText = showHelpText ? DefaultHelpText : null;
            });
        }
        public bool EnableFilter {
            get => enableFilter;
            set => SetProperty(ref enableFilter, value);
        }

        public List<string> States { get; }

        public IList<ColorViewModel> Colors { get; }
        public IList<BoxMode> BoxModes { get; }
        public IList<FilterMode> FilterModes { get; }
        public IList<CornerMode> CornerModes { get; }

        public ICommand ResetToDefaultCommand { get; }
        public ICommand ToggleErrorCommand { get; }

        public ComboBoxEditViewModel() {
            States = new List<string>() {
                "Alabama",
                "Alaska",
                "Arizona",
                "Arkansas",
                "California",
                "Colorado",
                "Connecticut",
                "Delaware",
                "Florida",
                "Georgia",

                "Hawaii",
                "Idaho",
                "Illinois",
                "Indiana",
                "Iowa",
                "Kansas",
                "Kentucky",
                "Louisiana",
                "Maine",
                "Maryland",

                "Massachusetts",
                "Michigan",
                "Minnesota",
                "Mississippi",
                "Missouri",
                "Montana",
                "Nebraska",
                "Nevada",
                "New Hampshire",
                "New Jersey",

                "New Mexico",
                "New York",
                "North Carolina",
                "North Dakota",
                "Ohio",
                "Oklahoma",
                "Oregon",
                "Pennsylvania",
                "Rhode Island",
                "South Carolina",

                "South Dakota",
                "Tennessee",
                "Texas",
                "Utah",
                "Vermont",
                "Virginia",
                "Washington",
                "West Virginia",
                "Wisconsin",
                "Wyoming"
            };

            SelectedState = States[4];

            Colors = ColorViewModel.CreateDefaultColors();

            BoxModes = new List<BoxMode> {
                BoxMode.Outlined,
                BoxMode.Filled
            };
            CornerModes = new List<CornerMode> {
                CornerMode.Round,
                CornerMode.Cut
            };

            FilterModes = new List<FilterMode> {
                FilterMode.StartsWith,
                FilterMode.EndsWith,
                FilterMode.Contains
            };

            ResetToDefaultCommand = new DelegateCommand(ResetToDefault);
            ToggleErrorCommand = new DelegateCommand(ToggleError);

            ResetToDefault();
        }

        public void ResetToDefault() {
            SelectedBoxMode = BoxModes[0];
            SelectedCornerMode = CornerModes[0];
            SelectedFilterMode = FilterModes[0];
            SelectedAccentColor = Colors[5];
            TopLeftCornerRadius = DefaultCornerRadius;
            TopRightCornerRadius = DefaultCornerRadius;
            BottomLeftCornerRadius = DefaultCornerRadius;
            BottomRightCornerRadius = DefaultCornerRadius;

            ShowStartIcon = false;
            ShowHelpText = true;
            EnableFilter = false;

            SetError(false);
        }

        public void ToggleError() => SetError(!ActualHasError);

        public void SetError(bool hasError) {
            ActualErrorText = hasError ? DefaultErrorText : null;
            ActualHasError = hasError;
        }

        void UpdateCornerRadius() {
            CornerRadius = new CornerRadius(TopLeftCornerRadius, TopRightCornerRadius, BottomLeftCornerRadius, BottomRightCornerRadius);
        }
    }
}
