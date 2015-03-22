# -*- coding: utf-8 -*-
"""
Spyder Editor

This temporary script file is located here:
C:\Users\Jonas\.spyder2\.temp.py
"""
import urllib2
from BeautifulSoup import BeautifulSoup

soup = BeautifulSoup(urllib2.urlopen('http://www.premierleague.com/en-gb.html').read())

table = soup.find('table', {'class': 'leagueTable'})

rows = table.findAll('tr')

for row in rows:
    if row.find('td', {'class': 'col-club'}):
        print row.find('a').string,
        print row.find('td', {'class': 'col-pts'}).string


