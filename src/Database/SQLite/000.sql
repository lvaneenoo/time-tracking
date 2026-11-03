CREATE TABLE timesheets
(
    timesheet_date   TEXT NOT NULL,
    timesheet_status INT  NOT NULL,
    modified_on      TEXT NOT NULL,

    PRIMARY KEY(timesheet_date)
);

CREATE TABLE timesheet_entries
(
    timesheet_date TEXT NOT NULL,
    period_start   TEXT NOT NULL,
    period_end     TEXT NOT NULL,
    comment        TEXT NOT NULL,

    PRIMARY KEY(timesheet_date, period_start, period_end),
    FOREIGN KEY(timesheet_date) REFERENCES timesheets(timesheet_date)
);

INSERT INTO timesheets(timesheet_date, timesheet_status, modified_on)
     VALUES ('2025-01-01', 0, strftime('%Y-%m-%d %H:%M:%f'));

INSERT INTO timesheet_entries(timesheet_date, period_start, period_end, comment)
     VALUES ('2025-01-01', '09:00', '09:59', 'Rework login screen'),
            ('2025-01-01', '10:00', '10:59', 'Implement OAuth');
