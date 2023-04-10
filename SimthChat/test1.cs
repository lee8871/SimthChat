using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimthChat {
	/// <summary>
	/// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
	///
	/// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
	/// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
	/// 元素中:
	///
	///     xmlns:MyNamespace="clr-namespace:SimthChat"
	///
	///
	/// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
	/// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
	/// 元素中:
	///
	///     xmlns:MyNamespace="clr-namespace:SimthChat;assembly=SimthChat"
	///
	/// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
	/// 并重新生成以避免编译错误:
	///
	///     在解决方案资源管理器中右击目标项目，然后依次单击
	///     “添加引用”->“项目”->[浏览查找并选择此项目]
	///
	///
	/// 步骤 2)
	/// 继续操作并在 XAML 文件中使用控件。
	///
	///     <MyNamespace:test1/>
	///
	/// </summary>
	public class test1 : Control {
		static test1() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(test1), new FrameworkPropertyMetadata(typeof(test1)));
		}
		TransformGroup ts;
		ScaleTransform control_scale;

		Pen R_pen = new Pen(new SolidColorBrush(Colors.Red), 0.002);
		Pen X_pen = new Pen(new SolidColorBrush(Colors.Red), 0.001);
		Pen G_pen = new Pen(new SolidColorBrush(Colors.Blue), 0.002);
		Pen B_pen = new Pen(new SolidColorBrush(Colors.Blue), 0.001);
		Pen base_pen = new Pen(new SolidColorBrush(Colors.Black), 0.002);
		public test1() {
			control_scale = new ScaleTransform();

			ts = new TransformGroup() {
				Children = {
					new TranslateTransform(1.1,1.1),
					control_scale,
				}
			};

		}
		protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
			base.OnRenderSizeChanged(sizeInfo);
			control_scale.ScaleX = control_scale.ScaleY = (Math.Min(sizeInfo.NewSize.Width, sizeInfo.NewSize.Height) - 6) / 2.2;
		}

		protected override void OnRender(DrawingContext drawingContext) {
			base.OnRender(drawingContext);
			drawingContext.PushTransform(ts);
			double[] er_circle_r_table = { .1, .2, .3, .4, .5, .6,.7,.8,.9, 1,1.2,1.4,1.6,1.8, 2.5, 3, 4, 5, 10, 20, 30, 40, 50 };
			
			drawingContext.DrawRectangle(this.Background, null, new Rect(-1.1, -1.1, 2.2, 2.2));

			//	drawingContext.PushOpacityMask(new DrawingBrush(new GeometryDrawing(new RadialGradientBrush(Colors.Black, Colors.Transparent), null, new EllipseGeometry(new Point(0, 0), 1, 1))));

			var mask_dg = new DrawingGroup();
			mask_dg.Children.Add(new GeometryDrawing(Brushes.Black, base_pen, new EllipseGeometry(new Point(0, 0), 500, 500)));
			mask_dg.Children.Add(new GeometryDrawing(Brushes.Transparent, base_pen, new EllipseGeometry(new Point(0, 0), 10000, 10000)));
			//drawingContext.DrawDrawing(mask_dg);
			drawingContext.PushOpacityMask(new DrawingBrush(mask_dg));
			foreach (var r in er_circle_r_table) {
				drawingContext.DrawGeometry(Brushes.Transparent, R_pen, new EllipseGeometry(new Point(r / (1 + r), 0), 1 / (1 + r), 1 / (1 + r)));
				drawingContext.DrawGeometry(Brushes.Transparent, G_pen, new EllipseGeometry(new Point(-r / (1 + r), 0), 1 / (1 + r), 1 / (1 + r)));
			}

			foreach (var r in er_circle_r_table) {
				drawingContext.DrawGeometry(Brushes.Transparent, X_pen, new EllipseGeometry(new Point(1, 1 / r), 1 / r, 1 / r));
				drawingContext.DrawGeometry(Brushes.Transparent, X_pen, new EllipseGeometry(new Point(1, -1 / r), 1 / r, 1 / r));
				drawingContext.DrawGeometry(Brushes.Transparent, B_pen, new EllipseGeometry(new Point(-1, 1 / r), 1 / r, 1 / r));
				drawingContext.DrawGeometry(Brushes.Transparent, B_pen, new EllipseGeometry(new Point(-1, -1 / r), 1 / r, 1 / r));
			}
			//drawingContext.Pop();
			//drawingContext.PushOpacityMask(new DrawingBrush(new GeometryDrawing(Brushes.Wheat, new Pen(new SolidColorBrush(Color.FromArgb(128,100,100,100)), new EllipseGeometry(new Point(0, 0), 0.5, 0.5))));

			//drawingContext.DrawGeometry(Brushes.Black, null, new EllipseGeometry(new Point(0, 0), 0.5, 0.5));

			drawingContext.DrawGeometry(Brushes.Transparent, base_pen, new EllipseGeometry(new Point(0, 0), 1, 1));
		}
	}
}
