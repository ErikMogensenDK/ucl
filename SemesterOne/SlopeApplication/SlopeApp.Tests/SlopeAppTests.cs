using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SlopeApp;
using Xunit;

namespace SlopeApp.Tests
{
    public class SlopeAppTests
    {
        [Fact]
        public void CalculateSlope_SimpleValuesShouldReturnZero()
        {
            // Given
            double x1, x2, y1, y2;
            x1 = 3;
            y1 = 3; 
            x2 = 5;
            y2 = 3;
            double expected = 0;

            // When
            double actual = Program.CalculateSlope(x1, x2, y1, y2);

            // Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateSlope_SimpleValuesShouldReturnOne()
        {
            // Given
            double x1, x2, y1, y2;
            x1 = 3;
            y1 = 3; 
            x2 = 5;
            y2 = 5;
            double expected = 1;

            // When
            double actual = Program.CalculateSlope(x1, x2, y1, y2);

            // Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateSlope_SimpleValuesShouldReturnZeroPointFive()
        {
            // Given
            double x1, x2, y1, y2;
            x1 = 3;
            y1 = 3; 
            x2 = 5;
            y2 = 4;
            double expected = 0.5;

            // When
            double actual = Program.CalculateSlope(x1, x2, y1, y2);

            // Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestName()
        {
            // Given
        
            // When
        
            // Then
        }
    }
}