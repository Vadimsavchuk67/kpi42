using Microsoft.Data.Sqlite;
using PostService.CommonTypes;
using PostService.Models;

namespace PostService.DataAccess;

public class PostingRepository : IPostingRepository
{
    private const string ConnectionString = "Data Source=PostingDb.db";

    public void CreateDb()
    {
        using var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        const string sql = @"
            CREATE TABLE IF NOT EXISTS Postings (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                [From] TEXT NOT NULL,
                [To] TEXT NOT NULL,
                Content TEXT NOT NULL,
                DeliveryType INTEGER NOT NULL,
                Weight REAL NOT NULL,
                Width INTEGER NOT NULL,
                Height INTEGER NOT NULL,
                Depth INTEGER NOT NULL,
                Value REAL NOT NULL,
                Price REAL NOT NULL,
                CreatedAt INTEGER NOT NULL);";
        using var cmd = new SqliteCommand(sql, conn);
        cmd.ExecuteNonQuery();
    }

    public List<Posting> GetList()
    {
        var result = new List<Posting>();
        using var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        using var cmd = new SqliteCommand("SELECT * FROM Postings ORDER BY Id", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
            result.Add(ReadPosting(reader));

        return result;
    }

    public Posting? GetById(int postingId)
    {
        using var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        using var cmd = new SqliteCommand("SELECT * FROM Postings WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", postingId);
        using var reader = cmd.ExecuteReader();
        if (reader.Read())
            return ReadPosting(reader);

        return null;
    }

    public int Create(Posting posting)
    {
        using var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        const string sql = "INSERT INTO Postings ([From], [To], [Content], [DeliveryType], [Weight], [Width], [Height], [Depth], [Value], [Price], [CreatedAt]) VALUES (@From, @To, @Content, @DeliveryType, @Weight, @Width, @Height, @Depth, @Value, @Price, @CreatedAt) RETURNING Id";
        using var cmd = new SqliteCommand(sql, conn);
        AddParameters(cmd, posting);
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void Update(Posting posting)
    {
        using var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        const string sql = "UPDATE Postings SET [From] = @From, [To] = @To, [Content] = @Content, [DeliveryType] = @DeliveryType, [Weight] = @Weight, [Width] = @Width, [Height] = @Height, [Depth] = @Depth, [Value] = @Value, [Price] = @Price, [CreatedAt] = @CreatedAt WHERE Id = @Id";
        using var cmd = new SqliteCommand(sql, conn);
        AddParameters(cmd, posting);
        cmd.Parameters.AddWithValue("@Id", posting.Id);
        cmd.ExecuteNonQuery();
    }

    public int Delete(int postingId)
    {
        using var conn = new SqliteConnection(ConnectionString);
        conn.Open();
        using var cmd = new SqliteCommand("DELETE FROM Postings WHERE Id = @Id", conn);
        cmd.Parameters.AddWithValue("@Id", postingId);
        return cmd.ExecuteNonQuery();
    }

    private static void AddParameters(SqliteCommand cmd, Posting posting)
    {
        cmd.Parameters.AddWithValue("@From", posting.From);
        cmd.Parameters.AddWithValue("@To", posting.To);
        cmd.Parameters.AddWithValue("@Content", posting.Content);
        cmd.Parameters.AddWithValue("@DeliveryType", (int)posting.DeliveryType);
        cmd.Parameters.AddWithValue("@Weight", posting.Weight);
        cmd.Parameters.AddWithValue("@Width", posting.Width);
        cmd.Parameters.AddWithValue("@Height", posting.Height);
        cmd.Parameters.AddWithValue("@Depth", posting.Depth);
        cmd.Parameters.AddWithValue("@Value", posting.Value);
        cmd.Parameters.AddWithValue("@Price", posting.Price);
        cmd.Parameters.AddWithValue("@CreatedAt", posting.CreatedAt.Ticks);
    }

    private static Posting ReadPosting(SqliteDataReader reader)
    {
        return new Posting
        {
            Id = reader.GetInt32(reader.GetOrdinal("Id")),
            From = reader.GetString(reader.GetOrdinal("From")),
            To = reader.GetString(reader.GetOrdinal("To")),
            Content = reader.GetString(reader.GetOrdinal("Content")),
            DeliveryType = (DeliveryType)reader.GetInt32(reader.GetOrdinal("DeliveryType")),
            Weight = reader.GetFloat(reader.GetOrdinal("Weight")),
            Width = reader.GetInt32(reader.GetOrdinal("Width")),
            Height = reader.GetInt32(reader.GetOrdinal("Height")),
            Depth = reader.GetInt32(reader.GetOrdinal("Depth")),
            Value = reader.GetFloat(reader.GetOrdinal("Value")),
            Price = reader.GetFloat(reader.GetOrdinal("Price")),
            CreatedAt = new DateTime(reader.GetInt64(reader.GetOrdinal("CreatedAt")), DateTimeKind.Utc)
        };
    }
}
