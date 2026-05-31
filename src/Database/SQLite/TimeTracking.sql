CREATE TABLE time_sheets
(
    time_sheet_date   TEXT NOT NULL,
    time_sheet_status INT  NOT NULL,
    modified_on       TEXT NOT NULL,

    PRIMARY KEY(time_sheet_date)
);

CREATE TABLE time_sheet_entries
(
    time_sheet_date TEXT NOT NULL,
    period_start    TEXT NOT NULL,
    period_end      TEXT NOT NULL,

    PRIMARY KEY(time_sheet_date, period_start, period_end),
    FOREIGN KEY(time_sheet_date) REFERENCES time_sheets(time_sheet_date)
);

INSERT INTO time_sheets(time_sheet_date, time_sheet_status, modified_on)
     VALUES ('2025-01-01', 0, strftime('%Y-%m-%d %H:%M:%f'));

INSERT INTO time_sheet_entries(time_sheet_date, period_start, period_end)
     VALUES ('2025-01-01', '09:00', '09:59'),
            ('2025-01-01', '10:00', '10:59');
