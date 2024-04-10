namespace Flight_Booking_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class one : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Airline",
                c => new
                    {
                        AirlineId = c.Int(nullable: false, identity: true),
                        AirlineName = c.String(nullable: false, maxLength: 30),
                        SeatingCapacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AirlineId);
            
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        FlightId = c.Int(nullable: false, identity: true),
                        AirlineId = c.Int(nullable: false),
                        AirportId = c.Int(nullable: false),
                        Source = c.String(nullable: false),
                        Destination = c.String(nullable: false),
                        SourceCode = c.String(),
                        DestinationCode = c.String(),
                        ArrivalTime = c.String(nullable: false),
                        DepartureTime = c.String(nullable: false),
                        TotalSeatsCapacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FlightId)
                .ForeignKey("dbo.Airline", t => t.AirlineId, cascadeDelete: true)
                .ForeignKey("dbo.Airport", t => t.AirportId, cascadeDelete: true)
                .Index(t => t.AirlineId)
                .Index(t => t.AirportId);
            
            CreateTable(
                "dbo.Airport",
                c => new
                    {
                        AirportId = c.Int(nullable: false, identity: true),
                        AirportName = c.String(nullable: false, maxLength: 30),
                        City = c.String(nullable: false, maxLength: 30),
                        Country = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.AirportId);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ScheduleId = c.Int(nullable: false, identity: true),
                        FlightId = c.Int(nullable: false),
                        AirlineId = c.Int(nullable: false),
                        Source = c.String(),
                        Destination = c.String(),
                        FlightDate = c.DateTime(nullable: false),
                        ArrivalTime = c.String(),
                        DepartureTime = c.String(),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ScheduleId)
                .ForeignKey("dbo.Airline", t => t.AirlineId, cascadeDelete: true)
                .ForeignKey("dbo.Flights", t => t.FlightId, cascadeDelete: false)
                .Index(t => t.FlightId)
                .Index(t => t.AirlineId);
            
            CreateTable(
                "dbo.Booking",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        From = c.String(),
                        To = c.String(nullable: false),
                        BookingDate = c.DateTime(nullable: false),
                        NumberOfPassengers = c.Int(nullable: false),
                        FlightId = c.Int(nullable: false),
                        ScheduleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Flights", t => t.FlightId, cascadeDelete: true)
                .ForeignKey("dbo.Schedules", t => t.ScheduleId, cascadeDelete: false)
                .ForeignKey("dbo.UserLogin", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.FlightId)
                .Index(t => t.ScheduleId);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 40),
                        LastName = c.String(nullable: false, maxLength: 40),
                        Email = c.String(nullable: false, maxLength: 20),
                        Role = c.String(),
                        Password = c.String(nullable: false, maxLength: 16),
                        ConfirmPassword = c.String(maxLength: 16),
                        Age = c.Int(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentDate = c.DateTime(nullable: false),
                        PaymentMode = c.String(),
                        UserName = c.String(),
                        CardType = c.String(),
                        CardNo = c.Long(nullable: false),
                        CVV = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BookingId = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        ScheduleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Booking", t => t.BookingId, cascadeDelete: true)
                .ForeignKey("dbo.Schedules", t => t.ScheduleId, cascadeDelete: false)
                .ForeignKey("dbo.UserLogin", t => t.UserID, cascadeDelete: false)
                .Index(t => t.BookingId)
                .Index(t => t.UserID)
                .Index(t => t.ScheduleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Booking", "UserID", "dbo.UserLogin");
            DropForeignKey("dbo.Payments", "UserID", "dbo.UserLogin");
            DropForeignKey("dbo.Payments", "ScheduleId", "dbo.Schedules");
            DropForeignKey("dbo.Payments", "BookingId", "dbo.Booking");
            DropForeignKey("dbo.Booking", "ScheduleId", "dbo.Schedules");
            DropForeignKey("dbo.Booking", "FlightId", "dbo.Flights");
            DropForeignKey("dbo.Schedules", "FlightId", "dbo.Flights");
            DropForeignKey("dbo.Schedules", "AirlineId", "dbo.Airline");
            DropForeignKey("dbo.Flights", "AirportId", "dbo.Airport");
            DropForeignKey("dbo.Flights", "AirlineId", "dbo.Airline");
            DropIndex("dbo.Payments", new[] { "ScheduleId" });
            DropIndex("dbo.Payments", new[] { "UserID" });
            DropIndex("dbo.Payments", new[] { "BookingId" });
            DropIndex("dbo.Booking", new[] { "ScheduleId" });
            DropIndex("dbo.Booking", new[] { "FlightId" });
            DropIndex("dbo.Booking", new[] { "UserID" });
            DropIndex("dbo.Schedules", new[] { "AirlineId" });
            DropIndex("dbo.Schedules", new[] { "FlightId" });
            DropIndex("dbo.Flights", new[] { "AirportId" });
            DropIndex("dbo.Flights", new[] { "AirlineId" });
            DropTable("dbo.Payments");
            DropTable("dbo.UserLogin");
            DropTable("dbo.Booking");
            DropTable("dbo.Schedules");
            DropTable("dbo.Airport");
            DropTable("dbo.Flights");
            DropTable("dbo.Airline");
        }
    }
}
