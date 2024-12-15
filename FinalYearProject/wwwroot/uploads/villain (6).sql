-- phpMyAdmin SQL Dump
-- version 5.1.0
-- https://www.phpmyadmin.net/
--
-- 主机： 127.0.0.1
-- 生成日期： 2024-09-22 16:53:30
-- 服务器版本： 10.4.19-MariaDB
-- PHP 版本： 7.3.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- 数据库： `villain`
--

-- --------------------------------------------------------

--
-- 表的结构 `admin`
--

CREATE TABLE `admin` (
  `admin_id` int(11) NOT NULL,
  `name` varchar(15) NOT NULL,
  `email` varchar(30) NOT NULL,
  `password` varchar(255) NOT NULL,
  `phone` varchar(11) NOT NULL,
  `position` varchar(10) NOT NULL,
  `address` varchar(100) NOT NULL,
  `image` varchar(40) NOT NULL,
  `status` int(11) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- 转存表中的数据 `admin`
--

INSERT INTO `admin` (`admin_id`, `name`, `email`, `password`, `phone`, `position`, `address`, `image`, `status`) VALUES
(1, 'Gary Ong', 'gary@admin.com', 'D00F5D5217896FB7FD601412CB890830', '0162531969', 'Admin', '7-5-12 BLK 7 APT HIJAU RIA JLN 1/1A TAMAN KEPONG INDAH KEPONG 52100 WPKL', 'admin.png', 1),
(2, 'WONG KA LOCK', 'kalock@gmail.com', 'c76bb9c9aa1f4b93ccd5175675d2858d', '01111182167', 'Staff', ' Level 10, KPMG Tower, 8, First Avenue, Bandar Utama, 47800 Petaling Jaya, Selangor. 47800.', '2925e08ab354b22651312de916bf541b.jpg', 1),
(3, 'LEE MING SWIM', 'jayden@gmail.com', '5f2a128122a99f45b7d680f8eb11134d', '01158607475', 'Staff', 'pv15 blok 5-20-1 ,taman genting kelang ,setapak ,50890 ,wpkl', 'da44c1212d9aca7e6c3c1594c23fdd41.jpg', 1),
(4, 'Wang Teng Zheng', 'teng@gmail.com', '34b0da37dac84ccf0e2509b73cdb0e83', '01738470611', 'Staff', ' 2, Jalan 4/23F, Residensi Teratai, 53000 Kuala Lumpur, Wilayah Persekutuan Kuala Lumpur', '9d6322cacfc45ea7ed60e31ba45596c1.jpeg', 1),
(5, 'Wong Ming Jun', 'ming@gmail.com', 'dd5a7efc21160a59ed194033392df4e7', '01127377877', 'Staff', 'Jalan Usahawan 2 Taman Danau Kota, Wangsa Maju, 53300 Kuala Lumpur', 'e68a0d92448d5d1efa231460ce519a0e.jpeg', 0),
(6, 'Ho Jun Hui', 'alexho@gmail.com', 'd3a17fc54afbdc2041954459da8acb72', '01280830371', 'Staff', 'Taman Bunga Raya; Address: Setapak, 53000 Kuala Lumpur', '5c4a17a218122c9f0eae92e0d44828fb.jpeg', 1),
(7, 'Ng Wen Xing', 'wenxing@gmail.com', 'f465aadb77dee0176c41e4033a1640e6', '01133041763', 'Staff', 'Taman Bunga Raya; Address: Setapak, 53000 Kuala Lumpur', '230c4ce595b028895873e757de7e8efc.jpeg', 1),
(8, 'Wong Weng Chia', 'wengchia@gmail.com', 'a38dd290cb8c3163e0d3c12bcd963188', '01029796011', 'Staff', 'Laman Rimbunan. Taman Kepong, 52100 Kuala Lumpur, Wilayah Persekutuan.', '6ac6f8e3ecd64106897eff77d2b6ea0f.jpeg', 1),
(9, 'Looi Jing Jie', 'jeff@gmail.com', '933723d403332708f4a519aaf19bfb8d', '01423340981', 'Staff', ' PV15 Platinum Lake Condominium, Taman Danau Kota, 53300 Kuala Lumpur, Wilayah Persekutuan Kuala Lum', '3c039647bae55ff78db81008b579a71a.jpeg', 1),
(10, 'Chiew Chin Chon', 'cc@gmail.com', '0a05f442406e3a4e4d8ace137ea6fe33', '01022293061', 'Staff', 'abc blok k,90132 cheras', '9c97a590442cde4734daa36f2dd7a702.jpeg', 1),
(11, 'Tang Jian Xian', 'oscar@gmail.com', 'f69072bb55d978eb8c4a16a6338d60f5', '01111303353', 'Staff', 'PV15 Platinum Lake Condominium, Taman Danau Kota, 53300 Kuala Lumpur, Wilayah Persekutuan Kuala Lump', '89509e99c50f012e4fb233c6601ba8f8.jpeg', 1),
(12, 'Sharrean Genpat', 'party@gmail.com', 'c932b0cd7c2be26f3cd27413ce91ef47', '01660840311', 'Staff', 'Lebuh Perdana Barat, Presint 1, 62000 Putrajaya, Wilayah Persekutuan Putrajaya', '52e982d799feb1ab646abc4eed036026.jpeg', 1),
(13, 'Tee Kiek Yong', 'tee@gmail.com', '60361b9de367bdeb44c99b90f7486c26', '01276769961', 'Staff', 'PV15 Platinum Lake Condominium, Danau Kota, 53300 Kuala Lumpur, Federal Territory of Kuala Lumpur', '1cbb7fbd4f85c9d67b63bf3cf619ed22.jpeg', 1),
(14, 'HEHEXD', 'ccc@gmail.com', 'defb99e69a9f1f6e06f15006b1f166ae', '01234676897', 'Admin', 'No address yes', 'clock.png', 1);

-- --------------------------------------------------------

--
-- 替换视图以便查看 `archived_reviews`
-- （参见下面的实际视图）
--
CREATE TABLE `archived_reviews` (
`id` int(11)
,`user_name` varchar(255)
,`user_review` text
,`user_rating` int(11)
,`user_image` varchar(255)
,`datetime` datetime
,`status` enum('read','not read','archived')
,`archived_at` datetime
);

-- --------------------------------------------------------

--
-- 表的结构 `category`
--

CREATE TABLE `category` (
  `category_id` int(11) NOT NULL,
  `name` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- 表的结构 `customer`
--

CREATE TABLE `customer` (
  `cust_id` int(11) NOT NULL,
  `email` varchar(30) NOT NULL,
  `username` varchar(255) NOT NULL,
  `gender` varchar(8) NOT NULL,
  `phone` varchar(20) NOT NULL,
  `pass` varchar(30) NOT NULL,
  `otp` int(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- 转存表中的数据 `customer`
--

INSERT INTO `customer` (`cust_id`, `email`, `username`, `gender`, `phone`, `pass`, `otp`) VALUES
(1, 'ongyy-wm21@student.tarc.edu.my', '', '', '0149733977', '123456', 0),
(11, 'ongyeeyung@gmail.com', '', '', '0149733977', '1013', 0),
(12, 'shunfusia07@gmail.com', '', '', '0167777777', '123', 0),
(13, 'chiewchinchong@gmail.com', 'testuser', 'Male', '03-5636 4646', '', 0),
(20, 'yee@gmail.com', 'testuser', 'Male', '03-5636 4646', '$2y$12$40tQWuBdaA.ELYnltUubreF', NULL);

-- --------------------------------------------------------

--
-- 表的结构 `payment`
--

CREATE TABLE `payment` (
  `payment_id` int(11) NOT NULL,
  `cust_id` varchar(30) NOT NULL,
  `method` varchar(30) NOT NULL,
  `amount` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  `date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- 转存表中的数据 `payment`
--

INSERT INTO `payment` (`payment_id`, `cust_id`, `method`, `amount`, `quantity`, `date`) VALUES
(25, '10001', 'PayPal', 200, 1, '2024-09-17'),
(26, '10001', 'PayPal', 200, 1, '2024-09-17'),
(27, '10001', 'Credit Card', 1100, 6, '2024-09-17'),
(28, '10001', 'PayPal', 500, 3, '2024-09-17');

-- --------------------------------------------------------

--
-- 表的结构 `payment_ticket`
--

CREATE TABLE `payment_ticket` (
  `payment_ticket_id` int(11) NOT NULL,
  `payment_id` int(11) DEFAULT NULL,
  `ticket_id` int(11) DEFAULT NULL,
  `quantity` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- 转存表中的数据 `payment_ticket`
--

INSERT INTO `payment_ticket` (`payment_ticket_id`, `payment_id`, `ticket_id`, `quantity`) VALUES
(7, 25, 10001, 1),
(8, 25, 10002, 2),
(9, 26, 10001, 1),
(10, 26, 10002, 2),
(11, 26, 10001, 3),
(12, 27, 10001, 1),
(13, 27, 10002, 2),
(14, 27, 10001, 3),
(15, 28, 10001, 1),
(16, 28, 10002, 2);

-- --------------------------------------------------------

--
-- 表的结构 `review`
--

CREATE TABLE `review` (
  `review_id` int(11) NOT NULL,
  `rate` int(11) NOT NULL,
  `cust_commet` varchar(100) NOT NULL,
  `reply_commet` varchar(100) NOT NULL,
  `EventID` int(11) NOT NULL,
  `cust_id` int(11) NOT NULL,
  `admin_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- 表的结构 `review_table`
--

CREATE TABLE `review_table` (
  `id` int(11) NOT NULL,
  `user_name` varchar(255) NOT NULL,
  `user_review` text NOT NULL,
  `user_rating` int(11) NOT NULL,
  `user_image` varchar(255) DEFAULT NULL,
  `datetime` datetime NOT NULL,
  `status` enum('read','not read','archived') NOT NULL,
  `archived_at` datetime DEFAULT NULL,
  `admin_reply` text DEFAULT NULL,
  `reply_datetime` datetime DEFAULT NULL,
  `user_email` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- 转存表中的数据 `review_table`
--

INSERT INTO `review_table` (`id`, `user_name`, `user_review`, `user_rating`, `user_image`, `datetime`, `status`, `archived_at`, `admin_reply`, `reply_datetime`, `user_email`) VALUES
(74, 'uu', 'uuu', 5, NULL, '2024-09-18 18:52:58', 'not read', NULL, NULL, NULL, 'ongyeeyung@gmail.com'),
(75, 'yy', 'yy', 1, NULL, '2024-09-18 18:57:38', 'not read', NULL, NULL, NULL, 'shunfusia07@gmail.com'),
(77, 'cccc', 'ttttttttt ********', 4, '66f00003aa422.jpeg', '2024-09-18 22:04:39', 'not read', NULL, NULL, NULL, 'ongyeeyung@gmail.com'),
(78, 'email', '****', 3, NULL, '2024-09-20 18:53:58', 'not read', NULL, NULL, NULL, 'shunfusia07@gmail.com'),
(98, 'SHUNFU', 'This Ticket system is quite good ******** **** and ****', 4, '66eff3b9cb8a5.jpeg', '2024-09-21 13:56:06', 'not read', NULL, 'Thanks for you Review', NULL, 'shunfusia07@gmail.com'),
(100, 'CCC', 'ccc', 3, NULL, '2024-09-22 16:27:47', 'read', NULL, NULL, NULL, 'chiewchinchong@gmail.com'),
(103, 'CCC', 'HEHEXD', 3, NULL, '2024-09-22 16:41:58', 'not read', NULL, 'no', NULL, 'chiewchinchong@gmail.com'),
(104, 'HEHEXD', ',,,,,,', 4, NULL, '2024-09-22 17:14:28', 'not read', NULL, NULL, NULL, 'chiewchinchong@gmail.com'),
(105, 'YYY', 'YYY', 4, NULL, '2024-09-22 17:15:20', 'not read', NULL, NULL, NULL, 'yee@gmail.com'),
(109, 'last test123', 'last test123 ****', 3, NULL, '2024-09-22 19:13:27', 'not read', NULL, 'qwe', NULL, 'shunfusia07@gmail.com'),
(110, 'wqe', 'qwe **** and ********', 3, NULL, '2024-09-22 19:20:42', 'not read', NULL, NULL, NULL, 'shunfusia07@gmail.com');

-- --------------------------------------------------------

--
-- 表的结构 `schedule`
--

CREATE TABLE `schedule` (
  `schedule_id` int(11) NOT NULL,
  `EventID` int(11) NOT NULL,
  `time` varchar(10) NOT NULL,
  `startdate` date NOT NULL,
  `enddate` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- 转存表中的数据 `schedule`
--

INSERT INTO `schedule` (`schedule_id`, `EventID`, `time`, `startdate`, `enddate`) VALUES
(8, 1, '10:03', '2024-07-30', '2025-09-24');

-- --------------------------------------------------------

--
-- 表的结构 `ticket`
--

CREATE TABLE `ticket` (
  `ticket_id` int(11) NOT NULL,
  `price` int(11) NOT NULL,
  `schedule_id` int(11) NOT NULL,
  `image` varchar(40) NOT NULL,
  `slot` int(11) NOT NULL,
  `slot_sold` int(11) NOT NULL,
  `category` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- 转存表中的数据 `ticket`
--

INSERT INTO `ticket` (`ticket_id`, `price`, `schedule_id`, `image`, `slot`, `slot_sold`, `category`) VALUES
(10001, 200, 8, 'logo.png', 100, 0, 'Standard'),
(10002, 150, 8, 'logo.png', 100, 0, 'VIP'),
(0, 500, 8, 'husky.jpg', 10, 0, 'SuperVIP');

-- --------------------------------------------------------

--
-- 表的结构 `villain`
--

CREATE TABLE `villain` (
  `EventID` int(11) NOT NULL,
  `EventName` varchar(40) NOT NULL,
  `location` varchar(50) NOT NULL,
  `Description` varchar(500) NOT NULL,
  `StartDate` date NOT NULL,
  `Seat` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- 转存表中的数据 `villain`
--

INSERT INTO `villain` (`EventID`, `EventName`, `location`, `Description`, `StartDate`, `Seat`) VALUES
(1, 'League of Legends', 'TAR UMT Red Bricks Cafeteria', '....', '2024-08-30', 500),
(2, 'Among Us', 'TAR UMT Red Bricks Cafeteria', 'A Among Us gaming event! Join us for an exciting gaming experience and get a chance to win some exclusive among us merchandise.', '2024-09-15', 100),
(3, 'Overwatchs', 'Kuala Lumpur Convention Centre ', 'An Overwatch E-sport gaming event collab with UTAR Kampar E-sports and Game Development Club - UKEC that contains Overwatch workshop for beginners and a small Overwatch tournament.', '2024-08-31', 100),
(4, 'Genshin Impact', 'Kuala Lumpur Convention Centre ', 'Genshin Impact is an action role-playing game developed by miHoYo. Set in the fantasy world of Teyvat, players assume the role of the Traveler, who searches for their lost sibling while unraveling the mysteries of Teyvat elemental powers.', '2024-09-10', 100),
(5, 'Warcrafts III', 'Kuala Lumpur Convention Centre ', 'Warcraft III: Reign of Chaos is a real-time strategy game released by Blizzard Entertainment in 2002. It features four playable factions - Humans, Orcs, Night Elves, and Undead - each with unique units, heroes, and abilities.', '2024-09-15', 100),
(6, 'Nikke', 'Kuala Lumpur Convention Centre ', 'NIKKE, or \"Nikke: The Goddess of Victory,\" is a mobile action RPG developed by Shift Up and published by Level Infinite. Set in a post-apocalyptic future, the game features a mix of fast-paced shooting combat and character collection mechanics.', '2024-09-20', 100),
(7, 'Wuthering Waves', 'Kuala Lumpur Convention Centre ', 'Wuthering Waves is an open-world action RPG developed by Kuro Game. Set in a post-apocalyptic world, players explore vast environments, battle enemies, and uncover the story of humanity\'s survival. The game features dynamic combat, character customization, and a deep narrative.', '2024-09-30', 100),
(8, 'Honor of Kings', 'Mid Valley Exhibition Centre (MVEC)', 'Honor of Kings is a popular multiplayer online battle arena (MOBA) game developed by Tencent Games. Players form teams and select heroes with unique skills to engage in strategic, real-time battles. The objective is to destroy the enemy\'s base while coordinating with teammates and using tactical gameplay.', '2024-10-05', 100),
(9, 'Honkai Impact', 'Pavilion Bukit Jalil', 'Honkai Impact 3rd is a free-to-play 3D action role-playing game developed by miHoYo. Released globally in 2018, it’s a part of the larger \"Honkai\" series and known for its high-quality graphics, dynamic gameplay, and compelling storylines.', '2024-09-11', 100),
(10, 'Mahjong Souls', 'National Stadium Bukit Jalil', 'Mahjong Soul is an online mahjong game developed by Cat Food Studio and published by Yostar. Released in 2019, it combines traditional Japanese riichi mahjong with anime aesthetics, making it accessible to both newcomers and experienced players.', '2024-10-31', 100),
(11, 'Palword', 'Tropicana Gardens Mall', 'Palworld is an upcoming open-world survival and creature-collecting game developed by Pocket Pair, the same studio behind Craftopia. It combines elements from multiple genres, including creature taming, crafting, building, and combat.', '2024-10-22', 100),
(12, 'Teamfight Tactics', 'Tropicana Gardens Mall', 'Teamfight Tactics (TFT) is an auto-battler strategy game developed by Riot Games, set within the universe of League of Legends. Released in 2019, TFT is part of the broader auto-battler genre, where players form teams of champions that fight automatically against the teams of other players.', '2024-10-17', 100),
(13, 'Zenless Zone Zero', 'Pavilion Bukit Jalil', 'Zenless Zone Zero (ZZZ) is an upcoming action role-playing game developed by miHoYo, the creators of Genshin Impact and Honkai Impact 3rd. Announced in 2022, the game is set in a futuristic, urban fantasy world and features fast-paced, hack-and-slash combat.', '2024-11-22', 100),
(14, 'Black Wukong', 'Kuala Lumpur Convention Centre', 'Black Myth: Wukong is shaping up to be an impressive action RPG that blends Chinese mythology with intense, Souls-like combat and next-gen visuals. With its unique take on Journey to the West, detailed world-building, and innovative gameplay mechanics.', '2024-12-28', 100),
(15, 'Valorants', 'Kuala Lumpur Convention Centre', 'Valorant is a free-to-play, tactical first-person shooter developed by Riot Games, released in 2020. It combines elements of Counter-Strike\'s strategic gunplay with unique \"hero shooter\" abilities, similar to Overwatch. Here’s a brief overview:', '2025-01-01', 100),
(16, 'Apex Legends', 'Tropicana Gardens Mall', 'Apex Legends is a free-to-play battle royale game developed by Respawn Entertainment and published by Electronic Arts. It was released in 2019 and is set in the same universe as the Titanfall series. The game is known for its fast-paced action, unique characters (called \"Legends\"), and team-focused gameplay.', '2025-01-10', 100),
(17, 'Rainbox Six Siege', 'Tropicana Gardens Mall', 'Rainbow Six Siege is a tactical first-person shooter developed by Ubisoft, released in 2015. Unlike many fast-paced shooters, it emphasizes strategy, teamwork, and environmental destruction, making it one of the most popular games in the tactical shooter genre.', '2025-01-30', 100);

-- --------------------------------------------------------

--
-- 视图结构 `archived_reviews`
--
DROP TABLE IF EXISTS `archived_reviews`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `archived_reviews`  AS SELECT `review_table`.`id` AS `id`, `review_table`.`user_name` AS `user_name`, `review_table`.`user_review` AS `user_review`, `review_table`.`user_rating` AS `user_rating`, `review_table`.`user_image` AS `user_image`, `review_table`.`datetime` AS `datetime`, `review_table`.`status` AS `status`, `review_table`.`archived_at` AS `archived_at` FROM `review_table` WHERE `review_table`.`archived_at` is not null ;

--
-- 转储表的索引
--

--
-- 表的索引 `admin`
--
ALTER TABLE `admin`
  ADD PRIMARY KEY (`admin_id`);

--
-- 表的索引 `category`
--
ALTER TABLE `category`
  ADD PRIMARY KEY (`category_id`);

--
-- 表的索引 `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`cust_id`);

--
-- 表的索引 `payment`
--
ALTER TABLE `payment`
  ADD PRIMARY KEY (`payment_id`);

--
-- 表的索引 `payment_ticket`
--
ALTER TABLE `payment_ticket`
  ADD PRIMARY KEY (`payment_ticket_id`);

--
-- 表的索引 `review`
--
ALTER TABLE `review`
  ADD PRIMARY KEY (`review_id`);

--
-- 表的索引 `review_table`
--
ALTER TABLE `review_table`
  ADD PRIMARY KEY (`id`);

--
-- 表的索引 `schedule`
--
ALTER TABLE `schedule`
  ADD PRIMARY KEY (`schedule_id`),
  ADD UNIQUE KEY `EventID` (`EventID`);

--
-- 表的索引 `villain`
--
ALTER TABLE `villain`
  ADD PRIMARY KEY (`EventID`),
  ADD UNIQUE KEY `EventID` (`EventID`);

--
-- 在导出的表使用AUTO_INCREMENT
--

--
-- 使用表AUTO_INCREMENT `admin`
--
ALTER TABLE `admin`
  MODIFY `admin_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- 使用表AUTO_INCREMENT `category`
--
ALTER TABLE `category`
  MODIFY `category_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- 使用表AUTO_INCREMENT `customer`
--
ALTER TABLE `customer`
  MODIFY `cust_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- 使用表AUTO_INCREMENT `payment`
--
ALTER TABLE `payment`
  MODIFY `payment_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- 使用表AUTO_INCREMENT `payment_ticket`
--
ALTER TABLE `payment_ticket`
  MODIFY `payment_ticket_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- 使用表AUTO_INCREMENT `review`
--
ALTER TABLE `review`
  MODIFY `review_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- 使用表AUTO_INCREMENT `review_table`
--
ALTER TABLE `review_table`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=111;

--
-- 使用表AUTO_INCREMENT `schedule`
--
ALTER TABLE `schedule`
  MODIFY `schedule_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- 使用表AUTO_INCREMENT `villain`
--
ALTER TABLE `villain`
  MODIFY `EventID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
