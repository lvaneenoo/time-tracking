CREATE TABLE time_sheet_entries
(
    time_sheet_date TEXT NOT NULL,
    period_start    TEXT NOT NULL,
    period_end      TEXT NOT NULL,
    PRIMARY KEY(time_sheet_date, period_start, period_end)
);

INSERT INTO time_sheet_entries(time_sheet_date, period_start, period_end)
     VALUES ('2025-01-01', '09:00:00.0000000', '09:59:00.0000000'),
            ('2025-01-01', '10:00:00.0000000', '10:59:00.0000000');
