using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using NUnit.Framework;

namespace ZeeGraph.Tests
{
    [TestFixture]
    public class BarSettingsDefaultTests
    {
        [Test]
        public void BarBaseAxisTest()
        {
            GraphPane pane = new GraphPane();
            BarSettings settings = new BarSettings(pane);

            var axis = settings.BarBaseAxis();

            Assert.AreSame(pane.XAxis, axis);
        }

        [Test]
        public void BaseTest()
        {
            GraphPane pane = new GraphPane();
            BarSettings settings = new BarSettings(pane);

            Assert.That(settings.Base == BarBase.X, "The Bar Base setting is not the x axis");
        }

        [Test]
        public void ClusterScaleWidthTest()
        {
            GraphPane pane = new GraphPane();
            BarSettings settings = new BarSettings(pane);

            var width = settings.ClusterScaleWidth;
            Assert.AreEqual(1.0d, width);
        }

        [Test]
        public void ClusterScaleWidthAutoIsSetToTrue()
        {
            GraphPane pane = new GraphPane();
            BarSettings settings = new BarSettings(pane);

            Assert.That(settings.ClusterScaleWidthAuto, "Cluster scale width is not set to auto");
        }

        [Test]
        public void GetClusterWidthTest()
        {
            GraphPane pane = new GraphPane();
            BarSettings settings = new BarSettings(pane);

            float value = settings.GetClusterWidth();

            Assert.AreEqual(0.0f, value);
        }

        [Test]
        public void GetDefaltMinBarGapTest()
        {
            GraphPane pane = new GraphPane();
            BarSettings bar = new BarSettings(pane);

            var value = bar.MinBarGap;

            Assert.AreEqual(0.200000003f, value);
        }

        [Test]
        public void GetDefaultMinClusterGapTest()
        {
            GraphPane pane = new GraphPane();
            BarSettings bar = new BarSettings(pane);

            var value = bar.MinClusterGap;

            Assert.AreEqual(1.0f, value);
        }
    }

    [TestFixture]
    public class BarSettingsCodeCoverageTests
    {
        [Test]
        public void ClusterScaleWidthAutoSetToFalseWhenSettingCluserScaleWidthManally()
        {
            GraphPane pane = new GraphPane();
            BarSettings settings = new BarSettings(pane);

            Assert.That(settings.ClusterScaleWidth != 999.0f, "Test is incorrect, already set to test value");

            settings.ClusterScaleWidth = 999.0f;

            Assert.That(settings.ClusterScaleWidth == 999.0f, "Cluster scale width is not set the requested property");
            Assert.That(!settings.ClusterScaleWidthAuto, "Cluster scale width is set to auto");
        }

        [Test]
        public void CalcClusterScaleWidth()
        {
            GraphPane pane = new GraphPane();
            BarSettings settings = new BarSettings(pane);

            settings.CalcClusterScaleWidth();

            double value = settings.ClusterScaleWidth;

            Assert.AreEqual(1.0d, value);
        }

        [Test]
        public void CalcClusterScaleWidth_WithAutoSetToFalseWillNotChangeValueWithNoJapeneseCandleSticks()
        {
            GraphPane pane = new GraphPane();
            BarSettings settings = new BarSettings(pane);

            settings.ClusterScaleWidth = 999.111f;
            var expectedValue = settings.ClusterScaleWidth;

            settings.ClusterScaleWidthAuto = false;

            settings.CalcClusterScaleWidth();

            var value = settings.ClusterScaleWidth;

            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        public void CalcClusterScaleWidth_WithAutoSetToTrueAndXAxisScaleIsAnyOrdinalWillNotChangeValueWithNoJapeneseCandleSticks()
        {
            GraphPane pane = new GraphPane();
            BarSettings settings = new BarSettings(pane);

            settings.ClusterScaleWidthAuto = true;
            settings.BarBaseAxis().Type = AxisType.Ordinal;

            var expectedValue = settings.ClusterScaleWidth;

            settings.CalcClusterScaleWidth();

            var value = settings.ClusterScaleWidth;

            Assert.AreEqual(expectedValue, value);
        }

        [Test]
        public void CalcClustScaleWidth_WithBarItems()
        {
            // todo: this passes but doesnt cover GetMinStepSize Code
            GraphPane pane = new GraphPane();
            BarSettings settings = new BarSettings(pane);

            pane.CurveList.Add( new BarItem("BarItemTest", new double[]{1,2,3,4,5,6}, new double[]{50,100,100,200,300,75}, Color.Blue));

            settings.CalcClusterScaleWidth();
            var value = settings.ClusterScaleWidth;

            Assert.AreEqual(1.0d, value);
        }
    }
}
