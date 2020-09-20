using NuclearWar.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace NuclearWar.Effects
{
    public class ThresholdEffect : ShaderEffect
    {
        private static PixelShader pixelShader = new PixelShader
        {
            UriSource = Utils.MakePackUri(typeof(ThresholdEffect), "Shaders/ThresholdEffect.fx.ps")
        };

        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(ThresholdEffect), 0);
        public Brush Input
        {
            get => (Brush)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }


        public static readonly DependencyProperty ThresholdProperty =
            DependencyProperty.Register("Threshold", typeof(double), typeof(ThresholdEffect),
                new UIPropertyMetadata(0.5, PixelShaderConstantCallback(0)));

        public double Threshold
        {
            get => (double)GetValue(ThresholdProperty);
            set => SetValue(ThresholdProperty, value);
        }


        public static readonly DependencyProperty BlankColorProperty =
            DependencyProperty.Register("BlankColor", typeof(Color), typeof(ThresholdEffect),
                new UIPropertyMetadata(Colors.Transparent, PixelShaderConstantCallback(1)));

        public Color BlankColor
        {
            get => (Color)GetValue(BlankColorProperty);
            set => SetValue(BlankColorProperty, value);
        }

        public ThresholdEffect()
        {
            PixelShader = pixelShader;
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(ThresholdProperty);
            UpdateShaderValue(BlankColorProperty);
        }
    }
}
