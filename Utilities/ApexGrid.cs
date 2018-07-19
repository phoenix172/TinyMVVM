﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace TinyMVVM.Utilities
{
  /// <summary>
  /// The ApexGrid control is a Grid that supports easy definition of rows and columns.
  /// </summary>
  public class ApexGrid : Grid
  {
    /// <summary>
    /// Called when the rows property is changed.
    /// </summary>
    /// <param name="dependencyObject">The dependency object.</param>
    /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
    private static void OnRowsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
      //  Get the apex grid.
      ApexGrid apexGrid = dependencyObject as ApexGrid;

      //  Clear any current rows definitions.
      apexGrid.RowDefinitions.Clear();

      //  Add each row from the row lengths definition.
      foreach (var rowLength in StringLengthsToGridLengths(apexGrid.Rows))
        apexGrid.RowDefinitions.Add(new RowDefinition() { Height = rowLength });
    }

    /// <summary>
    /// Called when the columns property is changed.
    /// </summary>
    /// <param name="dependencyObject">The dependency object.</param>
    /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
    private static void OnColumnsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
      //  Get the apex grid.
      ApexGrid apexGrid = dependencyObject as ApexGrid;

      //  Clear any current column definitions.
      apexGrid.ColumnDefinitions.Clear();

      //  Add each column from the column lengths definition.
      foreach (var columnLength in StringLengthsToGridLengths(apexGrid.Columns))
        apexGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = columnLength });
    }

    /// <summary>
    /// Turns a string of lengths, such as "3*,Auto,2000" into a set of gridlength.
    /// </summary>
    /// <param name="lengths">The string of lengths, separated by commas.</param>
    /// <returns>A list of GridLengths.</returns>
    private static List<GridLength> StringLengthsToGridLengths(string lengths)
    {
      //  Create the list of GridLengths.
      List<GridLength> gridLengths = new List<GridLength>();

      //  If the string is null or empty, this is all we can do.
      if (string.IsNullOrEmpty(lengths))
        return gridLengths;

      //  Split the string by comma.
      string[] theLengths = lengths.Split(',');
            
      //  Create a grid length converter.
      GridLengthConverter gridLengthConverter = new GridLengthConverter();
      
      //  Use the grid length converter to set each length.
      foreach(var length in theLengths)
        gridLengths.Add((GridLength)gridLengthConverter.ConvertFromString(length));
      
      //  Return the grid lengths.
      return gridLengths;
    }

    /// <summary>
    /// The rows dependency property.
    /// </summary>
    private static readonly DependencyProperty rowsProperty =
        DependencyProperty.Register("Rows", typeof(string), typeof(ApexGrid),
        new PropertyMetadata(null, new PropertyChangedCallback(OnRowsChanged)));

    /// <summary>
    /// The columns dependency property.
    /// </summary>
    private static readonly DependencyProperty columnsProperty =
        DependencyProperty.Register("Columns", typeof(string), typeof(ApexGrid),
        new PropertyMetadata(null, new PropertyChangedCallback(OnColumnsChanged)));

    /// <summary>
    /// Gets or sets the rows.
    /// </summary>
    /// <value>The rows.</value>
    [Description("The rows property."), Category("Common Properties")]
    public string Rows
    {
      get { return (string)GetValue(rowsProperty); }
      set { SetValue(rowsProperty, value); }
    }

    /// <summary>
    /// Gets or sets the columns.
    /// </summary>
    /// <value>The columns.</value>
    [Description("The columns property."), Category("Common Properties")]
    public string Columns
    {
      get { return (string)GetValue(columnsProperty); }
      set { SetValue(columnsProperty, value); }

    }
  }
}