// Copyright (c) 2019 Javier Cañon 
// https://www.javiercanon.com 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
using System;
using System.Collections.Generic;

namespace SO.SalesDemo.Model {
    public interface IDataProvider {
        SalesGroup GetTotalSalesByRange(DateTime start, DateTime end);
        IEnumerable<SalesGroup> GetSales(DateTime start, DateTime end, GroupingPeriod period);
        IEnumerable<SalesGroup> GetSalesByChannel(DateTime start, DateTime end, GroupingPeriod period);
        IEnumerable<SalesGroup> GetSalesByProduct(DateTime start, DateTime end, GroupingPeriod period);
        IEnumerable<SalesGroup> GetSalesByRegion(DateTime start, DateTime end, GroupingPeriod period);
        IEnumerable<SalesGroup> GetSalesBySector(DateTime start, DateTime end, GroupingPeriod period);
    }
    public static class DataProviderExtensions {
        public static SalesGroup GetTotalSalesByRange(this IDataProvider dataProvider, DateTimeRange range) {
            Verify(dataProvider);
            return dataProvider.GetTotalSalesByRange(range.Start, range.End);
        }
        public static IEnumerable<SalesGroup> GetSales(this IDataProvider dataProvider, DateTimeRange range, GroupingPeriod period) {
            Verify(dataProvider);
            return dataProvider.GetSales(range.Start, range.End, period);
        }
        public static IEnumerable<SalesGroup> GetSalesByChannel(this IDataProvider dataProvider, DateTimeRange range, GroupingPeriod period) {
            Verify(dataProvider);
            return dataProvider.GetSalesByChannel(range.Start, range.End, period);
        }
        public static IEnumerable<SalesGroup> GetSalesByProduct(this IDataProvider dataProvider, DateTimeRange range, GroupingPeriod period) {
            Verify(dataProvider);
            return dataProvider.GetSalesByProduct(range.Start, range.End, period);
        }
        public static IEnumerable<SalesGroup> GetSalesByRegion(this IDataProvider dataProvider, DateTimeRange range, GroupingPeriod period) {
            Verify(dataProvider);
            return dataProvider.GetSalesByRegion(range.Start, range.End, period);
        }
        public static IEnumerable<SalesGroup> GetSalesBySector(this IDataProvider dataProvider, DateTimeRange range, GroupingPeriod period) {
            Verify(dataProvider);
            return dataProvider.GetSalesBySector(range.Start, range.End, period);
        }

        static void Verify(IDataProvider dataProvider) {
            if(dataProvider == null)
                throw new ArgumentNullException();
        }
    }
    //
    public interface IDataGenerator {
        event ProgressEventHandler GenerationStart;
        event ProgressEventHandler GenerationProgress;
        event ProgressEventHandler GenerationComplete;
    }
    public delegate void ProgressEventHandler(
            object sender, ProgressEventArgs e
        );
    public class ProgressEventArgs {
        public ProgressEventArgs(int progress) {
            Progress = progress;
        }
        public int Progress { get; private set; }
    }
}
