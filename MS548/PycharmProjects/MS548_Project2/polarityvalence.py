import enum


class PolarityValence(enum.Enum):
    Neutral = 0
    Positive = 1
    VeryPositive = 2
    Negative = -1
    VeryNegative = -2

    @staticmethod
    def from_value(polarity_value: float):
        if polarity_value > 0.6:
            return PolarityValence.VeryPositive
        if polarity_value > 0.3:
            return PolarityValence.Positive
        if polarity_value > -0.3:
            return PolarityValence.Neutral
        if polarity_value > -0.6:
            return PolarityValence.Negative
        return PolarityValence.VeryNegative
