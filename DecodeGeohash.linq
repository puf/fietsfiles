<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Namespace>System.Windows</Namespace>
</Query>

var geohash = "dqcjq6";

var base32alphabet = "0123456789bcdefghjkmnpqrstuvwxyz";

Func<string,IEnumerable<bool>> Base32Decode = (s) => {
	if (s.Length > 12) throw new ArgumentOutOfRangeException(); // too many bits to put in a long
	long l = 0;
	var bitCount = 0;
	foreach (var c in s) {
		l <<= 5;
		bitCount += 5;
		l |= base32alphabet.IndexOf(c);
	}
	var result = new BitArray(BitConverter.GetBytes(l));
	result.Length = bitCount;
	return result.Cast<bool>();
};
Func<IEnumerable<bool>,int,IEnumerable<bool>> GetEveryOtherBit = (input,startAt) => input.Where( (b,i) => ((i-startAt) % 2) == 0 );
Func<IEnumerable<bool>,IEnumerable<bool>> GetOddBits  = (input) => GetEveryOtherBit(input,1);
Func<IEnumerable<bool>,IEnumerable<bool>> GetEvenBits = (input) => GetEveryOtherBit(input,0);
Func<IEnumerable<bool>,double,double,Point> Interpolate = (input, min, max) => {
	var range = new Point(min, max);
	foreach (var bit in input) {
		range = bit ? new Point(range.X + (range.Y-range.X)/2, range.Y) : new Point(range.X, range.Y - (range.Y-range.X)/2);
	}
	return range;
};

var bits = Base32Decode(geohash);

Interpolate(GetOddBits(bits).Reverse(), -90, 90).Dump("Lat");
Interpolate(GetEvenBits(bits).Reverse(), -180, 180).Dump("Lon");
