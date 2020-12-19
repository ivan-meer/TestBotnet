
SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `botnet`
--

-- --------------------------------------------------------

--
-- Table structure for table `bots`
--

CREATE TABLE IF NOT EXISTS `bots` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `ip` varchar(255) NOT NULL,
  `username` varchar(255) NOT NULL,
  `pcname` varchar(255) NOT NULL,
  `sysinfo` varchar(255) NOT NULL,
  `avname` varchar(255) NOT NULL,
  `machineguid` varchar(255) NOT NULL,
  `version` varchar(100) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=4 ;

-- --------------------------------------------------------

--
-- Table structure for table `tasks`
--

CREATE TABLE IF NOT EXISTS `tasks` (
  `cid` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `started` datetime NOT NULL,
  `completed` datetime NOT NULL,
  `cmd` varchar(15) NOT NULL,
  `arg1` varchar(100) NOT NULL,
  `arg2` varchar(100) NOT NULL,
  `arg3` varchar(100) NOT NULL,
  `arg4` varchar(100) NOT NULL,
  `status` varchar(15) NOT NULL,
  `target` varchar(255) NOT NULL,
  PRIMARY KEY (`cid`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=25 ;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
