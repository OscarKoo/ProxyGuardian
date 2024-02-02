using System.Windows.Forms;

namespace ProxyGuardian.Controls
{
    public class LabelTextBox : TableLayoutPanel
    {
        public Label Label { get; }
        public TextBox TextBox { get; }

        public LabelTextBox()
        {
            Label = new Label
            {
                AutoSize = true
            };
            var margin = Label.Margin;
            margin.Top = 8;
            Label.Margin = margin;

            TextBox = new TextBox
            {
                Dock = DockStyle.Fill
            };
            var size = Size;
            size.Height = TextBox.Height + 2;
            Size = size;

            AutoSize = true;

            ColumnCount = 2;
            RowCount = 1;
            ColumnStyles.Add(new ColumnStyle());
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            RowStyles.Add(new RowStyle());
            Controls.Add(Label, 0, 0);
            Controls.Add(TextBox, 1, 0);
        }
    }
}