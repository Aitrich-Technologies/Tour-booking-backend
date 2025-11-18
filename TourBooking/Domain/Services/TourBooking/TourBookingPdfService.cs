using Domain.Services.TourBooking.DTO;
using Microsoft.AspNetCore.Hosting;
using MigraDocCore.DocumentObjectModel;
using MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes;
using MigraDocCore.DocumentObjectModel.Shapes;
using MigraDocCore.DocumentObjectModel.Tables;
using MigraDocCore.Rendering;
using System.IO;
using System.Reflection;

public class TourBookingPdfService
{
    private readonly IWebHostEnvironment _env;

    public TourBookingPdfService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public byte[] GenerateBookingPdf(GetBookingDto booking)
    {
        var doc = new Document();
        doc.Info.Title = "Tour Booking Confirmation";
        doc.DefaultPageSetup.TopMargin = Unit.FromCentimeter(2);
        doc.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(2);
        doc.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(2);
        doc.DefaultPageSetup.RightMargin = Unit.FromCentimeter(2);

        DefineStyles(doc);

        var section = doc.AddSection();

        // -------------------------------------------------------------
        // HEADER WITH LOGO AND BRAND IDENTITY
        // -------------------------------------------------------------

        var headerTable = section.Headers.Primary.AddTable();
        headerTable.Borders.Visible = false;
        headerTable.AddColumn("3cm"); // Logo column
        headerTable.AddColumn("7cm"); // Company info
        headerTable.AddColumn("7cm"); // Contact info

        var headerRow = headerTable.AddRow();
        headerRow.VerticalAlignment = VerticalAlignment.Center;

        //// Logo cell
        //var logoCell = headerRow.Cells[0];
        //AddLogo(logoCell);

        // Company info cell
        var companyCell = headerRow.Cells[1];
        var companyName = companyCell.AddParagraph("Lions Sports Club");
        companyName.Format.Font.Size = 16;
        companyName.Format.Font.Bold = true;
        companyName.Format.Font.Color = new Color(41, 128, 185);

        var tagline = companyCell.AddParagraph("Your Adventure Partner");
        tagline.Format.Font.Size = 9;
        tagline.Format.Font.Color = Colors.Gray;

        // Contact info cell
        var contactCell = headerRow.Cells[2];
        contactCell.Format.Alignment = ParagraphAlignment.Right;
        var contact = contactCell.AddParagraph("www.lionssportsclub.com");
        contact.Format.Font.Size = 9;
        contact.Format.Font.Color = Colors.Gray;
        var email = contactCell.AddParagraph("lionssportsclub2000@gmail.com");
        email.Format.Font.Size = 9;
        email.Format.Font.Color = Colors.Gray;

        section.AddParagraph().AddLineBreak();
        section.AddParagraph().AddLineBreak();
        section.AddParagraph().AddLineBreak();

        // Divider line
        section.Headers.Primary.AddParagraph().AddLineBreak();
        var divider = section.Headers.Primary.AddParagraph();
        divider.Format.Borders.Bottom = new Border { Width = 2, Color = new Color(41, 128, 185) };
        divider.Format.SpaceAfter = "0.5cm";

        // -------------------------------------------------------------
        // TITLE SECTION
        // -------------------------------------------------------------
        var titleSection = section.AddTable();
        titleSection.Borders.Visible = false;
        titleSection.AddColumn("17cm");

        var titleRow = titleSection.AddRow();
        titleRow.Shading.Color = new Color(41, 128, 185);
        titleRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
        titleRow.Height = Unit.FromCentimeter(1.5);

        var title = titleRow.Cells[0].AddParagraph("BOOKING CONFIRMATION");
        title.Format.Font.Size = 20;
        title.Format.Font.Bold = true;
        title.Format.Font.Color = Colors.White;
        title.Format.Alignment = ParagraphAlignment.Center;

        section.AddParagraph().AddLineBreak();

        // -------------------------------------------------------------
        // BOOKING SUMMARY - MODERN CARD STYLE
        // -------------------------------------------------------------
        // BOOKING SUMMARY - MODERN CARD STYLE
        // -------------------------------------------------------------
        var summaryTable = section.AddTable();
        summaryTable.Borders.Color = new Color(189, 195, 199);
        summaryTable.Borders.Width = 1;
        summaryTable.Borders.Left.Width = 4;
        summaryTable.Borders.Left.Color = new Color(46, 204, 113);
        summaryTable.LeftPadding = Unit.FromCentimeter(0.3);
        summaryTable.RightPadding = Unit.FromCentimeter(0.3);

        summaryTable.AddColumn("4.5cm");
        summaryTable.AddColumn("3cm");
        summaryTable.AddColumn("3.5cm");
        summaryTable.AddColumn("3.5cm");
        summaryTable.AddColumn("3.5cm");

        var summaryRow = summaryTable.AddRow();
        summaryRow.Shading.Color = new Color(236, 240, 241);
        summaryRow.TopPadding = Unit.FromCentimeter(0.3);
        summaryRow.BottomPadding = Unit.FromCentimeter(0.3);

        AddInfoCell(summaryRow.Cells[0], "Booking ID", booking.Id.ToString());
        AddInfoCell(summaryRow.Cells[1], "Status", booking.Status ?? "-", GetStatusColor(booking.Status));
        AddInfoCell(summaryRow.Cells[2], "Booking Date", booking.CreatedAt?.ToString("MMM dd, yyyy") ?? "-");
        AddInfoCell(summaryRow.Cells[3], "Departure Date", booking.Tour?.DepartureDate?.ToString("MMM dd, yyyy") ?? "-");
        AddInfoCell(summaryRow.Cells[4], "Arrival Date", booking.Tour?.ArrivalDate?.ToString("MMM dd, yyyy") ?? "-");

        section.AddParagraph().AddLineBreak();

        // -------------------------------------------------------------
        // TOUR DETAILS - ENHANCED SECTION
        // -------------------------------------------------------------
        AddSectionHeader(section, "Tour Details", "🗺️");

        if (booking.Tour != null)
        {
            var tourBox = section.AddTable();
            tourBox.Borders.Width = 1;
            tourBox.Borders.Color = new Color(189, 195, 199);
            tourBox.Shading.Color = new Color(250, 250, 250);
            tourBox.AddColumn("17cm");
            tourBox.TopPadding = Unit.FromCentimeter(0.4);
            tourBox.BottomPadding = Unit.FromCentimeter(0.4);
            tourBox.LeftPadding = Unit.FromCentimeter(0.4);
            tourBox.RightPadding = Unit.FromCentimeter(0.4);

            var tourRow = tourBox.AddRow();
            var tourCell = tourRow.Cells[0];

            var tourName = tourCell.AddParagraph(booking.Tour.TourName);
            tourName.Format.Font.Size = 14;
            tourName.Format.Font.Bold = true;
            tourName.Format.Font.Color = new Color(52, 73, 94);
            tourName.Format.SpaceAfter = "0.2cm";

            var tourDesc = tourCell.AddParagraph(booking.Tour.TourDescription);
            tourDesc.Format.Font.Size = 10;
            tourDesc.Format.Font.Color = new Color(127, 140, 141);
        }

        section.AddParagraph().AddLineBreak();

        // -------------------------------------------------------------
        // CUSTOMER INFORMATION - TWO COLUMN LAYOUT
        // -------------------------------------------------------------
        AddSectionHeader(section, "Customer Information", "👤");

        var customerTable = section.AddTable();
        customerTable.Borders.Width = 1;
        customerTable.Borders.Color = new Color(189, 195, 199);
        customerTable.AddColumn("8.5cm");
        customerTable.AddColumn("8.5cm");

        AddCustomerInfoRow(customerTable, "Full Name", $"{booking.FirstName} {booking.LastName}", "Gender", booking.Gender ?? "-");
        AddCustomerInfoRow(customerTable, "Citizenship", booking.Citizenship ?? "-", "Passport Number", booking.PassportNumber ?? "-");

        section.AddParagraph().AddLineBreak();

        // -------------------------------------------------------------
        // PARTICIPANTS TABLE - MODERN DESIGN
        // -------------------------------------------------------------
        AddSectionHeader(section, "Participant List", "👥");

        if (booking.Participants?.Any() == true)
        {
            var table = section.AddTable();
            table.Borders.Width = 1;
            table.Borders.Color = new Color(189, 195, 199);

            table.AddColumn("1.5cm");
            table.AddColumn("7.5cm");
            table.AddColumn("4cm");
            table.AddColumn("4cm");

            // Header row
            var participantHeaderRow = table.AddRow();
            participantHeaderRow.Shading.Color = new Color(52, 73, 94);
            participantHeaderRow.HeadingFormat = true;
            participantHeaderRow.Height = Unit.FromCentimeter(0.8);
            participantHeaderRow.VerticalAlignment = VerticalAlignment.Center;

            AddHeaderCell(participantHeaderRow.Cells[0], "#");
            AddHeaderCell(participantHeaderRow.Cells[1], "Full Name");
            AddHeaderCell(participantHeaderRow.Cells[2], "Gender");
            AddHeaderCell(participantHeaderRow.Cells[3], "Passport Number");

            // Data rows with alternating colors
            int index = 1;
            foreach (var p in booking.Participants)
            {
                var row = table.AddRow();
                row.Height = Unit.FromCentimeter(0.8);
                row.VerticalAlignment = VerticalAlignment.Center;

                if (index % 2 == 0)
                {
                    row.Shading.Color = new Color(250, 250, 250);
                }

                AddDataCell(row.Cells[0], index.ToString());
                AddDataCell(row.Cells[1], $"{p.FirstName} {p.LastName}");
                AddDataCell(row.Cells[2], p.Gender ?? "-");
                AddDataCell(row.Cells[3], p.PassportNumber);

                index++;
            }
        }
        else
        {
            var noData = section.AddParagraph("No participants registered for this tour.");
            noData.Format.Font.Color = Colors.Gray;
            noData.Format.Font.Italic = true;
        }

        section.AddParagraph().AddLineBreak();

        // -------------------------------------------------------------
        // FOOTER NOTE
        // -------------------------------------------------------------
        var noteBox = section.AddTable();
        noteBox.Borders.Width = 1;
        noteBox.Borders.Color = new Color(52, 152, 219);
        noteBox.Borders.Left.Width = 4;
        noteBox.Shading.Color = new Color(235, 245, 251);
        noteBox.AddColumn("17cm");
        noteBox.TopPadding = Unit.FromCentimeter(0.3);
        noteBox.BottomPadding = Unit.FromCentimeter(0.3);
        noteBox.LeftPadding = Unit.FromCentimeter(0.4);

        var noteRow = noteBox.AddRow();
        var note = noteRow.Cells[0].AddParagraph("Please keep this confirmation for your records. Present this document at check-in.");
        note.Format.Font.Size = 9;
        note.Format.Font.Color = new Color(52, 73, 94);

        // -------------------------------------------------------------
        // FOOTER
        // -------------------------------------------------------------
        var footer = section.Footers.Primary.AddParagraph();
        footer.Format.Borders.Top = new Border { Width = 1, Color = new Color(189, 195, 199) };
        footer.Format.SpaceBefore = "0.3cm";
        footer.AddLineBreak();

        footer.AddText("Thank you for choosing Lions Sports Club | We wish you an amazing adventure!");
        footer.Format.Alignment = ParagraphAlignment.Center;
        footer.Format.Font.Size = 9;
        footer.Format.Font.Color = Colors.Gray;

        var pageInfo = section.Footers.Primary.AddParagraph();
        pageInfo.AddText("Page ");
        pageInfo.AddPageField();
        pageInfo.AddText(" | Generated on " + DateTime.Now.ToString("MMM dd, yyyy HH:mm"));
        pageInfo.Format.Alignment = ParagraphAlignment.Right;
        pageInfo.Format.Font.Size = 8;
        pageInfo.Format.Font.Color = Colors.Gray;

        // -------------------------------------------------------------
        // RENDER PDF
        // -------------------------------------------------------------
        var renderer = new PdfDocumentRenderer();
        renderer.Document = doc;
        renderer.RenderDocument();

        using var stream = new MemoryStream();
        renderer.PdfDocument.Save(stream);
        return stream.ToArray();
    }

    // -------------------------------------------------------------
    // STYLE DEFINITIONS
    // -------------------------------------------------------------
    private void DefineStyles(Document doc)
    {
        var normal = doc.Styles["Normal"];
        normal.Font.Name = "Arial";
        normal.Font.Size = 10;

        doc.Styles.AddStyle("Table", "Normal");
    }

    // -------------------------------------------------------------
    // HELPERS (unchanged from your original code)
    // -------------------------------------------------------------
    private void AddSectionHeader(Section section, string text, string icon)
    {
        var headerTable = section.AddTable();
        headerTable.Borders.Visible = false;
        headerTable.AddColumn("17cm");

        var row = headerTable.AddRow();
        row.Shading.Color = new Color(236, 240, 241);
        row.Height = Unit.FromCentimeter(0.8);
        row.VerticalAlignment = VerticalAlignment.Center;

        var p = row.Cells[0].AddParagraph($"  {icon}  {text}");
        p.Format.Font.Size = 13;
        p.Format.Font.Bold = true;
        p.Format.Font.Color = new Color(52, 73, 94);

        section.AddParagraph().Format.SpaceAfter = "0.2cm";
    }

    private void AddInfoCell(Cell cell, string label, string value, Color? valueColor = null)
    {
        var labelPara = cell.AddParagraph(label);
        labelPara.Format.Font.Size = 8;
        labelPara.Format.Font.Color = Colors.Gray;
        labelPara.Format.SpaceAfter = "0.05cm";

        var valuePara = cell.AddParagraph(value);
        valuePara.Format.Font.Size = 11;
        valuePara.Format.Font.Bold = true;
        valuePara.Format.Font.Color = valueColor ?? new Color(52, 73, 94);
    }

    private void AddCustomerInfoRow(Table table, string label1, string value1, string label2, string value2)
    {
        var row = table.AddRow();
        row.TopPadding = Unit.FromCentimeter(0.3);
        row.BottomPadding = Unit.FromCentimeter(0.3);

        if (table.Rows.Count % 2 == 0)
        {
            row.Shading.Color = new Color(250, 250, 250);
        }

        var cell1 = row.Cells[0];
        cell1.Format.LeftIndent = "0.3cm";
        var p1 = cell1.AddParagraph();
        p1.AddFormattedText($"{label1}: ", TextFormat.Bold);
        p1.AddText(value1);
        p1.Format.Font.Size = 10;

        var cell2 = row.Cells[1];
        cell2.Format.LeftIndent = "0.3cm";
        var p2 = cell2.AddParagraph();
        p2.AddFormattedText($"{label2}: ", TextFormat.Bold);
        p2.AddText(value2);
        p2.Format.Font.Size = 10;
    }

    private void AddHeaderCell(Cell cell, string text)
    {
        cell.Format.Alignment = ParagraphAlignment.Center;
        var p = cell.AddParagraph(text);
        p.Format.Font.Bold = true;
        p.Format.Font.Color = Colors.White;
        p.Format.Font.Size = 10;
    }

    private void AddDataCell(Cell cell, string text)
    {
        cell.Format.Alignment = ParagraphAlignment.Center;
        cell.VerticalAlignment = VerticalAlignment.Center;
        var p = cell.AddParagraph(text);
        p.Format.Font.Size = 9;
        p.Format.Font.Color = new Color(52, 73, 94);
    }

    private Color GetStatusColor(string status)
    {
        return status?.ToLower() switch
        {
            "confirmed" => new Color(46, 204, 113),
            "pending" => new Color(241, 196, 15),
            "cancelled" => new Color(231, 76, 60),
            _ => new Color(52, 73, 94)
        };
    }
   
}