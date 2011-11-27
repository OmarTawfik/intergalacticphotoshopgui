namespace IntergalacticUI.Classes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Effects;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Applies normal map effect on the effect holder
    /// </summary>
    public class NormalMapEffect : ShaderEffect
    {
        /// <summary>
        /// DependencyProperty for the input image
        /// </summary>
        public static readonly DependencyProperty InputProperty = ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(NormalMapEffect), 0);
        
        /// <summary>
        /// DependencyProperty for the X light direct
        /// </summary>
        public static readonly DependencyProperty ValueYProperty = DependencyProperty.Register("ValueY", typeof(double), typeof(NormalMapEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(1)));
        
        /// <summary>
        /// DependencyProperty for the Y light direct
        /// </summary>
        public static readonly DependencyProperty ValueXProperty = DependencyProperty.Register("ValueX", typeof(double), typeof(NormalMapEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)));
        
        /// <summary>
        /// DependencyProperty for the Z light direct
        /// </summary>
        public static readonly DependencyProperty ValueZProperty = DependencyProperty.Register("ValueZ", typeof(double), typeof(NormalMapEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(2)));
        
        /// <summary>
        /// The main pixel shader loader
        /// </summary>
        private static PixelShader pixelShader;
        ////= new PixelShader() { UriSource = new Uri(@"pack://application:,,,/Shaders;component/po.ps") };
    
        /// <summary>
        /// Initializes a new instance of the NormalMapEffect class
        /// </summary>
        public NormalMapEffect()
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            Stream stream = myAssembly.GetManifestResourceStream("IntergalacticUI.normal.ps");

            pixelShader = new System.Windows.Media.Effects.PixelShader();
            pixelShader.SetStreamSource(stream);
            PixelShader = pixelShader;
            UpdateShaderValue(InputProperty);
            UpdateShaderValue(ValueXProperty);
            UpdateShaderValue(ValueYProperty);
            UpdateShaderValue(ValueZProperty);
        }

        /// <summary>
        /// Gets or sets the X light direct
        /// </summary>
        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        /// <summary>
        /// Gets or sets the X light direct
        /// </summary>
        public double ValueX
        {
            get { return (double)GetValue(ValueXProperty); }
            set { SetValue(ValueXProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets the X light direct
        /// </summary>
        public double ValueY
        {
            get { return (double)GetValue(ValueYProperty); }
            set { SetValue(ValueYProperty, value); }
        }
        
        /// <summary>
        /// Gets or sets the X light direct
        /// </summary>
        public double ValueZ
        {
            get { return (double)GetValue(ValueZProperty); }
            set { SetValue(ValueZProperty, value); }
        }
    }
}
