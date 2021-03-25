import enum


class SubjectivityValence(enum.Enum):
    Objective = 0
    MostlyObjective = 1
    Subjective = 2
    VerySubjective = 3

    @staticmethod
    def from_value(subjectivity_value: float):
        if subjectivity_value > 0.75:
            return SubjectivityValence.VerySubjective
        if subjectivity_value > 0.5:
            return SubjectivityValence.Subjective
        if subjectivity_value > 0.25:
            return SubjectivityValence.MostlyObjective
        return SubjectivityValence.Objective
